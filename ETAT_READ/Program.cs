using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraBars.Ribbon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ETAT_READ
{
    static class Program
    {
        internal static string configPath = Path.Combine(
        Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
        "ETAT_READ.config.xml");
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(RibbonForm MdiParent)
        {
            Application.EnableVisualStyles();
            SAM_DOC.Properties.Settings.Default["atooerpConnectionString"] = Properties.Settings.Default["atooerpConnectionString"] = AtooERP.Network_setting.getConnectionString().Replace(";connectiontimeout=20000;connectionlifetime=20000;defaultcommandtimeout=20000;persistsecurityinfo=True", string.Empty);
            ETAT_READ_Form form = new ETAT_READ_Form();
            if (form.IsDisposed)
                return;
            form.MdiParent = MdiParent;
            form.Show();
        }

        /*static void Main()
        {
            AtooERP.Network_setting.setConnectionString();
            Booking.Network.setConnectionString();
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ETAT_READ_Form());

        }*/
    }
}
