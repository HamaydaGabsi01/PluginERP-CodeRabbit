namespace ENVOI_READ
{
    partial class Admission_generate
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
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetBooking1 = new Booking.DataSetBooking();
            this.booking_seasonCollectionTableAdapter1 = new Booking.DataSetBookingTableAdapters.booking_seasonCollectionTableAdapter();
            this.seasonLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetBooking2 = new Booking.DataSetBooking();
            this.booking_seasonCollectionTableAdapter2 = new Booking.DataSetBookingTableAdapters.booking_seasonCollectionTableAdapter();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetBooking1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seasonLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetBooking2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 56);
            this.layoutControlItem1.Size = new System.Drawing.Size(666, 111);
            this.layoutControlItem1.Text = "Choisir le type de la génération de la Re-Admission";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(292, 16);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup1.Size = new System.Drawing.Size(686, 214);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.seasonLookUpEdit);
            this.layoutControl1.Size = new System.Drawing.Size(686, 214);
            this.layoutControl1.Controls.SetChildIndex(this.radioGroup1, 0);
            this.layoutControl1.Controls.SetChildIndex(this.seasonLookUpEdit, 0);
            this.layoutControl1.Controls.SetChildIndex(this.validateSimpleButton, 0);
            // 
            // validateSimpleButton
            // 
            this.validateSimpleButton.Location = new System.Drawing.Point(346, 179);
            this.validateSimpleButton.Size = new System.Drawing.Size(328, 23);
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(12, 87);
            this.radioGroup1.Size = new System.Drawing.Size(662, 88);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem4.Size = new System.Drawing.Size(666, 30);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(292, 16);
            this.layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataMember = "booking_seasonCollection";
            this.bindingSource1.DataSource = this.dataSetBooking1;
            // 
            // dataSetBooking1
            // 
            this.dataSetBooking1.DataSetName = "DataSetBooking";
            this.dataSetBooking1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // booking_seasonCollectionTableAdapter1
            // 
            this.booking_seasonCollectionTableAdapter1.ClearBeforeFill = true;
            // 
            // seasonLookUpEdit
            // 
            this.seasonLookUpEdit.Location = new System.Drawing.Point(12, 12);
            this.seasonLookUpEdit.Name = "seasonLookUpEdit";
            this.seasonLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seasonLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Id", 34, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Nom", "Nom", 36, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DateDebut", "Date Debut", 73, DevExpress.Utils.FormatType.DateTime, "dd/MM/yyyy", false, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DateFin", "Date Fin", 57, DevExpress.Utils.FormatType.DateTime, "dd/MM/yyyy", false, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Type", "Type", 38, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("name", "name", 42, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Near)});
            this.seasonLookUpEdit.Properties.DataSource = this.bindingSource1;
            this.seasonLookUpEdit.Properties.DisplayMember = "Nom";
            this.seasonLookUpEdit.Properties.ShowHeader = false;
            this.seasonLookUpEdit.Properties.ValueMember = "Id";
            this.seasonLookUpEdit.Size = new System.Drawing.Size(662, 22);
            this.seasonLookUpEdit.StyleController = this.layoutControl1;
            this.seasonLookUpEdit.TabIndex = 8;
            // 
            // bindingSource2
            // 
            this.bindingSource2.DataMember = "booking_seasonCollection";
            this.bindingSource2.DataSource = this.dataSetBooking2;
            // 
            // dataSetBooking2
            // 
            this.dataSetBooking2.DataSetName = "DataSetBooking";
            this.dataSetBooking2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // booking_seasonCollectionTableAdapter2
            // 
            this.booking_seasonCollectionTableAdapter2.ClearBeforeFill = true;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.seasonLookUpEdit;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(666, 26);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // Admission_generate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 214);
            this.Name = "Admission_generate";
            this.Text = "Re-Admission";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetBooking1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seasonLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetBooking2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSource1;
        private Booking.DataSetBooking dataSetBooking1;
        private Booking.DataSetBookingTableAdapters.booking_seasonCollectionTableAdapter booking_seasonCollectionTableAdapter1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.LookUpEdit seasonLookUpEdit;
        private System.Windows.Forms.BindingSource bindingSource2;
        private Booking.DataSetBooking dataSetBooking2;
        private Booking.DataSetBookingTableAdapters.booking_seasonCollectionTableAdapter booking_seasonCollectionTableAdapter2;
    }
}