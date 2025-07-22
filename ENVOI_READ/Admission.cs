using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;

namespace ENVOI_READ
{
    public partial class Admission : AtooERP.deskTopModel.deskTopModelForm.deskTopModelFormState
    {
        public Admission()
        {
            InitializeComponent();
            this.titre.Text = "Etat Admission";
            this.Text = "Etat Admission";
            this.seasonLookUpEdit.EditValue = Booking.Season.getActualSeason();
            this.reservationTypeLookUpEdit.EditValue = 1;
            booking_seasonCollectionTableAdapter1.Fill(dataSetBooking1.booking_seasonCollection);
            booking_reservation_typeCollectionTableAdapter1.Fill(dataSetBooking1.booking_reservation_typeCollection);
            gridView.ActiveFilterString = "([DD] Is Null) And ([DE] Is Null Or [DE] = '')  And ([Compteur/Mois] > 3) And ([Date CheckIn] Is Not Null) And ([Conf Départ] Is Null)";
            gridView.BestFitColumns();
        }
        public virtual void setInformation()
        {
            if (checkEdit1.Checked)
            {
                if(presentCheckEdit.Checked)
                    reservationAdmissionTableAdapter1.FillPresentBySeasonAndReservationType(dataSetBookingState1.ReservationAdmission, Convert.ToInt32(seasonLookUpEdit.EditValue), Convert.ToInt32(reservationTypeLookUpEdit.EditValue));
                else
                reservationAdmissionTableAdapter1.FillBySeasonAndReservationType(dataSetBookingState1.ReservationAdmission, Convert.ToInt32(seasonLookUpEdit.EditValue), Convert.ToInt32(reservationTypeLookUpEdit.EditValue));
                this.titre.Text = "Etat Admission[" + seasonLookUpEdit.Text + " && " + reservationTypeLookUpEdit.Text + "]";
            }
            else
            {
                reservationAdmissionTableAdapter1.FillBySeason(dataSetBookingState1.ReservationAdmission, Convert.ToInt32(seasonLookUpEdit.EditValue));
                this.titre.Text = "Etat Admission[" + seasonLookUpEdit.Text + "]";

            }
            
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked)
            {
                reservationTypeLookUpEdit.Enabled = true;
                this.reservationTypeLookUpEdit.EditValue = 1;
            }
            else
            {
                reservationTypeLookUpEdit.Enabled = false;
                this.reservationTypeLookUpEdit.EditValue = null;
            }
        }

        private void ValidateSimpleButton_Click(object sender, EventArgs e)
        {
            setInformation();
        }

        private void Print_SimpleButton_Click(object sender, EventArgs e)
        {
            ShowGridPreview(gridControl);
        }

        private void Admission_Load(object sender, EventArgs e)
        {
            setInformation();
        }

        private void generateDepartureProcedureSimpleButton_Click(object sender, EventArgs e)
        {
            ArrayList rows = new ArrayList();

            // Add the selected rows to the list. 
            Int32[] selectedRowHandles = gridView.GetSelectedRows();
            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                int selectedRowHandle = selectedRowHandles[i];
                if (selectedRowHandle >= 0)
                    rows.Add(gridView.GetDataRow(selectedRowHandle));
            }
            AtooERP_Booking.Communication.Procedure_final_departure.Procedure_final_departure_generate_type Form =
                new AtooERP_Booking.Communication.Procedure_final_departure.Procedure_final_departure_generate_type(rows);
           // Form.MdiParent = this.MdiParent;
            Form.ShowDialog();

        }

        private void readmissionSimpleButton_Click(object sender, EventArgs e)
        {
            ArrayList rows = new ArrayList();

            // Add the selected rows to the list. 
            Int32[] selectedRowHandles = gridView.GetSelectedRows();
            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                int selectedRowHandle = selectedRowHandles[i];
                if (selectedRowHandle >= 0)
                    rows.Add(gridView.GetDataRow(selectedRowHandle));
            }
            Admission_generate Form =
                new Admission_generate(rows);
            // Form.MdiParent = this.MdiParent;
            Form.ShowDialog();
        }

        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            try
            {
                int Id = Convert.ToInt32(gridView.GetFocusedRowCellValue("IdReservation"));
                AtooERP_Booking.Reservation.Reservation_update FORM = new AtooERP_Booking.Reservation.Reservation_update(new Booking.Reservation(Id));
                bool trouve = false;
                for (int i = 0; i < this.MdiParent.MdiChildren.Length && trouve == false; i++)
                {
                    if (this.MdiParent.MdiChildren[i].GetType() == FORM.GetType() && this.MdiParent.MdiChildren[i].Text == FORM.Text)
                    {
                        this.MdiParent.MdiChildren[i].Focus();
                        trouve = true;
                    }
                }
                if (trouve == false)
                {
                    FORM.MdiParent = this.MdiParent;
                    FORM.Show();
                }
            }
            catch { }
            splashScreenManager1.CloseWaitForm();
        }

        private void departureDateConfirmationSimpleButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Voulez vous confirmer la date de départ définitive", "Date départ définitive",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                splashScreenManager1.ShowWaitForm();
                int[] selectedRows = gridView.GetSelectedRows();
                bool conf = false;
                for (int i = 0; i < gridView.SelectedRowsCount; i++)
                {
                    Booking.Reservation Reservation = new Booking.Reservation(Convert.ToInt32(Convert.ToInt32(gridView.GetRowCellValue(selectedRows[i], colIdReservation))));
                    Reservation.final_departure_date = Reservation.end_date;
                    Reservation.updateFinal_departure_date();
                    conf = true;
                }
                if (conf)
                {
                    MessageBox.Show("Date départ définitive enregistrée", "Date départ définitive", MessageBoxButtons.OK,
            MessageBoxIcon.Information);
                    setInformation();
                }
                splashScreenManager1.CloseWaitForm();
            }
        }
    }
}