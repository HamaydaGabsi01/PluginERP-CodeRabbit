using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars.Ribbon;

namespace SAM_TRANSFER
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(DevExpress.XtraBars.Ribbon.RibbonForm MdiParent)
        {
            Application.EnableVisualStyles();
            //Properties.Settings.Default["atooerpConnectionString"] = AtooERP.Network_setting.getConnectionString().Replace(";connectiontimeout=20000;connectionlifetime=20000;defaultcommandtimeout=20000;persistsecurityinfo=True", string.Empty);
            SAM_TRANSFERForm form = new SAM_TRANSFERForm();
            if (form.IsDisposed)
                return;
            form.MdiParent = MdiParent;
            form.Show();
        }
        /*static void Main()
        {
            //configPath = @"./SAM_API.config.xml";
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Properties.Settings.Default["atooerpConnectionString"] = AtooERP.Network_setting.getConnectionString().Replace("database=atooerp", "database=db_ragheb").Replace(";Connect Timeout=300;connectionlifetime=0;defaultcommandtimeout=300", string.Empty).Replace(";connectiontimeout=20000;connectionlifetime=20000;defaultcommandtimeout=20000;persistsecurityinfo=True", string.Empty);
            //AtooERP.Network_setting.setConnectionString(Properties.Settings.Default["atooerpConnectionString"].ToString());
            Application.Run(new SAM_TRANSFERForm());
        }*/

    }
}
