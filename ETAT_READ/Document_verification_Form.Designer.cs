
namespace ETAT_READ
{
    partial class Document_verification_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Document_verification_Form));
            this.DocumentsListGridControl = new DevExpress.XtraGrid.GridControl();
            this.DocumentsListGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReservationId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colapplicationId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colprofileId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfirstName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.collastName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colroom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colbeginDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colendDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coltype = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcontact = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colstate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcheckInDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcheckOutDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcreate_date = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colextension = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coltype_document = new DevExpress.XtraGrid.Columns.GridColumn();
            this.documentTypesRepositoryItemLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colpath = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfileName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsize = new DevExpress.XtraGrid.Columns.GridColumn();
            this.documentInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.OpenDocumentSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.ImportSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.DocumentTypebindingSource = new System.Windows.Forms.BindingSource(this.components);
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
            ((System.ComponentModel.ISupportInitialize)(this.DocumentsListGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentsListGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentTypesRepositoryItemLookUpEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentTypebindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // Print_SimpleButton
            // 
            this.Print_SimpleButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Print_SimpleButton.ImageOptions.Image")));
            this.Print_SimpleButton.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.Print_SimpleButton.Location = new System.Drawing.Point(640, 0);
            // 
            // splitContainerControl2
            // 
            // 
            // splitContainerControl2.Panel1
            // 
            this.splitContainerControl2.Panel1.Controls.Add(this.layoutControl1);
            this.splitContainerControl2.Size = new System.Drawing.Size(790, 381);
            // 
            // splitContainerControl
            // 
            this.splitContainerControl.Size = new System.Drawing.Size(790, 391);
            // 
            // DocumentsListGridControl
            // 
            this.DocumentsListGridControl.Location = new System.Drawing.Point(2, 2);
            this.DocumentsListGridControl.MainView = this.DocumentsListGridView;
            this.DocumentsListGridControl.Name = "DocumentsListGridControl";
            this.DocumentsListGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.documentTypesRepositoryItemLookUpEdit});
            this.DocumentsListGridControl.Size = new System.Drawing.Size(664, 313);
            this.DocumentsListGridControl.TabIndex = 5;
            this.DocumentsListGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.DocumentsListGridView});
            // 
            // DocumentsListGridView
            // 
            this.DocumentsListGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colReservationId,
            this.colapplicationId,
            this.colprofileId,
            this.colfirstName,
            this.collastName,
            this.colroom,
            this.colbeginDate,
            this.colendDate,
            this.coltype,
            this.colcontact,
            this.colstate,
            this.colcheckInDate,
            this.colcheckOutDate,
            this.colname,
            this.colcreate_date,
            this.colextension,
            this.coltype_document,
            this.colpath,
            this.colfileName,
            this.colsize});
            this.DocumentsListGridView.DetailHeight = 284;
            this.DocumentsListGridView.GridControl = this.DocumentsListGridControl;
            this.DocumentsListGridView.Name = "DocumentsListGridView";
            this.DocumentsListGridView.OptionsSelection.MultiSelect = true;
            this.DocumentsListGridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.DocumentsListGridView.OptionsView.AutoCalcPreviewLineCount = true;
            this.DocumentsListGridView.OptionsView.RowAutoHeight = true;
            this.DocumentsListGridView.OptionsView.ShowAutoFilterRow = true;
            this.DocumentsListGridView.OptionsView.ShowFooter = true;
            this.DocumentsListGridView.DoubleClick += new System.EventHandler(this.DocumentsListGridView_DoubleClick);
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            this.colId.OptionsColumn.AllowEdit = false;
            // 
            // colReservationId
            // 
            this.colReservationId.Caption = "ReservationId";
            this.colReservationId.FieldName = "reservationId";
            this.colReservationId.Name = "colReservationId";
            this.colReservationId.OptionsColumn.AllowEdit = false;
            this.colReservationId.Visible = true;
            this.colReservationId.VisibleIndex = 8;
            // 
            // colapplicationId
            // 
            this.colapplicationId.FieldName = "applicationId";
            this.colapplicationId.Name = "colapplicationId";
            this.colapplicationId.OptionsColumn.AllowEdit = false;
            this.colapplicationId.Visible = true;
            this.colapplicationId.VisibleIndex = 6;
            // 
            // colprofileId
            // 
            this.colprofileId.FieldName = "profileId";
            this.colprofileId.Name = "colprofileId";
            this.colprofileId.OptionsColumn.AllowEdit = false;
            this.colprofileId.Visible = true;
            this.colprofileId.VisibleIndex = 7;
            // 
            // colfirstName
            // 
            this.colfirstName.FieldName = "firstName";
            this.colfirstName.Name = "colfirstName";
            this.colfirstName.OptionsColumn.AllowEdit = false;
            this.colfirstName.Visible = true;
            this.colfirstName.VisibleIndex = 3;
            // 
            // collastName
            // 
            this.collastName.FieldName = "lastName";
            this.collastName.Name = "collastName";
            this.collastName.OptionsColumn.AllowEdit = false;
            this.collastName.Visible = true;
            this.collastName.VisibleIndex = 4;
            // 
            // colroom
            // 
            this.colroom.FieldName = "room";
            this.colroom.Name = "colroom";
            this.colroom.OptionsColumn.AllowEdit = false;
            this.colroom.Visible = true;
            this.colroom.VisibleIndex = 9;
            // 
            // colbeginDate
            // 
            this.colbeginDate.FieldName = "beginDate";
            this.colbeginDate.Name = "colbeginDate";
            this.colbeginDate.OptionsColumn.AllowEdit = false;
            this.colbeginDate.Visible = true;
            this.colbeginDate.VisibleIndex = 10;
            // 
            // colendDate
            // 
            this.colendDate.FieldName = "endDate";
            this.colendDate.Name = "colendDate";
            this.colendDate.OptionsColumn.AllowEdit = false;
            this.colendDate.Visible = true;
            this.colendDate.VisibleIndex = 11;
            // 
            // coltype
            // 
            this.coltype.FieldName = "type";
            this.coltype.Name = "coltype";
            this.coltype.OptionsColumn.AllowEdit = false;
            this.coltype.Visible = true;
            this.coltype.VisibleIndex = 12;
            // 
            // colcontact
            // 
            this.colcontact.FieldName = "contact";
            this.colcontact.Name = "colcontact";
            this.colcontact.OptionsColumn.AllowEdit = false;
            this.colcontact.Visible = true;
            this.colcontact.VisibleIndex = 13;
            // 
            // colstate
            // 
            this.colstate.FieldName = "state";
            this.colstate.Name = "colstate";
            this.colstate.OptionsColumn.AllowEdit = false;
            this.colstate.Visible = true;
            this.colstate.VisibleIndex = 14;
            // 
            // colcheckInDate
            // 
            this.colcheckInDate.FieldName = "checkInDate";
            this.colcheckInDate.Name = "colcheckInDate";
            this.colcheckInDate.OptionsColumn.AllowEdit = false;
            this.colcheckInDate.Visible = true;
            this.colcheckInDate.VisibleIndex = 16;
            // 
            // colcheckOutDate
            // 
            this.colcheckOutDate.FieldName = "checkOutDate";
            this.colcheckOutDate.Name = "colcheckOutDate";
            this.colcheckOutDate.OptionsColumn.AllowEdit = false;
            this.colcheckOutDate.Visible = true;
            this.colcheckOutDate.VisibleIndex = 17;
            // 
            // colname
            // 
            this.colname.FieldName = "name";
            this.colname.Name = "colname";
            this.colname.OptionsColumn.AllowEdit = false;
            this.colname.Visible = true;
            this.colname.VisibleIndex = 5;
            // 
            // colcreate_date
            // 
            this.colcreate_date.FieldName = "create_date";
            this.colcreate_date.Name = "colcreate_date";
            this.colcreate_date.OptionsColumn.AllowEdit = false;
            this.colcreate_date.Visible = true;
            this.colcreate_date.VisibleIndex = 2;
            // 
            // colextension
            // 
            this.colextension.FieldName = "extension";
            this.colextension.Name = "colextension";
            this.colextension.OptionsColumn.AllowEdit = false;
            this.colextension.Visible = true;
            this.colextension.VisibleIndex = 15;
            // 
            // coltype_document
            // 
            this.coltype_document.ColumnEdit = this.documentTypesRepositoryItemLookUpEdit;
            this.coltype_document.FieldName = "type_document";
            this.coltype_document.Name = "coltype_document";
            this.coltype_document.Visible = true;
            this.coltype_document.VisibleIndex = 1;
            // 
            // documentTypesRepositoryItemLookUpEdit
            // 
            this.documentTypesRepositoryItemLookUpEdit.AutoHeight = false;
            this.documentTypesRepositoryItemLookUpEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.documentTypesRepositoryItemLookUpEdit.DisplayMember = "name";
            this.documentTypesRepositoryItemLookUpEdit.Name = "documentTypesRepositoryItemLookUpEdit";
            this.documentTypesRepositoryItemLookUpEdit.ValueMember = "Id";
            // 
            // colpath
            // 
            this.colpath.FieldName = "path";
            this.colpath.Name = "colpath";
            this.colpath.OptionsColumn.AllowEdit = false;
            // 
            // colfileName
            // 
            this.colfileName.FieldName = "fileName";
            this.colfileName.Name = "colfileName";
            this.colfileName.OptionsColumn.AllowEdit = false;
            // 
            // colsize
            // 
            this.colsize.FieldName = "size";
            this.colsize.Name = "colsize";
            this.colsize.OptionsColumn.AllowEdit = false;
            this.colsize.Visible = true;
            this.colsize.VisibleIndex = 18;
            // 
            // documentInfoBindingSource
            // 
            this.documentInfoBindingSource.DataSource = typeof(SAM_DOC.Document_information);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.OpenDocumentSimpleButton);
            this.layoutControl1.Controls.Add(this.ImportSimpleButton);
            this.layoutControl1.Controls.Add(this.DocumentsListGridControl);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(790, 317);
            this.layoutControl1.TabIndex = 6;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // OpenDocumentSimpleButton
            // 
            this.OpenDocumentSimpleButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("OpenDocumentSimpleButton.ImageOptions.Image")));
            this.OpenDocumentSimpleButton.Location = new System.Drawing.Point(670, 2);
            this.OpenDocumentSimpleButton.Name = "OpenDocumentSimpleButton";
            this.OpenDocumentSimpleButton.Size = new System.Drawing.Size(118, 36);
            this.OpenDocumentSimpleButton.StyleController = this.layoutControl1;
            this.OpenDocumentSimpleButton.TabIndex = 10;
            this.OpenDocumentSimpleButton.Text = "Open";
            this.OpenDocumentSimpleButton.Click += new System.EventHandler(this.OpenDocumentSimpleButton_Click);
            // 
            // ImportSimpleButton
            // 
            this.ImportSimpleButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("ImportSimpleButton.ImageOptions.Image")));
            this.ImportSimpleButton.Location = new System.Drawing.Point(670, 42);
            this.ImportSimpleButton.Name = "ImportSimpleButton";
            this.ImportSimpleButton.Size = new System.Drawing.Size(118, 36);
            this.ImportSimpleButton.StyleController = this.layoutControl1;
            this.ImportSimpleButton.TabIndex = 9;
            this.ImportSimpleButton.Text = "Import Selected";
            this.ImportSimpleButton.Click += new System.EventHandler(this.ImportSimpleButton_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.False;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(790, 317);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.DocumentsListGridControl;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(668, 317);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.OpenDocumentSimpleButton;
            this.layoutControlItem2.Location = new System.Drawing.Point(668, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(122, 40);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.ImportSimpleButton;
            this.layoutControlItem3.Location = new System.Drawing.Point(668, 40);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(122, 277);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // Document_verification_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "Document_verification_Form";
            this.Text = "Document_verification_Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Document_verification_Form_FormClosing);
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
            ((System.ComponentModel.ISupportInitialize)(this.DocumentsListGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentsListGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentTypesRepositoryItemLookUpEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentTypebindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl DocumentsListGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView DocumentsListGridView;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colReservationId;
        private DevExpress.XtraGrid.Columns.GridColumn colapplicationId;
        private DevExpress.XtraGrid.Columns.GridColumn colprofileId;
        private DevExpress.XtraGrid.Columns.GridColumn colfirstName;
        private DevExpress.XtraGrid.Columns.GridColumn collastName;
        private DevExpress.XtraGrid.Columns.GridColumn colroom;
        private DevExpress.XtraGrid.Columns.GridColumn colbeginDate;
        private DevExpress.XtraGrid.Columns.GridColumn colendDate;
        private DevExpress.XtraGrid.Columns.GridColumn coltype;
        private DevExpress.XtraGrid.Columns.GridColumn colcontact;
        private DevExpress.XtraGrid.Columns.GridColumn colstate;
        private DevExpress.XtraGrid.Columns.GridColumn colcheckInDate;
        private DevExpress.XtraGrid.Columns.GridColumn colcheckOutDate;
        private DevExpress.XtraGrid.Columns.GridColumn colname;
        private DevExpress.XtraGrid.Columns.GridColumn colcreate_date;
        private DevExpress.XtraGrid.Columns.GridColumn colextension;
        private DevExpress.XtraGrid.Columns.GridColumn coltype_document;
        private DevExpress.XtraGrid.Columns.GridColumn colpath;
        private DevExpress.XtraGrid.Columns.GridColumn colfileName;
        private DevExpress.XtraGrid.Columns.GridColumn colsize;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit documentTypesRepositoryItemLookUpEdit;
        private System.Windows.Forms.BindingSource documentInfoBindingSource;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton OpenDocumentSimpleButton;
        private DevExpress.XtraEditors.SimpleButton ImportSimpleButton;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private System.Windows.Forms.BindingSource DocumentTypebindingSource;
    }
}