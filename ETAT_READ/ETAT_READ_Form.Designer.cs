namespace ETAT_READ
{
    partial class ETAT_READ_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ETAT_READ_Form));
            DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::ETAT_READ.SplashScreen1), true, true);
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bookingReadmissionDataSet1 = new ETAT_READ.BookingReadmissionDataSet();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOldReservation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGuest = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRoom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRoom2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNewReservation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colApplicationId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFirstName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLastName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheckInscription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContactValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheckBudget = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBeginDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEndDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCounteur = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colImpaye = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProfileId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colget_files = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colstatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldocs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPDD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.atooerp_personTableAdapter1 = new SAM_DOC.DataSetSAM_APITableAdapters.atooerp_personTableAdapter();
            this.EditConfigSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.splashScreenManager2 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::ETAT_READ.WaitForm1), true, true);
            this.RefreshSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.SendPropositionSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.SendDDSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.PVSimpleButton = new DevExpress.XtraEditors.SimpleButton();
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
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bookingReadmissionDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // Print_SimpleButton
            // 
            this.Print_SimpleButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Print_SimpleButton.ImageOptions.Image")));
            this.Print_SimpleButton.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.Print_SimpleButton.Location = new System.Drawing.Point(268, 0);
            this.Print_SimpleButton.Click += new System.EventHandler(this.Print_SimpleButton_Click);
            // 
            // splitContainerControl2
            // 
            // 
            // splitContainerControl2.Panel1
            // 
            this.splitContainerControl2.Panel1.Controls.Add(this.gridControl1);
            // 
            // splitContainerControl2.Panel2
            // 
            this.splitContainerControl2.Panel2.Controls.Add(this.PVSimpleButton);
            this.splitContainerControl2.Panel2.Controls.Add(this.SendDDSimpleButton);
            this.splitContainerControl2.Panel2.Controls.Add(this.SendPropositionSimpleButton);
            this.splitContainerControl2.Panel2.Controls.Add(this.RefreshSimpleButton);
            this.splitContainerControl2.Panel2.Controls.Add(this.EditConfigSimpleButton);
            this.splitContainerControl2.Size = new System.Drawing.Size(1091, 509);
            // 
            // splitContainerControl
            // 
            this.splitContainerControl.Size = new System.Drawing.Size(1091, 519);
            // 
            // splashScreenManager1
            // 
            splashScreenManager1.ClosingDelay = 500;
            // 
            // gridControl1
            // 
            this.gridControl1.DataMember = "BookingReadmission";
            this.gridControl1.DataSource = this.bookingReadmissionDataSet1;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1});
            this.gridControl1.Size = new System.Drawing.Size(1091, 445);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // bookingReadmissionDataSet1
            // 
            this.bookingReadmissionDataSet1.DataSetName = "NewDataSet";
            // 
            // gridView1
            // 
            this.gridView1.Appearance.ColumnFilterButton.Options.UseTextOptions = true;
            this.gridView1.Appearance.ColumnFilterButton.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colOldReservation,
            this.colGuest,
            this.colRoom,
            this.colRoom2,
            this.colNewReservation,
            this.colNote,
            this.colApplicationId,
            this.colFirstName,
            this.colLastName,
            this.colCheckInscription,
            this.colContactValue,
            this.colCheckBudget,
            this.colState,
            this.colBeginDate,
            this.colEndDate,
            this.colCounteur,
            this.colDD,
            this.colImpaye,
            this.colProfileId,
            this.colget_files,
            this.colstatus,
            this.coldocs,
            this.colPDD});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridView1_RowCellClick);
            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView1_RowCellStyle);
            this.gridView1.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridView1_CustomRowCellEdit);
            this.gridView1.CustomColumnSort += new DevExpress.XtraGrid.Views.Base.CustomColumnSortEventHandler(this.gridView1_CustomColumnSort);
            this.gridView1.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.gridView1_CustomUnboundColumnData);
            // 
            // colId
            // 
            this.colId.Caption = "ID";
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            this.colId.OptionsColumn.AllowEdit = false;
            this.colId.Visible = true;
            this.colId.VisibleIndex = 1;
            this.colId.Width = 37;
            // 
            // colOldReservation
            // 
            this.colOldReservation.Caption = "Anc. Rés.";
            this.colOldReservation.FieldName = "OldReservation";
            this.colOldReservation.Name = "colOldReservation";
            this.colOldReservation.OptionsColumn.AllowEdit = false;
            this.colOldReservation.Visible = true;
            this.colOldReservation.VisibleIndex = 2;
            this.colOldReservation.Width = 33;
            // 
            // colGuest
            // 
            this.colGuest.Caption = "Res.";
            this.colGuest.FieldName = "Guest";
            this.colGuest.Name = "colGuest";
            this.colGuest.OptionsColumn.AllowEdit = false;
            this.colGuest.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Guest", "{0}")});
            // 
            // colRoom
            // 
            this.colRoom.Caption = "Ch";
            this.colRoom.FieldName = "Room";
            this.colRoom.Name = "colRoom";
            this.colRoom.OptionsColumn.AllowEdit = false;
            this.colRoom.Visible = true;
            this.colRoom.VisibleIndex = 3;
            this.colRoom.Width = 70;
            // 
            // colRoom2
            // 
            this.colRoom2.Caption = "Nv Ch";
            this.colRoom2.FieldName = "Room2";
            this.colRoom2.Name = "colRoom2";
            this.colRoom2.OptionsColumn.AllowEdit = false;
            this.colRoom2.Visible = true;
            this.colRoom2.VisibleIndex = 17;
            this.colRoom2.Width = 68;
            // 
            // colNewReservation
            // 
            this.colNewReservation.Caption = "Nv. Rés.";
            this.colNewReservation.FieldName = "NewReservation";
            this.colNewReservation.Name = "colNewReservation";
            this.colNewReservation.OptionsColumn.AllowEdit = false;
            this.colNewReservation.Visible = true;
            this.colNewReservation.VisibleIndex = 13;
            this.colNewReservation.Width = 57;
            // 
            // colNote
            // 
            this.colNote.Caption = "Note";
            this.colNote.FieldName = "Note";
            this.colNote.Name = "colNote";
            this.colNote.OptionsColumn.AllowEdit = false;
            this.colNote.Visible = true;
            this.colNote.VisibleIndex = 21;
            this.colNote.Width = 211;
            // 
            // colApplicationId
            // 
            this.colApplicationId.Caption = "ID App";
            this.colApplicationId.FieldName = "ApplicationId";
            this.colApplicationId.Name = "colApplicationId";
            this.colApplicationId.OptionsColumn.AllowEdit = false;
            this.colApplicationId.Visible = true;
            this.colApplicationId.VisibleIndex = 18;
            this.colApplicationId.Width = 57;
            // 
            // colFirstName
            // 
            this.colFirstName.Caption = "Prénom";
            this.colFirstName.FieldName = "FirstName";
            this.colFirstName.Name = "colFirstName";
            this.colFirstName.OptionsColumn.ReadOnly = true;
            this.colFirstName.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "FirstName", "{0}")});
            this.colFirstName.Visible = true;
            this.colFirstName.VisibleIndex = 4;
            this.colFirstName.Width = 107;
            // 
            // colLastName
            // 
            this.colLastName.Caption = "Nom";
            this.colLastName.FieldName = "LastName";
            this.colLastName.Name = "colLastName";
            this.colLastName.OptionsColumn.ReadOnly = true;
            this.colLastName.Visible = true;
            this.colLastName.VisibleIndex = 5;
            this.colLastName.Width = 99;
            // 
            // colCheckInscription
            // 
            this.colCheckInscription.Caption = "Inscri";
            this.colCheckInscription.FieldName = "CheckInscription";
            this.colCheckInscription.Name = "colCheckInscription";
            this.colCheckInscription.OptionsColumn.AllowEdit = false;
            this.colCheckInscription.Visible = true;
            this.colCheckInscription.VisibleIndex = 10;
            this.colCheckInscription.Width = 40;
            // 
            // colContactValue
            // 
            this.colContactValue.Caption = "E-mail";
            this.colContactValue.FieldName = "ContactValue";
            this.colContactValue.Name = "colContactValue";
            this.colContactValue.OptionsColumn.ReadOnly = true;
            this.colContactValue.Visible = true;
            this.colContactValue.VisibleIndex = 6;
            this.colContactValue.Width = 178;
            // 
            // colCheckBudget
            // 
            this.colCheckBudget.Caption = "Budget";
            this.colCheckBudget.FieldName = "CheckBudget";
            this.colCheckBudget.Name = "colCheckBudget";
            this.colCheckBudget.OptionsColumn.AllowEdit = false;
            this.colCheckBudget.Visible = true;
            this.colCheckBudget.VisibleIndex = 11;
            this.colCheckBudget.Width = 45;
            // 
            // colState
            // 
            this.colState.Caption = "Etat";
            this.colState.DisplayFormat.FormatString = "f3";
            this.colState.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colState.FieldName = "State";
            this.colState.Name = "colState";
            this.colState.OptionsColumn.AllowEdit = false;
            this.colState.Visible = true;
            this.colState.VisibleIndex = 14;
            this.colState.Width = 57;
            // 
            // colBeginDate
            // 
            this.colBeginDate.Caption = "Date Début";
            this.colBeginDate.FieldName = "BeginDate";
            this.colBeginDate.Name = "colBeginDate";
            this.colBeginDate.OptionsColumn.AllowEdit = false;
            this.colBeginDate.Visible = true;
            this.colBeginDate.VisibleIndex = 15;
            this.colBeginDate.Width = 113;
            // 
            // colEndDate
            // 
            this.colEndDate.Caption = "Date Fin";
            this.colEndDate.FieldName = "EndDate";
            this.colEndDate.Name = "colEndDate";
            this.colEndDate.OptionsColumn.AllowEdit = false;
            this.colEndDate.Visible = true;
            this.colEndDate.VisibleIndex = 16;
            this.colEndDate.Width = 91;
            // 
            // colCounteur
            // 
            this.colCounteur.Caption = "Compteur";
            this.colCounteur.FieldName = "Counteur";
            this.colCounteur.Name = "colCounteur";
            this.colCounteur.OptionsColumn.AllowEdit = false;
            this.colCounteur.Visible = true;
            this.colCounteur.VisibleIndex = 12;
            this.colCounteur.Width = 53;
            // 
            // colDD
            // 
            this.colDD.Caption = "DD";
            this.colDD.FieldName = "DD";
            this.colDD.Name = "colDD";
            this.colDD.OptionsColumn.AllowEdit = false;
            this.colDD.Visible = true;
            this.colDD.VisibleIndex = 8;
            this.colDD.Width = 47;
            // 
            // colImpaye
            // 
            this.colImpaye.Caption = "Impayé";
            this.colImpaye.FieldName = "Impaye";
            this.colImpaye.Name = "colImpaye";
            this.colImpaye.OptionsColumn.AllowEdit = false;
            this.colImpaye.Visible = true;
            this.colImpaye.VisibleIndex = 9;
            this.colImpaye.Width = 54;
            // 
            // colProfileId
            // 
            this.colProfileId.Caption = "ID Profil";
            this.colProfileId.FieldName = "ProfileId";
            this.colProfileId.Name = "colProfileId";
            this.colProfileId.OptionsColumn.AllowEdit = false;
            this.colProfileId.Width = 45;
            // 
            // colget_files
            // 
            this.colget_files.Caption = "Charger";
            this.colget_files.ColumnEdit = this.repositoryItemButtonEdit1;
            this.colget_files.Name = "colget_files";
            this.colget_files.Visible = true;
            this.colget_files.VisibleIndex = 22;
            this.colget_files.Width = 60;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.ReadOnly = true;
            this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repositoryItemButtonEdit1.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit1_ButtonPressed);
            this.repositoryItemButtonEdit1.Click += new System.EventHandler(this.repositoryItemButtonEdit1_Click);
            // 
            // colstatus
            // 
            this.colstatus.AppearanceCell.Options.UseTextOptions = true;
            this.colstatus.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colstatus.Caption = "SAM";
            this.colstatus.FieldName = "colstatus";
            this.colstatus.Name = "colstatus";
            this.colstatus.OptionsColumn.AllowEdit = false;
            this.colstatus.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colstatus.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
            this.colstatus.UnboundDataType = typeof(string);
            this.colstatus.Visible = true;
            this.colstatus.VisibleIndex = 19;
            // 
            // coldocs
            // 
            this.coldocs.Caption = "Docs";
            this.coldocs.FieldName = "coldocs";
            this.coldocs.Name = "coldocs";
            this.coldocs.OptionsColumn.AllowEdit = false;
            this.coldocs.UnboundDataType = typeof(string);
            this.coldocs.Visible = true;
            this.coldocs.VisibleIndex = 20;
            this.coldocs.Width = 67;
            // 
            // colPDD
            // 
            this.colPDD.Caption = "PDD";
            this.colPDD.FieldName = "PDD";
            this.colPDD.Name = "colPDD";
            this.colPDD.OptionsColumn.AllowEdit = false;
            this.colPDD.Visible = true;
            this.colPDD.VisibleIndex = 7;
            // 
            // atooerp_personTableAdapter1
            // 
            this.atooerp_personTableAdapter1.ClearBeforeFill = true;
            // 
            // EditConfigSimpleButton
            // 
            this.EditConfigSimpleButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.EditConfigSimpleButton.ImageOptions.Image = global::ETAT_READ.Properties.Resources.properties_32x32;
            this.EditConfigSimpleButton.Location = new System.Drawing.Point(960, 0);
            this.EditConfigSimpleButton.Name = "EditConfigSimpleButton";
            this.EditConfigSimpleButton.Size = new System.Drawing.Size(131, 54);
            this.EditConfigSimpleButton.TabIndex = 6;
            this.EditConfigSimpleButton.Text = "Edit Configuration";
            this.EditConfigSimpleButton.Click += new System.EventHandler(this.EditConfigSimpleButton_Click);
            // 
            // splashScreenManager2
            // 
            this.splashScreenManager2.ClosingDelay = 500;
            // 
            // RefreshSimpleButton
            // 
            this.RefreshSimpleButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.RefreshSimpleButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("RefreshSimpleButton.ImageOptions.Image")));
            this.RefreshSimpleButton.Location = new System.Drawing.Point(829, 0);
            this.RefreshSimpleButton.Name = "RefreshSimpleButton";
            this.RefreshSimpleButton.Size = new System.Drawing.Size(131, 54);
            this.RefreshSimpleButton.TabIndex = 7;
            this.RefreshSimpleButton.Text = "Recharger";
            this.RefreshSimpleButton.Click += new System.EventHandler(this.RefreshSimpleButton_Click);
            // 
            // SendPropositionSimpleButton
            // 
            this.SendPropositionSimpleButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.SendPropositionSimpleButton.ImageOptions.Image = global::ETAT_READ.Properties.Resources.mail_32x32;
            this.SendPropositionSimpleButton.Location = new System.Drawing.Point(676, 0);
            this.SendPropositionSimpleButton.Name = "SendPropositionSimpleButton";
            this.SendPropositionSimpleButton.Size = new System.Drawing.Size(153, 54);
            this.SendPropositionSimpleButton.TabIndex = 8;
            this.SendPropositionSimpleButton.Text = "Envoyer Proposition";
            this.SendPropositionSimpleButton.Click += new System.EventHandler(this.SendPropositionSimpleButton_Click);
            // 
            // SendDDSimpleButton
            // 
            this.SendDDSimpleButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.SendDDSimpleButton.ImageOptions.Image = global::ETAT_READ.Properties.Resources.walking_32x32;
            this.SendDDSimpleButton.Location = new System.Drawing.Point(523, 0);
            this.SendDDSimpleButton.Name = "SendDDSimpleButton";
            this.SendDDSimpleButton.Size = new System.Drawing.Size(153, 54);
            this.SendDDSimpleButton.TabIndex = 9;
            this.SendDDSimpleButton.Text = "Envoyer DD";
            this.SendDDSimpleButton.Click += new System.EventHandler(this.SendDDSimpleButton_Click);
            // 
            // PVSimpleButton
            // 
            this.PVSimpleButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.PVSimpleButton.ImageOptions.Image = global::ETAT_READ.Properties.Resources.boreport_32x32;
            this.PVSimpleButton.Location = new System.Drawing.Point(418, 0);
            this.PVSimpleButton.Name = "PVSimpleButton";
            this.PVSimpleButton.Size = new System.Drawing.Size(105, 54);
            this.PVSimpleButton.TabIndex = 10;
            this.PVSimpleButton.Text = "PV";
            this.PVSimpleButton.Click += new System.EventHandler(this.PVSimpleButton_Click);
            // 
            // ETAT_READ_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 578);
            this.Name = "ETAT_READ_Form";
            this.Text = "Form1";
            this.Activated += new System.EventHandler(this.ETAT_READ_Form_Activated);
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
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bookingReadmissionDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private BookingReadmissionDataSet bookingReadmissionDataSet1;
        private SAM_DOC.DataSetSAM_APITableAdapters.atooerp_personTableAdapter atooerp_personTableAdapter1;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colOldReservation;
        private DevExpress.XtraGrid.Columns.GridColumn colGuest;
        private DevExpress.XtraGrid.Columns.GridColumn colRoom;
        private DevExpress.XtraGrid.Columns.GridColumn colRoom2;
        private DevExpress.XtraGrid.Columns.GridColumn colNewReservation;
        private DevExpress.XtraGrid.Columns.GridColumn colNote;
        private DevExpress.XtraGrid.Columns.GridColumn colApplicationId;
        private DevExpress.XtraGrid.Columns.GridColumn colFirstName;
        private DevExpress.XtraGrid.Columns.GridColumn colLastName;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckInscription;
        private DevExpress.XtraGrid.Columns.GridColumn colContactValue;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckBudget;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        private DevExpress.XtraGrid.Columns.GridColumn colBeginDate;
        private DevExpress.XtraGrid.Columns.GridColumn colEndDate;
        private DevExpress.XtraGrid.Columns.GridColumn colCounteur;
        private DevExpress.XtraGrid.Columns.GridColumn colDD;
        private DevExpress.XtraGrid.Columns.GridColumn colImpaye;
        private DevExpress.XtraGrid.Columns.GridColumn colProfileId;
        private DevExpress.XtraGrid.Columns.GridColumn colget_files;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colstatus;
        private DevExpress.XtraEditors.SimpleButton EditConfigSimpleButton;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager2;
        private DevExpress.XtraGrid.Columns.GridColumn coldocs;
        private DevExpress.XtraEditors.SimpleButton RefreshSimpleButton;
        private DevExpress.XtraEditors.SimpleButton SendPropositionSimpleButton;
        private DevExpress.XtraEditors.SimpleButton SendDDSimpleButton;
        private DevExpress.XtraGrid.Columns.GridColumn colPDD;
        private DevExpress.XtraEditors.SimpleButton PVSimpleButton;
    }
}

