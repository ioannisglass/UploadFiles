using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UploadFiles;

namespace BaseModule
{
    public class WebPoster : BaseLogSender
    {
        private object _locker = new object();

        private string _server = "";
        public WebPoster()
        {
            change_settings();
        }
        public override void change_settings()
        {
            lock (_locker)
            {
                _server = Env.server_url;
            }
        }
        public override bool upload_copylog(List<LogEntry> log_list)
        {
            try
            {
                HttpWebRequest request;
                request = (HttpWebRequest)HttpWebRequest.Create(Env.server_url);

                var json_data = /*Regex.Unescape*/(JsonConvert.SerializeObject(log_list, Newtonsoft.Json.Formatting.Indented));

                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = json_data.Length;
                request.UserAgent = "MacOS,Safari,Mozilla/5.0 (Macintosh; Intel Mac OS X 1058) AppleWebKit/534.50.2 (KHTML like Gecko) Version/12.1.1 Safari/605.1.15";
                request.Accept = "*/*";

                var bytes_data = Encoding.ASCII.GetBytes(json_data);

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(bytes_data, 0, bytes_data.Length);
                }

                HttpWebResponse response = null;
                response = (HttpWebResponse)request.GetResponse();
                string response_string = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return true;
            }
            catch (Exception exception)
            {
                Env.log_error($"Exception Error ({System.Reflection.MethodBase.GetCurrentMethod().Name}): {exception.Message}");
                return false;
            }
        }
    }
}
