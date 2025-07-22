using AtooERP.Document;
using SAM_DOC;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAM_TRANSFER
{
    public partial class SAM_TRANSFERForm : AtooERP.deskTopModel.deskTopModelForm.deskTopModelFormState
    {
        public SAM_TRANSFERForm()
        {
            InitializeComponent();
            transfertSimpleButton.Visible = true;
            beginingDateEdit.DateTime = DateTime.Now.AddDays(-30) ;
            endDateEdit.DateTime = DateTime.Now;
            setInformation();
        }
        Booking.Reservation Reservation;
        public SAM_TRANSFERForm(Booking.Reservation Reservation)
        {
            InitializeComponent();
            updateReservationSimpleButton.Visible = true;
            this.Reservation = Reservation;
            beginingDateEdit.DateTime = DateTime.Now.AddDays(-30);
            endDateEdit.DateTime = DateTime.Now;
            setInformation();
        }
        public virtual void setInformation()
        {
            if (Convert.ToInt16(radioGroup1.EditValue) == 1) { 
            gridControl.DataSource = Booking.Reservation.Xml_file.getXml_fileList(beginingDateEdit.DateTime.Date, endDateEdit.DateTime.Date.AddDays(1).AddSeconds(-1));
            this.titre.Text = "Liste Fichier XML[" + beginingDateEdit.DateTime.Date.ToShortDateString() +
                "==>" + endDateEdit.DateTime.Date.ToShortDateString() + "]";
            }
            else
            {
                gridControl.DataSource = Booking.Reservation.Xml_file.getXml_fileList(IdApplicationTextEdit.Text);
                this.titre.Text = "Liste Fichier XML[IdApplication: " + IdApplicationTextEdit.Text + "]";
            }
            if (!(Reservation is null))
                if (Reservation.guest.HasValue)
                    this.titre.Text += " " + new Booking.Guest((int)Reservation.guest).full_name;
            gridView.BestFitColumns();
            
        }
        private void folderLocationSimpleButton_Click(object sender, EventArgs e)
        {
            AtooERP_Booking.Reservation.XML.Xml_location_update Form = new AtooERP_Booking.Reservation.XML.Xml_location_update(new Booking.Reservation.Xml_location());
            Form.ShowDialog();
        }

        private void ValidateSimpleButton_Click(object sender, EventArgs e)
        {
            setInformation();
        }

        private void Print_SimpleButton_Click(object sender, EventArgs e)
        {
            ShowGridPreview(gridControl);
          
            
        }

        private void transfertSimpleButton_Click(object sender, EventArgs e)
        {
            BindingList<Booking.Reservation.Xml_file> rows = new BindingList<Booking.Reservation.Xml_file>();
            rows.Add(gridView.GetFocusedRow() as Booking.Reservation.Xml_file);

            for (int i = 0; i < rows.Count; i++)
            {
                if (!Booking.Reservation.isExist(rows[i].application_id))
                {
                    Booking.Reservation Reservation = rows[i].setReservation();
                    ImportDocuments(Reservation);
                    if (Reservation != null)
                    if (i == rows.Count - 1)
                    {
                            if (Reservation != null)
                                new Thread(() => Reservation.setLogOnTransfert()).Start();
                           AtooERP_Booking.Reservation.Reservation_update FORM = new AtooERP_Booking.Reservation.Reservation_update(Reservation);
                        FORM.MdiParent = this.MdiParent;
                        FORM.Show();
                    }
                }
                else
                    MessageBox.Show("La Réservation de " + rows[i].fullName + " sous ID: " + rows[i].application_id + " existe déjà.",
                        "Réservation Existante", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radioGroup1_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt16(radioGroup1.EditValue) == 1)
            {
                beginingDateLayoutControlItem.Visibility = endDateLayoutControlItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                IdApplicationLayoutControlItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                beginingDateLayoutControlItem.Visibility = endDateLayoutControlItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                IdApplicationLayoutControlItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }
        private void updateReservationSimpleButton_Click(object sender, EventArgs e)
        {
            BindingList<Booking.Reservation.Xml_file> rows = new BindingList<Booking.Reservation.Xml_file>();
            
            rows.Add(gridView.GetFocusedRow() as Booking.Reservation.Xml_file);
            
            for (int i = 0; i < rows.Count; i++)
            {
                if (!Booking.Reservation.isExist(rows[i].application_id))
                {
                    Booking.Reservation Reservation = rows[i].updateReservation(this.Reservation);
                    if (Reservation != null)
                        new Thread(() => Reservation.setLogOnTransfert()).Start();
                    this.Close();
                }
                else
                    MessageBox.Show("La Réservation de " + rows[i].fullName + " sous ID: " + rows[i].application_id + " existe déjà.",
                        "Réservation Existante", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void setLogOnImport(int docId, string docName)

        {
            AtooERP.Log.setLog(GetType().FullName, docId, docName, "import", null);

            return;
        }
        internal async Task SetLogOnImportAsync(int docId, string docName)
        {
            await Task.Run(() => setLogOnImport(docId, docName));
        }
        ConcurrentQueue<Task> loggingTasks = new ConcurrentQueue<Task>();
        private async void ImportDocuments(Booking.Reservation Reservation)
        {
            var (documentsFolder, documentTypes) = SAM_DOC.SAM_APIForm.LoadConfig();
            if (Directory.Exists(documentsFolder))
            {
                var documents = SAM_DOC.Document_information.LoadDocumentsInfo(documentsFolder, documentTypes);
                documents = new BindingList<SAM_DOC.Document_information>(
    documents.Where(d => d.reservationId == Reservation.Id).ToList()
);
                if (documents.Count > 0)
                {
                    DialogResult rslt = MessageBox.Show("Documents related to this reservation have been located. Do you want to import them?", "Import Documents", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if(rslt == DialogResult.Yes)
                    {
                        await Task.Run(() =>
                        {
                            for (int row = 0; row < documents.Count; row++)
                            {
                                bool hasMultipleOfType = false;
                                Document_information file = documents[row];
                                for (int _row = 0; _row< documents.Count; _row ++)
                                {
                                    Document_information _file = documents[_row];
                                    if (_row == row)
                                        break;
                                    if (_file != null && _file.type_document == file.type_document && _file.applicationId == file.applicationId && _file.fileName != file.fileName)
                                    {
                                        hasMultipleOfType = true;
                                        break;
                                    }
                                }
                                if (file.reservationId == 0)
                                    continue;
                                int reservationId = file.reservationId;
                                BindingList<Document> DocumentList = AtooERP.Document.Document.getListByPiece_typeAndPiece("Booking.Reservation, Booking, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", reservationId);

                                bool exist = false;

                                foreach (AtooERP.Document.Document Doc in DocumentList)
                                {
                                    if (Doc.type_document == file.type_document && ((!hasMultipleOfType) || Doc.content == null))
                                    {
                                        
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
                                    new AtooERP.Document.Document(docName, DateTime.Now, null, null, DateTime.Now, null, file.extension, (uint)file.type_document, file.content, reservationId, "Booking.Reservation, Booking, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", DateTime.Now).insert();
                                    int docIdnew = Convert.ToInt32(new AtooERP.DataSetMoezTableAdapters.atooerp_documentTableAdapter().getLastId());
                                    loggingTasks.Enqueue(SetLogOnImportAsync(docIdnew, docName));
                                }
                                if (TypeMapping.DocIsPicture(file.type_document))
                                {
                                    try
                                    {
                                        new SAM_DOC.DataSetSAM_APITableAdapters.atooerp_personTableAdapter().UpdatePictureByReservation(file.content, file.reservationId);

                                    }
                                    catch { }
                                }
                                try
                                {
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
                            }

                        });
                    }
                }
            }
        }
    }
}
