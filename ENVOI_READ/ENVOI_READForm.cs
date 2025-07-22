using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ENVOI_READ
{
    public partial class ENVOI_READForm : AtooERP_Booking.State.Admission
    {
        public ENVOI_READForm()
        {
            InitializeComponent();
            
        }

        public override void setInformation()
        {
            base.setInformation();
            
            // Add filter for colDD is false
            gridView.ActiveFilterString = "([DD] = False) AND ([DE] IS NULL)";
        }
    }
}
