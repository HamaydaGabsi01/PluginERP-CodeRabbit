
namespace SAM_DOC
{
    partial class ConfigurationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationForm));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.CancelSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.SaveSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.DocumentTypesGridControl = new DevExpress.XtraGrid.GridControl();
            this.TypeMappingBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DocumentTypesGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colFileName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTypeId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DocumentTypeRepositoryItemLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colsPicture = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PathTextEdit = new DevExpress.XtraEditors.ButtonEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.PathLayoutControlItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel1)).BeginInit();
            this.splitContainerControl2.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel2)).BeginInit();
            this.splitContainerControl2.Panel2.SuspendLayout();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl.Panel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl.Panel2)).BeginInit();
            this.splitContainerControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentTypesGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TypeMappingBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentTypesGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentTypeRepositoryItemLookUpEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PathTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PathLayoutControlItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // Print_SimpleButton
            // 
            this.Print_SimpleButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Print_SimpleButton.ImageOptions.Image")));
            this.Print_SimpleButton.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.Print_SimpleButton.Location = new System.Drawing.Point(1032, 0);
            this.Print_SimpleButton.Visible = false;
            // 
            // splitContainerControl2
            // 
            // 
            // splitContainerControl2.Panel1
            // 
            this.splitContainerControl2.Panel1.Controls.Add(this.layoutControl1);
            this.splitContainerControl2.Size = new System.Drawing.Size(1182, 589);
            // 
            // splitContainerControl
            // 
            this.splitContainerControl.Size = new System.Drawing.Size(1182, 599);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.CancelSimpleButton);
            this.layoutControl1.Controls.Add(this.SaveSimpleButton);
            this.layoutControl1.Controls.Add(this.DocumentTypesGridControl);
            this.layoutControl1.Controls.Add(this.PathTextEdit);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1182, 525);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // CancelSimpleButton
            // 
            this.CancelSimpleButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("CancelSimpleButton.ImageOptions.Image")));
            this.CancelSimpleButton.Location = new System.Drawing.Point(1056, 52);
            this.CancelSimpleButton.Name = "CancelSimpleButton";
            this.CancelSimpleButton.Size = new System.Drawing.Size(114, 36);
            this.CancelSimpleButton.StyleController = this.layoutControl1;
            this.CancelSimpleButton.TabIndex = 8;
            this.CancelSimpleButton.Text = "Cancel";
            this.CancelSimpleButton.Click += new System.EventHandler(this.CancelSimpleButton_Click);
            // 
            // SaveSimpleButton
            // 
            this.SaveSimpleButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("SaveSimpleButton.ImageOptions.Image")));
            this.SaveSimpleButton.Location = new System.Drawing.Point(1056, 12);
            this.SaveSimpleButton.Name = "SaveSimpleButton";
            this.SaveSimpleButton.Size = new System.Drawing.Size(114, 36);
            this.SaveSimpleButton.StyleController = this.layoutControl1;
            this.SaveSimpleButton.TabIndex = 7;
            this.SaveSimpleButton.Text = "Save";
            this.SaveSimpleButton.Click += new System.EventHandler(this.SaveSimpleButton_Click);
            // 
            // DocumentTypesGridControl
            // 
            this.DocumentTypesGridControl.DataSource = this.TypeMappingBindingSource;
            this.DocumentTypesGridControl.Location = new System.Drawing.Point(12, 36);
            this.DocumentTypesGridControl.MainView = this.DocumentTypesGridView;
            this.DocumentTypesGridControl.Name = "DocumentTypesGridControl";
            this.DocumentTypesGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.DocumentTypeRepositoryItemLookUpEdit});
            this.DocumentTypesGridControl.Size = new System.Drawing.Size(1040, 477);
            this.DocumentTypesGridControl.TabIndex = 4;
            this.DocumentTypesGridControl.UseEmbeddedNavigator = true;
            this.DocumentTypesGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.DocumentTypesGridView});
            // 
            // TypeMappingBindingSource
            // 
            this.TypeMappingBindingSource.DataSource = typeof(SAM_DOC.TypeMapping);
            // 
            // DocumentTypesGridView
            // 
            this.DocumentTypesGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colFileName,
            this.colTypeId,
            this.colsPicture});
            this.DocumentTypesGridView.GridControl = this.DocumentTypesGridControl;
            this.DocumentTypesGridView.Name = "DocumentTypesGridView";
            this.DocumentTypesGridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.DocumentTypesGridView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.DocumentTypesGridView.OptionsView.ShowAutoFilterRow = true;
            this.DocumentTypesGridView.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.DocumentTypesGridView_InitNewRow);
            // 
            // colFileName
            // 
            this.colFileName.FieldName = "FileName";
            this.colFileName.Name = "colFileName";
            this.colFileName.Visible = true;
            this.colFileName.VisibleIndex = 0;
            // 
            // colTypeId
            // 
            this.colTypeId.ColumnEdit = this.DocumentTypeRepositoryItemLookUpEdit;
            this.colTypeId.FieldName = "TypeId";
            this.colTypeId.Name = "colTypeId";
            this.colTypeId.Visible = true;
            this.colTypeId.VisibleIndex = 1;
            // 
            // DocumentTypeRepositoryItemLookUpEdit
            // 
            this.DocumentTypeRepositoryItemLookUpEdit.AutoHeight = false;
            this.DocumentTypeRepositoryItemLookUpEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DocumentTypeRepositoryItemLookUpEdit.Name = "DocumentTypeRepositoryItemLookUpEdit";
            this.DocumentTypeRepositoryItemLookUpEdit.ShowFooter = false;
            this.DocumentTypeRepositoryItemLookUpEdit.ShowHeader = false;
            // 
            // colsPicture
            // 
            this.colsPicture.Caption = "Is Picture";
            this.colsPicture.FieldName = "isPicture";
            this.colsPicture.Name = "colsPicture";
            this.colsPicture.Visible = true;
            this.colsPicture.VisibleIndex = 2;
            // 
            // PathTextEdit
            // 
            this.PathTextEdit.Location = new System.Drawing.Point(79, 12);
            this.PathTextEdit.Name = "PathTextEdit";
            this.PathTextEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.PathTextEdit.Size = new System.Drawing.Size(973, 20);
            this.PathTextEdit.StyleController = this.layoutControl1;
            this.PathTextEdit.TabIndex = 5;
            this.PathTextEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.PathTextEdit_ButtonClick);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.PathLayoutControlItem,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1182, 525);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.DocumentTypesGridControl;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1044, 481);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // PathLayoutControlItem
            // 
            this.PathLayoutControlItem.Control = this.PathTextEdit;
            this.PathLayoutControlItem.Location = new System.Drawing.Point(0, 0);
            this.PathLayoutControlItem.Name = "PathLayoutControlItem";
            this.PathLayoutControlItem.Size = new System.Drawing.Size(1044, 24);
            this.PathLayoutControlItem.Text = "Folder Path";
            this.PathLayoutControlItem.TextSize = new System.Drawing.Size(55, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.SaveSimpleButton;
            this.layoutControlItem4.Location = new System.Drawing.Point(1044, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(118, 40);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.CancelSimpleButton;
            this.layoutControlItem5.Location = new System.Drawing.Point(1044, 40);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(118, 465);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 658);
            this.Name = "ConfigurationForm";
            this.Text = "ConfigurationForm";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel1)).EndInit();
            this.splitContainerControl2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel2)).EndInit();
            this.splitContainerControl2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl.Panel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl.Panel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
            this.splitContainerControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DocumentTypesGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TypeMappingBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentTypesGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentTypeRepositoryItemLookUpEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PathTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PathLayoutControlItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl DocumentTypesGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView DocumentTypesGridView;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton CancelSimpleButton;
        private DevExpress.XtraEditors.SimpleButton SaveSimpleButton;
        private DevExpress.XtraLayout.LayoutControlItem PathLayoutControlItem;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private System.Windows.Forms.BindingSource TypeMappingBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colFileName;
        private DevExpress.XtraGrid.Columns.GridColumn colTypeId;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit DocumentTypeRepositoryItemLookUpEdit;
        private DevExpress.XtraEditors.ButtonEdit PathTextEdit;
        private DevExpress.XtraGrid.Columns.GridColumn colsPicture;
    }
}