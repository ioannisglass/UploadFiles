using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UploadFiles;

namespace BaseModule
{
    public class FtpRW : BaseLogSender
    {
        private object _locker = new object();

        private string _server = "";
        private int _port = 21;
        private string _username = "";
        private string _password = "";
        private bool _passive_mode = false;
        private string _win_domain = "";
        private string _win_user = "";

        private readonly string app_log_file_name = "app.csv";

        public FtpRW()
        {
            change_settings();
        }
        public override void change_settings()
        {
            lock (_locker)
            {
                _server = Env.server_url;
                _port = Env.server_port;
                _username = Env.server_user_name;
                _password = Env.server_password;
                _win_domain = Env.windows_domain_name;
                _win_user = Env.windows_user_name;
                _passive_mode = Env.ftp_passive_mode;
            }
        }
        private string make_ftp_path(string relative_path)
        {
            string uri = $"ftp://{_server}:{_port}/{relative_path}";
            return uri;
        }
        private string make_log_relative_path(DateTime date)
        {
            string date_str = date.ToString("yyyyMMdd");
            string relative_path = $"{date.Year}/{date.Month}/{_win_domain}_{_win_user}_{date_str}.csv";
            return relative_path;
        }
        private static byte[] ReadToEnd(Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }
        private byte[] DownloadFile(string relative_path)
        {
            var fileData = new byte[0];

            try
            {
                string path = make_ftp_path(relative_path);

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(path);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.UsePassive = _passive_mode;
                request.UseBinary = true;
                request.KeepAlive = true;

                request.Credentials = new NetworkCredential(_username, _password);
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                fileData = ReadToEnd(responseStream);

                response.Close();
            }
            catch (Exception exception)
            {
                // create folder will fail if it already exist
                Env.log_error($"Exception Error ({System.Reflection.MethodBase.GetCurrentMethod().Name}): {exception.Message}");
            }

            return fileData;
        }
        private bool DoesFileExist(string relative_path)
        {
            var fileExist = false;

            var request = (FtpWebRequest)WebRequest.Create(make_ftp_path(relative_path));
            request.Method = WebRequestMethods.Ftp.GetFileSize;
            request.UsePassive = _passive_mode;
            request.Credentials = new NetworkCredential(_username, _password);

            try
            {
                using (var response = request.GetResponse())
                {
                    fileExist = true;
                }
            }
            catch (Exception exception)
            {
                // create folder will fail if it already exist
                Env.log_error($"Exception Error {relative_path} : ({System.Reflection.MethodBase.GetCurrentMethod().Name}): {exception.Message}");
            }

            return fileExist;
        }

        // upload file and handle everything
        private void UploadFile(string relative_path, byte[] fileBinary)
        {
            relative_path = ParseRelativePath(relative_path);
            CreateFolders(relative_path);
            DeleteFile(relative_path);
            SendFile(relative_path, fileBinary);
        }

        private void SendFile(string relative_path, byte[] fileBinary)
        {
            var request = (FtpWebRequest)WebRequest.Create(make_ftp_path(relative_path));
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(_username, _password);
            request.UsePassive = _passive_mode;
            var stream = request.GetRequestStream();
            var sourceStream = new MemoryStream(fileBinary);
            var length = 1024;
            var buffer = new byte[length];
            var bytesRead = 0;

            do
            {
                bytesRead = sourceStream.Read(buffer, 0, length);
                stream.Write(buffer, 0, bytesRead);
            } while (bytesRead != 0);

            sourceStream.Close();
            stream.Close();
        }
        private void AppendFile(string relative_path, byte[] data)
        {
            relative_path = ParseRelativePath(relative_path);
            CreateFolders(relative_path);

            var request = (FtpWebRequest)WebRequest.Create(make_ftp_path(relative_path));
            request.ContentLength = data.Length;
            request.Method = WebRequestMethods.Ftp.AppendFile;
            request.Credentials = new NetworkCredential(_username, _password);
            request.UsePassive = _passive_mode;
            var stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            string ftp_response = response.StatusDescription;
            string status_code = Convert.ToString(response.StatusCode);
            response.Close();
        }

        private void DeleteFile(string relative_path)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(make_ftp_path(relative_path));
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request.Credentials = new NetworkCredential(_username, _password);
                request.UsePassive = _passive_mode;
                request.GetResponse();
            }
            catch (Exception exception)
            {
                // create folder will fail if it already exist
                Env.log_error($"Exception Error ({System.Reflection.MethodBase.GetCurrentMethod().Name}): {exception.Message}");
            }
        }

        // parse the relative path for strange slashes
        private string ParseRelativePath(string relative_path)
        {
            // split to remove all slashes and duplicates
            var split = relative_path.Split(new string[] { "\\", "/" }, StringSplitOptions.RemoveEmptyEntries);

            // join the string back with valid slash
            var result = String.Join("/", split);

            return result;
        }

        // Check if the relative path need folders to be created
        private void CreateFolders(string relative_path)
        {
            if (DoesFileExist(relative_path))
                return;
            if (relative_path.IndexOf("/") > 0)
            {
                var folders = relative_path.Split(new[] { "/" }, StringSplitOptions.None).ToList();

                var folderToCreate = make_ftp_path("");
                folderToCreate = folderToCreate.Substring(0, folderToCreate.Length - 1);

                for (int i = 0; i < folders.Count - 1; i++)
                {
                    folderToCreate += ("/" + folders[i]);

                    CreateFolder(folderToCreate);
                }
            }
        }

        // create a single folder on the FTP
        private void CreateFolder(string folderPath)
        {
            FtpWebResponse response;
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(folderPath);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = new NetworkCredential(_username, _password);
                request.UsePassive = _passive_mode;
                response = (FtpWebResponse)request.GetResponse();
            }
            catch (Exception exception)
            {
                // create folder will fail if it already exist
                Env.log_error($"Exception Error {folderPath} : ({System.Reflection.MethodBase.GetCurrentMethod().Name}): {exception.Message}");
            }
        }
        public bool upload_applog(string log)
        {
            bool ret = false;

            lock (_locker)
            {
                try
                {
                    byte[] data = Encoding.UTF8.GetBytes(log + "\r\n");
                    AppendFile(app_log_file_name, data);
                    ret = true;
                }
                catch (Exception exception)
                {
                    // create folder will fail if it already exist
                    Env.log_error($"Exception Error ({System.Reflection.MethodBase.GetCurrentMethod().Name}): {exception.Message}");
                    ret = false;
                }
            }
            return ret;
        }
        public override bool upload_copylog(List<LogEntry> log_list)
        {
            bool ret = false;

            /**
             * 1. Confirm log contents. : 20190810-01:03:50,"Y:signature_shuntie4.png","Z:signature_shuntie4.png"
             *      (no slash after drive letter)
             * 2. FTP append.
             * 3. Directory checker.
             **/


            lock (_locker)
            {
                try
                {
                    foreach (LogEntry log in log_list)
                    {
                        string relative_path = make_log_relative_path(log.time);
                        if (!DoesFileExist(relative_path))
                        {
                            AppendFile(relative_path, Encoding.UTF8.GetBytes("CopyTime,Source File,Destination File\r\n"));
                        }
                        byte[] data = Encoding.UTF8.GetBytes(log.ToString() + "\r\n");
                        AppendFile(relative_path, data);
                    }
                    ret = true;
                }
                catch (Exception exception)
                {
                    // create folder will fail if it already exist
                    Env.log_error($"Exception Error ({System.Reflection.MethodBase.GetCurrentMethod().Name}): {exception.Message}");
                    ret = false;
                }
            }
            return ret;
        }
        public bool upload_copylog_file(DateTime date, string log_file_path)
        {
            bool ret = false;

            lock (_locker)
            {
                try
                {
                    string relative_path = make_log_relative_path(date);
                    byte[] data = File.ReadAllBytes(log_file_path);
                    UploadFile(relative_path, data);
                    ret = true;
                }
                catch (Exception exception)
                {
                    // create folder will fail if it already exist
                    Env.log_error($"Exception Error ({System.Reflection.MethodBase.GetCurrentMethod().Name}): {exception.Message}");
                    ret = false;
                }
            }
            return ret;
        }
    }
}
