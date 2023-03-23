using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UploadFiles
{
    class Env
    {
        public static string windows_user_name = "";
        public static string windows_domain_name = "";

        public static System.Object locker_log = new object();
        public static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static readonly string SERVER_TYPE_FTP = "ftp";
        public static readonly string SERVER_TYPE_WEB = "web";
        public static readonly string SERVER_TYPE_MYSQL = "mysql";

        static public string copy_src_root = "";
        static public string copy_dst_root = "";
        static public string server_type = SERVER_TYPE_FTP;
        static public bool ftp_passive_mode = false;
        static public string server_url = "";
        static public int server_port = 21;
        static public string server_user_name = "";
        static public string server_password = "";
        static public DateTime schedule_time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
        static public bool copy_all_at_first = false;
        static public bool show_next_time = true;

        public static void log(string msg, string logtype, bool msgbox = false)
        {
            lock (locker_log)
            {
                try
                {
                    if (logtype == "error")
                        logger.Error(msg);
                    else
                        logger.Info(msg);

                    if (msgbox)
                        MessageBox.Show(msg);

                    msg = DateTime.Now.ToString("dd.MM.yyyy_hh:mm:ss ") + msg;
                    //g_main_frm.log(msg, logtype);
                    Console.WriteLine(msg);
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Exception Error ({System.Reflection.MethodBase.GetCurrentMethod().Name}): {exception.Message}");
                }
            }
        }
        public static void log_info(string msg, bool msgbox = false)
        {
            log(msg, "info", msgbox);
        }

        public static void log_error(string msg, bool msgbox = false)
        {
            log(msg, "error", msgbox);
        }
        public static void log_todo(string msg)
        {
            log(msg, "todo", false);
        }
        static public bool load_from_ini()
        {
            string ini_text = "";
            var parser = new FileIniDataParser();
            IniData data = null;

            try
            {
                data = parser.ReadFile("Configuration.ini");
            }
            catch (Exception exception)
            {
                log_error($"Error in read_from_ini : {exception.Message}");
                return false;
            }

            try
            {
                ini_text = data["Main"]["Passive_Mode"];
                ftp_passive_mode = bool.Parse(ini_text);
            }
            catch (Exception exception)
            {
                log_error($"Invalid passive mode. ({ini_text}) : {exception.Message}");
                return false;
            }
            try
            {
                ini_text = data["Main"]["COPY_SRC"];
                copy_src_root = ini_text;
            }
            catch (Exception exception)
            {
                log_error($"Invalid source root path. ({ini_text}) : {exception.Message}");
                return false;
            }
            try
            {
                ini_text = data["Main"]["COPY_DST"];
                copy_dst_root = ini_text;
            }
            catch (Exception exception)
            {
                log_error($"Invalid destination root path. ({ini_text}) : {exception.Message}");
                return false;
            }
            try
            {
                ini_text = data["Main"]["SERVER_TYPE"];
                server_type = ini_text;
                if (server_type != SERVER_TYPE_FTP && server_type != SERVER_TYPE_WEB && server_type != SERVER_TYPE_MYSQL)
                {
                    log_error($"Invalid server type. ({ini_text})");
                    return false;
                }
            }
            catch (Exception exception)
            {
                log_error($"Invalid server type. ({ini_text}) : {exception.Message}");
                return false;
            }
            try
            {
                ini_text = data["Main"]["SERVER_URL"];
                server_url = ini_text;
            }
            catch (Exception exception)
            {
                log_error($"Invalid server URL. ({ini_text}) : {exception.Message}");
                return false;
            }
            try
            {
                ini_text = data["Main"]["SERVER_PORT"];
                server_port = int.Parse(ini_text);
            }
            catch (Exception exception)
            {
                log_error($"Invalid server port. ({ini_text}) : {exception.Message}");
                return false;
            }
            try
            {
                ini_text = data["Main"]["SERVER_USER_NAME"];
                server_user_name = ini_text;
            }
            catch (Exception exception)
            {
                log_error($"Invalid server user name. ({ini_text}) : {exception.Message}");
                return false;
            }
            try
            {
                ini_text = data["Main"]["SERVER_PASSWORD"];
                server_password = ini_text;
            }
            catch (Exception exception)
            {
                log_error($"Invalid server password. ({ini_text}) : {exception.Message}");
                return false;
            }
            try
            {
                ini_text = data["Main"]["SCHEDULE_TIME"];
                schedule_time = DateTime.ParseExact(ini_text, "HH:mm", CultureInfo.InvariantCulture);
            }
            catch (Exception exception)
            {
                log_error($"Invalid schedule time. ({ini_text}) : {exception.Message}");
                return false;
            }
            try
            {
                ini_text = data["Main"]["COPY_ALL_AT_FIRST"];
                copy_all_at_first = bool.Parse(ini_text);
            }
            catch (Exception exception)
            {
                log_error($"Invalid COPY_ALL_AT_FIRST. ({ini_text}) : {exception.Message}");
                return false;
            }
            try
            {
                ini_text = data["Main"]["SHOW_NEXT_TIME"];
                show_next_time = bool.Parse(ini_text);
            }
            catch (Exception exception)
            {
                log_error($"Invalid SHOW_NEXT_TIME. ({ini_text}) : {exception.Message}");
                return false;
            }
            return true;
        }
        static public bool save_into_ini()
        {
            IniData data = null;

            try
            {
                var parser = new FileIniDataParser();

                try
                {
                    data = parser.ReadFile("Configuration.ini");
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Exception Error ({System.Reflection.MethodBase.GetCurrentMethod().Name}): {exception.Message}");
                    data = new IniData();
                }

                data["Main"]["Passive_Mode"] = ftp_passive_mode.ToString();
                data["Main"]["COPY_SRC"] = copy_src_root;
                data["Main"]["COPY_DST"] = copy_dst_root;
                data["Main"]["SERVER_TYPE"] = server_type;
                data["Main"]["SERVER_URL"] = server_url;
                data["Main"]["SERVER_PORT"] = server_port.ToString();
                data["Main"]["SERVER_USER_NAME"] = server_user_name;
                data["Main"]["SERVER_PASSWORD"] = server_password;
                data["Main"]["SCHEDULE_TIME"] = schedule_time.ToString("HH:mm");
                data["Main"]["COPY_ALL_AT_FIRST"] = copy_all_at_first.ToString();
                data["Main"]["SHOW_NEXT_TIME"] = show_next_time.ToString();

                parser.WriteFile("Configuration.ini", data);
            }
            catch (Exception exception)
            {
                log_error($"Invalid save ini. : {exception.Message}");
                return false;
            }
            return true;
        }
        static public bool is_valid_ini()
        {
            return (copy_src_root != "" && copy_dst_root != "");
        }
    }
}
