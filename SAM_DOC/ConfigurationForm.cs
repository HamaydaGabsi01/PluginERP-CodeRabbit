using AtooERP.Type_document;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SAM_DOC
{
    public partial class ConfigurationForm : AtooERP.deskTopModel.deskTopModelForm.deskTopModelFormState
    {


        public ConfigurationForm()
        {
            InitializeComponent();
            LoadTypeDocument();
            LoadConfiguration();
        }

        public override void setTitle()
        {
            titre.Text = "SAM_API Configuration";
        }
        public void setLogOnValidate()

        {
            AtooERP.Log.setLog(GetType().FullName, 0, "", "save", null);
            return;
        }
        private void LoadTypeDocument()
        {
            DocumentTypeRepositoryItemLookUpEdit.DataSource = Type_document.getCollectionList();
            DocumentTypeRepositoryItemLookUpEdit.ValueMember = "Id";
            DocumentTypeRepositoryItemLookUpEdit.DisplayMember = "name";
            DocumentTypeRepositoryItemLookUpEdit.Columns.Clear();
            DocumentTypeRepositoryItemLookUpEdit.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("name", "Document Type"));
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void DocumentTypesGridView_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var dataTable = DocumentTypeRepositoryItemLookUpEdit.DataSource as DataTable;
            var selectedId = dataTable.Rows[0]["Id"];
            DocumentTypesGridView.SetRowCellValue(e.RowHandle, "TypeId", selectedId);

        }

        private void LoadConfiguration()
        {
            var (documentsFolder, loadedDocumentTypes) = SAM_APIForm.LoadConfig();
            PathTextEdit.Text = documentsFolder;
            TypeMappingBindingSource.DataSource = TypeMapping.getList();
        }

        private void SaveConfiguration()
        {
            var typeMappings = TypeMappingBindingSource.DataSource as IEnumerable<TypeMapping>;


            XDocument doc = new XDocument(
                new XElement("Configuration",
                    new XElement("DocumentsFolder", PathTextEdit.Text),
                    new XElement("DocumentTypes",
                        from type in typeMappings
                        select new XElement("Type",
                            new XElement("FileName", type.FileName),
                            new XElement("TypeId", type.TypeId.ToString()),
                            new XElement("isPicture", Convert.ToBoolean(type.isPicture))
                        )
                    )
                )
            );

            doc.Save(Program.configPath);
            setLogOnValidate();
        }

        private void SaveSimpleButton_Click(object sender, EventArgs e)
        {

            string path = PathTextEdit.Text.Trim();


            if (!Directory.Exists(path))
            {
                MessageBox.Show($"The specified path '{path}' does not exist. Please enter a valid path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (CheckForDuplicateFileNames())
            {
                MessageBox.Show("Duplicate FileNames detected. Please ensure all FileNames are unique before saving.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveConfiguration();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        private bool CheckForDuplicateFileNames()
        {
            var fileNames = new HashSet<string>();

            for (int i = 0; i < DocumentTypesGridView.RowCount; i++)
            {
                var row = DocumentTypesGridView.GetRow(i) as TypeMapping;
                if (row != null)
                {
                    if (!string.IsNullOrWhiteSpace(row.FileName))
                    {
                        if (!fileNames.Add(row.FileName))
                        {
                            
                            return true; 
                        }
                    }
                }
            }

            return false; 
        }

        private void PathTextEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
                if (f.SelectedPath != "")
                    PathTextEdit.EditValue = PathTextEdit.Text = f.SelectedPath;
        }


    }

    public class TypeMapping
    {

        public string FileName { get; set; }
        public int TypeId { get; set; }
        public bool isPicture { get; set; }

        public static List<TypeMapping> getList()
        {
            XDocument doc = XDocument.Load(Program.configPath);
            return doc.Root.Element("DocumentTypes")
            .Elements("Type")
            .Select(type => new TypeMapping
            {
                FileName = type.Element("FileName").Value,
                TypeId = Convert.ToInt32(type.Element("TypeId").Value),
                isPicture = Convert.ToBoolean(type.Element("isPicture").Value)
            })
            .ToList();
        }
        public static bool DocIsPicture(int docType)
        {
            List<TypeMapping> mappings = getList();
            TypeMapping mapping = mappings.FirstOrDefault(m => m.TypeId == docType);
            return mapping?.isPicture ?? false;
        }
    }
}