using BaseModule;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UploadFiles
{
    class TaskTrayApplicationContext : ApplicationContext
    {
        NotifyIcon notifyIcon = new NotifyIcon();

        private frmConfig config_frm = new frmConfig();
        private Timer schedule_timer = new Timer();
        private Timer ani_timer = new Timer();

        private LogMgr logger = null;
        private Uploader uploader = null;
        private int icon_counter = 0;

        public TaskTrayApplicationContext()
        {
            MenuItem configMenuItem = new MenuItem("Configuration", new EventHandler(ShowConfig));
            MenuItem copyAllMenuItem = new MenuItem("CopyAll", new EventHandler(CopyAll));
            MenuItem exitMenuItem = new MenuItem("Exit", new EventHandler(Exit));

            notifyIcon.Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
            notifyIcon.DoubleClick += new EventHandler(ShowConfig);
            notifyIcon.ContextMenu = new ContextMenu(new MenuItem[] { configMenuItem, copyAllMenuItem, /*showLogMenuItem,*/ exitMenuItem });
            notifyIcon.Visible = true;

            get_user_info();

            Env.load_from_ini();

            if (Env.show_next_time || !Env.is_valid_ini())
                ShowConfig(null, EventArgs.Empty);

            logger = new LogMgr();
            uploader = new Uploader(logger);

            if (Env.is_valid_ini())
            {
                logger.change_settings();

                if (Env.copy_all_at_first)
                    uploader.backup(true, DateTime.Now);
            }

            schedule_timer.Tick += new EventHandler(Schedule_Tick);
            schedule_timer.Interval = 60 * 1000;
            schedule_timer.Enabled = true;
            schedule_timer.Start();

            ani_timer.Tick += new EventHandler(AniTimer_Tick);
            ani_timer.Interval = 500;
            ani_timer.Enabled = true;
            ani_timer.Start();
        }
        private void AniTimer_Tick(object sender, EventArgs e)
        {
            if (uploader.is_uploading_now())
            {
                icon_counter++;
                if (icon_counter == 2)
                    icon_counter = 0;
            }
            else
            {
                icon_counter = 0;
            }
            notifyIcon.Icon = (icon_counter == 0) ? Properties.Resources.up2 : Properties.Resources.up1;
        }

        private void Schedule_Tick(object sender, EventArgs e)
        {
            if (!Env.is_valid_ini())
                return;
            if (Env.schedule_time.Hour == DateTime.Now.Hour && Env.schedule_time.Minute == DateTime.Now.Minute)
            {
                uploader.backup(false, DateTime.Now);
            }
        }

        private void CopyAll(object sender, EventArgs e)
        {
            uploader.backup(true, DateTime.Now);
        }
        private void ShowConfig(object sender, EventArgs e)
        {
            try
            {
                if (config_frm.Visible)
                {
                    config_frm.Activate();
                    config_frm.Focus();
                }
                else
                {
                    if (config_frm.ShowDialog() == DialogResult.OK)
                    {
                        if (Env.is_valid_ini())
                        {
                            Env.save_into_ini();
                            logger.change_settings();
                        }
                    }
                    else
                    {
                        // restore.
                        Env.load_from_ini();
                    }
                }
            }
            catch (Exception exception)
            {
                Env.log_error($"Exception Error ({System.Reflection.MethodBase.GetCurrentMethod().Name}): {exception.Message}");
            }
        }
        private void Exit(object sender, EventArgs e)
        {
            // We must manually tidy up and remove the icon before we exit.
            // Otherwise it will be left behind until the user mouses over.
            notifyIcon.Visible = false;

            Environment.Exit(0);
        }
        private void get_user_info()
        {
            try
            {
                Env.windows_domain_name = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
                Env.windows_user_name = System.DirectoryServices.AccountManagement.UserPrincipal.Current.Name;
            }
            catch (Exception exception)
            {
                Env.log_error($"Exception Error ({System.Reflection.MethodBase.GetCurrentMethod().Name}): {exception.Message}");

                Env.windows_domain_name = System.Environment.UserDomainName;
                Env.windows_user_name = System.Environment.UserName;
            }
        }
    }
}
