using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace ETAT_READ
{
    public partial class PV_Readmission : DevExpress.XtraReports.UI.XtraReport
    {
        public PV_Readmission()
        {
            InitializeComponent();
            objectDataSource1.DataSource = new BookingReadmissionTableAdapter().GetData(26);
            InitializeConditionalFormatting();
            AddConditionalIcons();
            SetDataInHeader();
        }

        public void SetDataInHeader()
        {
            xrLabel1.Text = "Réunion du :" + DateTime.Now.ToString("d");
        }

        private void InitializeConditionalFormatting()
        {
            // Create formatting rules for different states

            // Rule for ADMISEBRASSEE - Blue background
            var ruleAdmiseBrassee = new FormattingRule();
            this.FormattingRuleSheet.Add(ruleAdmiseBrassee);
            ruleAdmiseBrassee.Name = "RuleAdmiseBrassee";
            ruleAdmiseBrassee.Condition = "[State] = 'ADMISE-BRASSEE'";
            ruleAdmiseBrassee.Formatting.BackColor = Color.LightBlue;
            this.Detail.FormattingRules.Add(ruleAdmiseBrassee);

            // Rule for EN ATTENTE - Yellow background
            var ruleEnAttente = new FormattingRule();
            this.FormattingRuleSheet.Add(ruleEnAttente);
            ruleEnAttente.DataSource = this.DataSource;
            ruleEnAttente.DataMember = this.DataMember;
            ruleEnAttente.Name = "RuleEnAttente";
            ruleEnAttente.Condition = "[State] = 'EN ATTENTE'";
            ruleEnAttente.Formatting.BackColor = Color.LightYellow;
            this.Detail.FormattingRules.Add(ruleEnAttente);

            // Rule for ADMISE - Green background
            var ruleAdmise = new FormattingRule();
            this.FormattingRuleSheet.Add(ruleAdmise);
            ruleAdmise.Name = "RuleAdmise";
            ruleAdmise.Condition = "[State] = 'ADMISE'";
            ruleAdmise.Formatting.BackColor = Color.LightGreen;
            this.Detail.FormattingRules.Add(ruleAdmise);

        }

        private void AddConditionalIcons()
        {
            // Configure Budget cell - text on left, icon on right
            this.tableCell34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            //this.tableCell34.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 25, 0, 0, 100F); // Right padding for icon space
            
            // Add picture box for Budget cell - positioned on the right
            var budgetPicture = new XRPictureBox();
            budgetPicture.Name = "budgetPicture";
            budgetPicture.AnchorHorizontal = DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right;
            //budgetPicture.LocationFloat = new DevExpress.Utils.PointFloat(this.tableCell34.WidthF - 22F, 4F); // Right side positioning
            //budgetPicture.SizeF = new System.Drawing.SizeF(16F, 16F);
            budgetPicture.Sizing = DevExpress.XtraPrinting.ImageSizeMode.AutoSize; // Better for maintaining aspect ratio
            budgetPicture.Borders = DevExpress.XtraPrinting.BorderSide.None;
            budgetPicture.BeforePrint += BudgetPicture_BeforePrint;
            
            this.tableCell34.Controls.Add(budgetPicture);
            
            // Configure Inscription cell - text on left, icon on right
            this.tableCell35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
           // this.tableCell35.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 25, 0, 0, 100F); // Right padding for icon space
            
            // Add picture box for Inscription cell - positioned on the right
            var inscriptionPicture = new XRPictureBox();
            inscriptionPicture.Name = "inscriptionPicture";
            inscriptionPicture.AnchorHorizontal = DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right;
            //inscriptionPicture.LocationFloat = new DevExpress.Utils.PointFloat(this.tableCell35.WidthF - 22F, 4F); // Right side positioning
            //inscriptionPicture.SizeF = new System.Drawing.SizeF(16F, 16F);
            inscriptionPicture.Sizing = DevExpress.XtraPrinting.ImageSizeMode.AutoSize; // Better for maintaining aspect ratio
            inscriptionPicture.Borders = DevExpress.XtraPrinting.BorderSide.None;
            inscriptionPicture.BeforePrint += InscriptionPicture_BeforePrint;
            
            this.tableCell35.Controls.Add(inscriptionPicture);
        }

        private void BudgetPicture_BeforePrint(object sender, CancelEventArgs e)
        {
            var pictureBox = sender as XRPictureBox;
            var report = pictureBox.Report;
            
            // Get the current row data
            var checkBudgetValue = report.GetCurrentColumnValue("CheckBudget")?.ToString();
            
            // Set the appropriate image based on the value
            if (checkBudgetValue == "Oui" || checkBudgetValue == "Yes" || checkBudgetValue == "1" || checkBudgetValue == "True")
            {
                pictureBox.ImageSource = new DevExpress.XtraPrinting.Drawing.ImageSource(Properties.Resources.checkmark);
            }
            else
            {
                pictureBox.ImageSource = new DevExpress.XtraPrinting.Drawing.ImageSource(Properties.Resources.cross);
            }
        }

        private void InscriptionPicture_BeforePrint(object sender, CancelEventArgs e)
        {
            var pictureBox = sender as XRPictureBox;
            var report = pictureBox.Report;
            
            // Get the current row data
            var checkInscriptionValue = report.GetCurrentColumnValue("CheckInscription")?.ToString();
            
            // Set the appropriate image based on the value
            if (checkInscriptionValue == "Oui" || checkInscriptionValue == "Yes" || checkInscriptionValue == "1" || checkInscriptionValue == "True")
            {
                pictureBox.ImageSource = new DevExpress.XtraPrinting.Drawing.ImageSource(Properties.Resources.checkmark);
            }
            else
            {
                pictureBox.ImageSource = new DevExpress.XtraPrinting.Drawing.ImageSource(Properties.Resources.cross);
            }
        }
    }
}
