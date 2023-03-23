using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UploadFiles;

namespace BaseModule
{
    public class Uploader
    {
        private readonly string delayed_backup_log_file = "delayed_backup.csv";

        private LogMgr _logger = null;
        private bool _is_uploading = false;
        private DateTime _modified_date;
        private Thread _upload_thread = null;
        private bool _must_stop = false;
        private bool _copy_all = false;

        public Uploader(LogMgr logger)
        {
            _logger = logger;
        }
        public void backup(bool copy_all, DateTime modified_date)
        {
            if (Env.copy_src_root == "" || Env.copy_dst_root == "")
                return;
            if (!Directory.Exists(Env.copy_src_root))
                return;

            if (_is_uploading)
                return;

            if (!Directory.Exists(Env.copy_dst_root))
            {
                try
                {
                    Directory.CreateDirectory(Env.copy_dst_root);
                }
                catch (Exception exception)
                {
                    Env.log_error($"Exception Error ({System.Reflection.MethodBase.GetCurrentMethod().Name}): {exception.Message}");
                    return;
                }
            }

            _modified_date = modified_date;
            _must_stop = false;
            _copy_all = copy_all;
            _upload_thread = new Thread(upload_thread_func);
            _upload_thread.Start();
        }
        public void exit()
        {
            if (_is_uploading)
            {
                _must_stop = true;
                _upload_thread.Join();
            }
        }
        public bool is_uploading_now()
        {
            return _is_uploading;
        }
        private void upload_thread_func()
        {
            _is_uploading = true;
            _logger.append_delayed_copy_logs();
            backup_delayed_files();
            _backup_folder(Env.copy_src_root);
            _is_uploading = false;
        }
        public void _backup_folder(string folderpath, bool reserve_if_fails = true)
        {
            try
            {
                if (_must_stop)
                    return;

                string[] subdirectoryEntries = Directory.GetDirectories(folderpath);
                foreach (string subdirectory in subdirectoryEntries)
                {
                    if (_must_stop)
                        return;

                    string sub_dir = Path.GetFileName(subdirectory);
                    sub_dir = sub_dir.ToLower();
                    if (sub_dir == "$recycle.bin" || sub_dir == "system volume information")
                        continue;

                    string backupfolderpath = Env.copy_dst_root + get_rel_path(subdirectory);
                    Directory.CreateDirectory(backupfolderpath);

                    _backup_folder(subdirectory, reserve_if_fails);
                }

                string[] fileEntries = Directory.GetFiles(folderpath);
                foreach (string fileName in fileEntries)
                {
                    if (_must_stop)
                        return;

                    DateTime lastModified = File.GetLastWriteTime(fileName);
                    if (_copy_all ||
                        (lastModified.Year == _modified_date.Year && lastModified.Month == _modified_date.Month && lastModified.Day == _modified_date.Day))
                    {
                        string backupfilepath = Env.copy_dst_root + get_rel_path(fileName);
                        _backup_file(fileName, backupfilepath, reserve_if_fails);
                    }
                }

                return;
            }
            catch (Exception exception)
            {
                Env.log_error($"Exception Error ({System.Reflection.MethodBase.GetCurrentMethod().Name}): {exception.Message}");
                return;
            }
        }

        private bool _backup_file(string sourcefilepath, string destfilepath, bool reserve_if_fails = true)
        {
            bool ret = false;
            try
            {
                File.Copy(sourcefilepath, destfilepath, true);
                _logger.append_copy_log(sourcefilepath, destfilepath);
                ret = true;
            }
            catch (Exception exception)
            {
                Env.log_error($"Exception Error ({System.Reflection.MethodBase.GetCurrentMethod().Name}): {exception.Message}");
                if (reserve_if_fails)
                    append_reserved_files(sourcefilepath);
                ret = false;
            }
            return ret;
        }

        private string get_rel_path(string user_abs_path)
        {
            string path = user_abs_path.Substring(Env.copy_src_root.Length);
            return path;
        }
        private void append_reserved_files(string src_path)
        {
            File.AppendAllText(delayed_backup_log_file, src_path + "\r\n");
        }
        private void backup_delayed_files()
        {
            if (!File.Exists(delayed_backup_log_file))
                return;
            string[] lines = File.ReadAllLines(delayed_backup_log_file);
            List<string> failed_lines = new List<string>();

            foreach (string line in lines)
            {
                string src = line;
                if (src.EndsWith("\n"))
                    src = src.Substring(0, src.Length - "\n".Length);

                string dst = Env.copy_dst_root + get_rel_path(src);
                if (!_backup_file(src, dst, false))
                    failed_lines.Add(src);
            }

            File.Delete(delayed_backup_log_file);

            foreach (string line in failed_lines)
                append_reserved_files(line);
        }
    }
}
