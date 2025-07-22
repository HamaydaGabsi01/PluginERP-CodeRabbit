using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using AtooERP.Document;
using AtooERP.Type_document;

namespace SAM_DOC
{
    public partial class SAM_APIForm : AtooERP.deskTopModel.deskTopModelForm.deskTopModelFormState
    {
        public static (string documentsFolder, Dictionary<string, Type_document> documentTypes) LoadConfig()
        {
            XDocument doc;
            var documentTypes = new Dictionary<string, Type_document>();
            try
            {
                doc = XDocument.Load(Program.configPath);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+ "\n Verify that the configuration file exisits in "+ Program.configPath);
                return("", documentTypes);
            }
            string documentsFolder = doc.Root.Element("DocumentsFolder").Value;
            
            try
            {
                documentTypes = doc.Root.Element("DocumentTypes")
                    .Elements("Type")
                    .Select(type => (type.Element("FileName").Value, new Type_document(Convert.ToInt32(type.Element("TypeId").Value))))
                    .ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n Verify Configuration for the document types!");
                return(documentsFolder, documentTypes);
            }


            return (documentsFolder, documentTypes);
        }
        public SAM_APIForm()
        {
            InitializeComponent();

            this.LoadDocuments();
        }

        public override void setTitle()
        {
            titre.Text = "SAM_DOC";
        }

        public void setLogOnImport(int docId, string docName)

        {
            AtooERP.Log.setLog(GetType().FullName, docId, docName, "import", null);

            return;
        }

        public void LoadDocuments()
        {
            var (documentsFolder, documentTypes) = LoadConfig();
            if (!Directory.Exists(documentsFolder))
            {
                DialogResult result = MessageBox.Show("The documents path specified in the configuration does not exist. Do you want to change it?", "Documents path not found", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {

                    if (EditConfiguration())
                    {
                        LoadDocuments();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else if (result == DialogResult.No)
                {
                    this.Close();
                }
            }
            else
                try
                {
                    var documents = Document_information.LoadDocumentsInfo(documentsFolder, documentTypes);
                    if (ShowAllCheckBox.Checked)
                    {
                        DocumentsListGridControl.DataSource  = documents;
                    }
                    else
                    {
                        DocumentsListGridControl.DataSource =  documents.Where(d => d.reservationId != 0).ToList();
                    }
                    
                } catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private void EditConfigSimpleButton_Click(object sender, EventArgs e)
        {
            if (EditConfiguration())
            {
                LoadDocuments();
            }

        }
        static public bool EditConfiguration()
        {
            using (var configForm = new ConfigurationForm())
            {
                return configForm.ShowDialog() == DialogResult.OK;
            }
        }
        internal async Task SetLogOnImportAsync(int docId, string docName)
        {
            await Task.Run(() => setLogOnImport(docId, docName));
        }
        ConcurrentQueue<Task> loggingTasks = new ConcurrentQueue<Task>();
        private async void ImportSimpleButton_Click(object sender, EventArgs e)
        {
            DialogResult rslt = MessageBox.Show("Would you like to insert all files. If not, some files might be overwritten!", "Import", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            bool insertAll = false;
            if (rslt == DialogResult.Yes)
            {
                insertAll = true;
            }

            int totalRows = DocumentsListGridView.GetSelectedRows()
                .Where((index) =>
                {
                    Document_information row = DocumentsListGridView.GetRow(index) as Document_information;
                    return row.reservationId != 0 && row.type_document >0;
                })
                .ToList().Count;

            progressLayoutControlGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            progressBarControl1.Properties.Minimum = 0;
            progressBarControl1.Properties.Maximum = totalRows;
            progressBarControl1.EditValue = 0;
            int imported = 0;
            Func<int> getPercentage = () => totalRows != 0 ? Convert.ToInt32((Convert.ToDecimal(imported) / totalRows) * 100) : 0;
            await Task.Run(() =>
            {
                foreach (int row in DocumentsListGridView.GetSelectedRows())
                {
                    bool hasMultipleOfType = false;
                    Document_information file = DocumentsListGridView.GetRow(row) as Document_information;
                    if (file == null || file.type_document == 0)
                        continue;
                    foreach (int _row in DocumentsListGridView.GetSelectedRows())
                    {
                        Document_information _file = DocumentsListGridView.GetRow(_row) as Document_information;
                        if (_row == row)
                            break;
                        if (_file != null && _file.type_document == file.type_document && _file.applicationId == file.applicationId && _file.fileName != file.fileName)
                        {
                            hasMultipleOfType = true;
                            break;
                        }
                    }
                    Invoke((Action)(() =>
                    {
                        progressTextEdit.Text = $"Importing: { imported } out of { totalRows } ({ getPercentage.Invoke() }%)... reading {file.path}";
                    }));
                    if (file.reservationId == 0)
                        continue;
                    int reservationId = file.reservationId;
                    BindingList<Document> DocumentList = AtooERP.Document.Document.getListByPiece_typeAndPiece("Booking.Reservation, Booking, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", reservationId);
                    Invoke((Action)(() =>
                    {
                        progressTextEdit.Text = $"Importing: { imported } out of { totalRows } ({ (Convert.ToDecimal(imported) / totalRows) * 100 }%)... comparing with existing documents";
                    }));

                    bool exist = false;
                    
                    foreach (AtooERP.Document.Document Doc in DocumentList)
                    {
                        if (Doc.type_document == file.type_document &&(( !insertAll && !hasMultipleOfType) || Doc.content == null))
                        {
                            Invoke((Action)(() =>
                            {
                                progressTextEdit.Text = $"Importing: { imported } out of { totalRows } ({ getPercentage.Invoke() }%)... updating  {Doc.name}";
                            }));
                            exist = true;
                            Doc.date = DateTime.Now;
                            Doc.content = file.content;
                            Doc.extension = file.extension;
                            Doc.size = file.size;
                            Doc.update();
                            loggingTasks.Enqueue(SetLogOnImportAsync(Doc.Id, Doc.name));
                            break;
                        }
                    }

                    if (!exist)
                    {
                        string docName = file.type_documentName + " | Res[" + file.reservationId + "]: " + file.firstName + " " + file.lastName;
                        Invoke((Action)(() =>
                        {
                            progressTextEdit.Text = $"Importing: { imported } out of { totalRows } ({ getPercentage.Invoke() }%)... inserting  {docName}";
                        }));
                        new AtooERP.Document.Document(docName, DateTime.Now, null, null, DateTime.Now, null, file.extension, (uint)file.type_document, file.content, reservationId, "Booking.Reservation, Booking, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", DateTime.Now).insert();
                        int docIdnew = Convert.ToInt32(new AtooERP.DataSetMoezTableAdapters.atooerp_documentTableAdapter().getLastId());
                        loggingTasks.Enqueue(SetLogOnImportAsync(docIdnew, docName));
                    }
                    if (TypeMapping.DocIsPicture(file.type_document))
                    {
                        try
                        {
                            new DataSetSAM_APITableAdapters.atooerp_personTableAdapter().UpdatePictureByReservation(file.content, file.reservationId);

                        }
                        catch { }
                    }
                    try
                    {
                        Invoke((Action)(() =>
                        {
                            progressTextEdit.Text = $"Importing: { imported } out of { totalRows } ({ getPercentage.Invoke() }%)... deleting {file.path}";
                        }));
                        File.Delete(file.path);
                        string directoryPath = Path.GetDirectoryName(file.path);

                        if (!Directory.EnumerateFileSystemEntries(directoryPath).Any())
                        {
                            Directory.Delete(directoryPath);
                        }
                    }
                    catch (FileNotFoundException ex)
                    {
                        Console.WriteLine("File already deleted");
                    }
                    Invoke((Action)(() =>
                    {
                        imported++;
                        progressBarControl1.EditValue = imported;
                    }));
                }

            });
            progressTextEdit.Text = $"Importing: { imported } out of { totalRows } ({ getPercentage.Invoke() }%) completed.";
            this.LoadDocuments();
            DocumentsListGridView.RefreshData();
            Console.WriteLine("ImportSimpleButton_Click method completed");
            MessageBox.Show("Import completed successfully!", "Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
            progressLayoutControlGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            
                Parallel.ForEach(loggingTasks, async (task) => await task);
            
        }

        private void ImportAllSimpleButton_Click(object sender, EventArgs e)
        {
            DocumentsListGridView.SelectAll();
            ImportSimpleButton_Click(sender, e);
        }

        private void OpenDocumentSimpleButton_Click(object sender, EventArgs e)
        {
            DocumentsListGridView_DoubleClick(sender, e);

        }

        private void DocumentsListGridView_DoubleClick(object sender, EventArgs e)
        {
            Document_information file = DocumentsListGridView.GetFocusedRow() as Document_information;
            string path = Path.GetTempFileName();
            File.WriteAllBytes(path + file.extension, file.content);
            Process.Start(path + file.extension);
        }

        private void Print_SimpleButton_Click(object sender, EventArgs e)
        {

                if (!DocumentsListGridControl.IsPrintingAvailable)
                {
                    MessageBox.Show("The 'DevExpress.XtraPrinting' library is not found", "Error");
                    return;
                }
                // Open the Preview window.
                AtooERP.GridReport GridReport = new AtooERP.GridReport(DocumentsListGridControl, "Liste Documents");
                GridReport.print();
            
        }

        private void refreshSimpleButton_Click(object sender, EventArgs e)
        {
            this.LoadDocuments();
        }

        private void DeleteSimpleButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DocumentsListGridView.SelectedRowsCount; i++)
            {
                try
                {
                Document_information file = DocumentsListGridView.GetRow(i) as Document_information;
                    File.Delete(file.path);
                    string directory = Path.GetDirectoryName(file.path);
                    if (directory != null && Directory.Exists(directory) &&
                        !Directory.EnumerateFileSystemEntries(directory).Any())
                    {
                        Directory.Delete(directory);
                    }

                }
                catch { }
                
            }
            this.LoadDocuments();
        }

        private void ShowAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            LoadDocuments();
        }

    }

}

