using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadFiles;

namespace BaseModule
{
    public class LogMgr
    {
        private BaseLogSender _log_sender = null;
        private FtpRW _ftp_mgr = null;
        private WebPoster _web_poster = null;
        private readonly string delayed_log_file = "delayed_copylog.csv";

        public LogMgr()
        {
            _ftp_mgr = new FtpRW();
            _web_poster = new WebPoster();
            _log_sender = _web_poster;
        }
        public void change_settings()
        {
            if (Env.server_type == Env.SERVER_TYPE_FTP)
                _log_sender = _ftp_mgr;
            else if (Env.server_type == Env.SERVER_TYPE_WEB)
                _log_sender = _web_poster;
            else
                throw new Exception("Unknown server type.");
            _log_sender.change_settings();
        }
        private void append_reserved_log(LogEntry log)
        {
            File.AppendAllText(delayed_log_file, log.ToString() + "\r\n");
        }
        public void append_copy_log(string src_path, string dst_path)
        {
            LogEntry log = new LogEntry(src_path, dst_path);

            //DateTime last_modified = File.GetLastWriteTime(src_path);

            if (!_log_sender.upload_copylog(new List<LogEntry> { log }))
            {
                append_reserved_log(log);
            }
        }
        public void append_delayed_copy_logs()
        {
            if (!File.Exists(delayed_log_file))
                return;
            string[] lines = File.ReadAllLines(delayed_log_file);
            List<LogEntry> failed_logs = new List<LogEntry>();

            foreach (string line in lines)
            {
                string log_str = line;
                if (log_str.EndsWith("\n"))
                    log_str = log_str.Substring(0, log_str.Length - "\n".Length);
                LogEntry log = LogEntry.ParseString(log_str);
                if (log == null)
                    continue;

                if (!_log_sender.upload_copylog(new List<LogEntry> { log }))
                {
                    failed_logs.Add(log);
                    break;
                }
            }

            File.Delete(delayed_log_file);

            foreach (LogEntry log in failed_logs)
                append_reserved_log(log);
        }
    }
}
