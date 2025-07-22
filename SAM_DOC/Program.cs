using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars.Ribbon;
using System.IO;

namespace SAM_DOC
{
    static class Program
    {
        internal static string configPath = Path.Combine(
        Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
        "SAM_DOC.config.xml"
);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(RibbonForm MdiParent)
        {
            Application.EnableVisualStyles();
            Properties.Settings.Default["atooerpConnectionString"] = AtooERP.Network_setting.getConnectionString().Replace(";connectiontimeout=20000;connectionlifetime=20000;defaultcommandtimeout=20000;persistsecurityinfo=True", string.Empty);
            SAM_APIForm form = new SAM_APIForm();
            if (form.IsDisposed)
                return;
            form.MdiParent = MdiParent;
            form.Show();

        }
        /*static void Main()
        {
            configPath = @"./SAM_DOC.config.xml";
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Properties.Settings.Default["atooerpConnectionString"] = AtooERP.Network_setting.getConnectionString().Replace("database=atooerp", "database=db_ragheb").Replace(";Connect Timeout=300;connectionlifetime=0;defaultcommandtimeout=300", string.Empty).Replace(";connectiontimeout=20000;connectionlifetime=20000;defaultcommandtimeout=20000;persistsecurityinfo=True",string.Empty);
            AtooERP.Network_setting.setConnectionString(Properties.Settings.Default["atooerpConnectionString"].ToString());
            Application.Run(new SAM_APIForm());

        }*/
    }
}
