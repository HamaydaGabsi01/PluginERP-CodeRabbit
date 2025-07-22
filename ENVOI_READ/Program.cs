using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraBars.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ENVOI_READ
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(RibbonForm MdiParent)
        {
            Application.EnableVisualStyles();
            //Properties.Settings.Default["atooerpConnectionString"] = AtooERP.Network_setting.getConnectionString().Replace(";connectiontimeout=20000;connectionlifetime=20000;defaultcommandtimeout=20000;persistsecurityinfo=True", string.Empty);
            Admission form = new Admission();
            if (form.IsDisposed)
                return;
            form.MdiParent = MdiParent;
            form.Show();
        }
    }
}
