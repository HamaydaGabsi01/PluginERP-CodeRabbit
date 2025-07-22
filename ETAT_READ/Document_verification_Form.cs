using AtooERP.Document;
using AtooERP.Type_document;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETAT_READ
{
    public partial class Document_verification_Form : AtooERP.deskTopModel.deskTopModelForm.deskTopModelFormState
    {
        string application_id;
        string archiveFolder;
        private bool importButtonPressed = false;
        
        // Public property to expose the import button state
        public bool WasImportButtonPressed => importButtonPressed;
        
        public Document_verification_Form(string application_id)
        {
            InitializeComponent();
            this.application_id = application_id;
            SAM_DOC.Properties.Settings.Default["atooerpConnectionString"] = Properties.Settings.Default["atooerpConnectionString"];
            this.titre.Text = "Importer des documents";
            LoadTypeDocument();
        }
        public void LoadDocuments()
        {
            var (documentsFolder,archiveFolder, documentTypes) = ETAT_READ_Form.LoadConfig();
            this.archiveFolder = archiveFolder;
            if (!Directory.Exists(documentsFolder))
            {
                DialogResult result = MessageBox.Show("Le chemin des documents spécifié dans la configuration n'existe pas. Souhaitez-vous le modifier ?", "Chemin des documents introuvable", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {

                    if (ETAT_READ_Form.EditConfiguration())
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
                    var documents = Document_information.LoadDocumentsInfo(documentsFolder, documentTypes, this.application_id);
                        DocumentsListGridControl.DataSource = documents;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private void LoadTypeDocument()
        {
            documentTypesRepositoryItemLookUpEdit.DataSource = Type_document.getCollectionList();
            documentTypesRepositoryItemLookUpEdit.ValueMember = "Id";
            documentTypesRepositoryItemLookUpEdit.DisplayMember = "name";
            documentTypesRepositoryItemLookUpEdit.Columns.Clear();
            documentTypesRepositoryItemLookUpEdit.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("name", "Document Type"));
        }

        private void OpenDocumentSimpleButton_Click(object sender, EventArgs e)
        {
            DocumentsListGridView_DoubleClick(sender, e);
        }

        private async void ImportSimpleButton_Click(object sender, EventArgs e)
        {
            // Get all data source documents
            var allDocuments = DocumentsListGridView.DataSource as BindingList<Document_information>;
            if (allDocuments == null)
            {
                MessageBox.Show("Aucun document n'est disponible.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Create a hashset of selected file names for quick lookup
            HashSet<string> selectedFileNames = new HashSet<string>();
            foreach (int rowIndex in DocumentsListGridView.GetSelectedRows())
            {
                Document_information doc = DocumentsListGridView.GetRow(rowIndex) as Document_information;
                if (doc != null)
                {
                    // Sanitize file name for consistent comparison
                    string sanitizedFileName = ArchiveHelper.SanitizeFilePath(doc.fileName);
                    selectedFileNames.Add(sanitizedFileName);
                }
            }

            // Build lists of accepted and rejected documents
            List<string> acceptedDocuments = new List<string>();
            List<string> rejectedDocuments = new List<string>();
            
            // All selected documents are accepted
            foreach (int rowIndex in DocumentsListGridView.GetSelectedRows())
            {
                Document_information doc = DocumentsListGridView.GetRow(rowIndex) as Document_information;
                if (doc != null)
                {
                    acceptedDocuments.Add(doc.fileName);
                }
            }
            
            // All unselected documents are rejected
            foreach (Document_information doc in allDocuments)
            {
                if (doc != null)
                {
                    // Sanitize file name for consistent comparison
                    string sanitizedFileName = ArchiveHelper.SanitizeFilePath(doc.fileName);
                    if (!selectedFileNames.Contains(sanitizedFileName))
                    {
                        rejectedDocuments.Add(doc.fileName);
                    }
                }
            }
            
            // Build confirmation message
            StringBuilder messageBuilder = new StringBuilder();
            messageBuilder.AppendLine("Voulez-vous procéder à l'importation des documents suivants ?");
            messageBuilder.AppendLine();
            
            messageBuilder.AppendLine("Documents acceptés :");
            if (acceptedDocuments.Count > 0)
            {
                foreach (string doc in acceptedDocuments)
                {
                    messageBuilder.AppendLine("- " + doc);
                }
            }
            else
            {
                messageBuilder.AppendLine("- Aucun document accepté");
            }
            
            messageBuilder.AppendLine();
            messageBuilder.AppendLine("Documents rejetés :");
            if (rejectedDocuments.Count > 0)
            {
                foreach (string doc in rejectedDocuments)
                {
                    messageBuilder.AppendLine("- " + doc);
                }
            }
            else
            {
                messageBuilder.AppendLine("- Aucun document rejeté");
            }
            
            // Show confirmation dialog
            DialogResult confirmResult = MessageBox.Show(
                messageBuilder.ToString(),
                "Confirmation d'importation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                
            if (confirmResult == DialogResult.No)
            {
                return; // User canceled import
            }
            
            // Original import code starts here
            DialogResult rslt = MessageBox.Show("Souhaitez-vous insérer tous les fichiers ? Si non, certains fichiers pourraient être écrasés !", "Importation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            bool insertAll = false;
            if (rslt == DialogResult.Yes)
            {
                insertAll = true;
            }

            int totalRows = DocumentsListGridView.GetSelectedRows()
                .Where((index) =>
                {
                    Document_information row = DocumentsListGridView.GetRow(index) as Document_information;
                    return row.reservationId != 0; // Modified to count documents regardless of type_document value
                })
                .ToList().Count;

            int imported = 0;
            Func<int> getPercentage = () => totalRows != 0 ? Convert.ToInt32((Convert.ToDecimal(imported) / totalRows) * 100) : 0;
            await Task.Run(() =>
            {
                foreach (int row in DocumentsListGridView.GetSelectedRows())
                {
                    bool hasMultipleOfType = false;
                    Document_information file = DocumentsListGridView.GetRow(row) as Document_information;
                    if (file == null) // Removed type_document check
                        continue;
                        
                    // Only check for multiple of same type if the document has a type
                    if (file.type_document > 0)
                    {
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
                    }

                    if (file.reservationId == 0)
                        continue;
                    int reservationId = file.reservationId;
                    BindingList<Document> DocumentList = AtooERP.Document.Document.getListByPiece_typeAndPiece("Booking.Reservation, Booking, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", reservationId);

                    bool exist = false;

                    // For documents with valid type, check if we can update existing document
                    if (file.type_document > 0)
                    {
                        foreach (AtooERP.Document.Document Doc in DocumentList)
                        {
                            if (Doc.type_document == file.type_document && ((!insertAll && !hasMultipleOfType) || Doc.content == null))
                            {
                                
                                exist = true;
                                Doc.date = DateTime.Now;
                                Doc.content = file.content;
                                Doc.extension = file.extension;
                                Doc.size = file.size;
                                Doc.update();
                                break;
                            }
                        }
                    }

                    if (!exist)
                    {
                        string docName;
                        if (file.type_document > 0)
                        {
                            docName = file.type_documentName + " | Res[" + file.reservationId + "]: " + file.firstName + " " + file.lastName;
                        }
                        else
                        {
                            // For documents without type, use filename as document name
                            docName = file.fileName + " | Res[" + file.reservationId + "]: " + file.firstName + " " + file.lastName;
                        }
                        uint? documentType = file.type_document > 0 ? (uint?)file.type_document : null;


                        new AtooERP.Document.Document(docName, DateTime.Now, null, null, DateTime.Now, null, file.extension, documentType, file.content, reservationId, "Booking.Reservation, Booking, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", DateTime.Now).insert();
                        int docIdnew = Convert.ToInt32(new AtooERP.DataSetMoezTableAdapters.atooerp_documentTableAdapter().getLastId());
                    }
                    
                    // Only update picture if document is a picture type
                    if (file.type_document > 0 && TypeMapping.DocIsPicture(file.type_document))
                    {
                        try
                        {
                            new SAM_DOC.DataSetSAM_APITableAdapters.atooerp_personTableAdapter().UpdatePictureByReservation(file.content, file.reservationId);

                        }
                        catch { }
                    }
                }

            });
            
            // Move files to accept/reject folders BEFORE refreshing data
            try {
                foreach (Document_information doc in allDocuments)
                {
                        if (doc != null && doc.path != null)
                        {
                            // Sanitize file name for consistent comparison
                            string sanitizedFileName = ArchiveHelper.SanitizeFilePath(doc.fileName);
                        // If file is in selectedFileNames hashset, it's accepted (imported), otherwise rejected
                            bool isAccepted = selectedFileNames.Contains(sanitizedFileName);
                            
                            // Check if file exists before trying to move it
                            if (File.Exists(doc.path))
                            {
                                try
                                {
                                    ArchiveHelper.MoveFileToAcceptRejectFolder(doc.path, this.archiveFolder, isAccepted);
                                }
                                catch (Exception moveEx)
                                {
                                    // Log the error but continue with other files
                                    Console.WriteLine($"Error moving file {doc.path}: {moveEx.Message}");
                            }
                        }
                    }
                }
            }
            catch(Exception ex) 
            {
                // Log the error but don't crash the application
                Console.WriteLine($"Error during file archiving process: {ex.Message}");
            }
            
            this.LoadDocuments();
            DocumentsListGridView.RefreshData();
            
            MessageBox.Show("Importation terminée avec succès !", "Importation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            importButtonPressed = true;
            this.Close();
        }
        private void DocumentsListGridView_DoubleClick(object sender, EventArgs e)
        {
            Document_information file = DocumentsListGridView.GetFocusedRow() as Document_information;
            string path = Path.GetTempFileName();
            File.WriteAllBytes(path + file.extension, file.content);
            Process.Start(path + file.extension);
        }

        private void Document_verification_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!importButtonPressed)
            {
                DialogResult result = MessageBox.Show(
                    "Vous n'avez pas importé de documents. Vous pourrez les importer plus tard. Voulez-vous vraiment fermer ce formulaire?",
                    "Confirmation de fermeture",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
