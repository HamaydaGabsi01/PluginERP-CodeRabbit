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
using System.Threading;
using DevExpress.XtraReports.UI;
using System.IO;
using AtooERP_Booking.Communication.Admission;

namespace ENVOI_READ
{
    public partial class Admission_generate : AtooERP_Booking.Communication.Procedure_final_departure.Procedure_final_departure_generate_type
    {
        public Admission_generate()
        {
            InitializeComponent();
        }
        public Admission_generate(ArrayList rows) :base(rows)
        {
            InitializeComponent();
            booking_seasonCollectionTableAdapter1.Fill(dataSetBooking1.booking_seasonCollection);
            this.seasonLookUpEdit.EditValue = dataSetBooking1.booking_seasonCollection.Max(row => row.Id);
            this.rows = rows;
        }

        public override void validate()
        {
            DialogResult result = MessageBox.Show("Voulez-vous " +
                  radioGroup1.Properties.Items[radioGroup1.SelectedIndex].Description +
                  "\nles re-admission pour les résidents séléctionnés?", "Re-Admission",
                       MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                //Generate();

                if (Convert.ToInt16(radioGroup1.EditValue) == 1 || Convert.ToInt16(radioGroup1.EditValue) == 3)
                {
                    PrintDialog dialog = new PrintDialog();
                    printer = dialog.PrinterSettings.PrinterName;
                }
                Thread thread = new Thread(Generate);
                thread.Start();
                this.Close();
            }
        }
        public override void Generate()
        {
            Booking.Season Season = new Booking.Season(Convert.ToInt32(seasonLookUpEdit.EditValue));
            for (int i = 0; i < rows.Count; i++)
            {
                DataRow row = rows[i] as DataRow;
                int IdReservation = Convert.ToInt32(row["IdReservation"]);
                Booking.Admission Admission = new Booking.Admission(new Booking.Reservation(IdReservation), Season);
                Admission.validate();
                switch (Convert.ToInt16(radioGroup1.EditValue))
                {
                    case 1:
                        if (Admission.attachment)
                        {
                            Admission_report Report = new Admission_report(Admission);
                            Report.ShowPrintStatusDialog = false;
                            Report.ShowPrintMarginsWarning = false;
                            Report.Print(printer);
                        }
                        
                        break;
                    case 2:
                        if (Admission.attachment)
                        {
                            Admission_report Report2 = new Admission_report(Admission);
                            string path = Path.GetTempFileName() + ".pdf";
                            Report2.ExportToPdf(path);
                            Admission.sendLicenceViaMail(path);
                        }
                        else
                            Admission.sendLicenceViaMail(null);
                       

                        break;
                    case 3:
                        if (Admission.attachment)
                        {
                            Admission_report Report3 = new Admission_report(Admission);
                            Report3.ShowPrintStatusDialog = false;
                            Report3.ShowPrintMarginsWarning = false;
                            Report3.Print(printer);
                        
                        string path3 = Path.GetTempFileName() + ".pdf";
                        Report3.ExportToPdf(path3);
                        Admission.sendLicenceViaMail(path3);
                        }
                        else
                            Admission.sendLicenceViaMail(null);
                        break;
                    default:
                        break;
                }
            }
        }

    }
}