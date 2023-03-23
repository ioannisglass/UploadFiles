using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModule
{
    public class LogEntry
    {
        [JsonConverter(typeof(LogEntryConverter))]
        public DateTime time;
        public string src;
        public string dst;

        public LogEntry()
        {

        }
        public LogEntry(string _src, string _dst)
        {
            src = _src;
            dst = _dst;
            time = DateTime.Now;
        }
        public LogEntry(string _src, string _dst, DateTime _time)
        {
            src = _src;
            dst = _dst;
            time = _time;
        }
        override public string ToString()
        {
            string log;
            string date_str = time.ToString("yyyy-MM-dd HH:mm:ss");
            log = $"{date_str},\"{src}\",\"{dst}\"";
            return log;
        }
        static public LogEntry ParseString(string log_str)
        {
            LogEntry ret = null;
            string[] vals = log_str.Split(',');
            if (vals.Length < 3)
                return null;

            ret = new LogEntry();

            ret.time = DateTime.ParseExact(vals[0], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

            string paths = log_str.Substring(vals[0].Length + 1);
            if (paths[0] != '\"' && !paths.EndsWith("\""))
                return null;
            paths = paths.Substring(1, paths.Length - 2);
            int finder = paths.IndexOf("\",\"");
            if (finder < 0)
                return null;
            ret.src = paths.Substring(0, finder);
            ret.dst = paths.Substring(finder + 3);
            return ret;
        }
    }
    public class LogEntryConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            DateTime time = (DateTime)value;
            string conv_value = time.ToString("yyyy-MM-dd HH:mm:ss");
            writer.WriteValue(conv_value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            DateTime time = DateTime.MinValue;

            if (reader.TokenType == JsonToken.Null)
            {
                throw new Exception($"Unknown mail server type : ({reader.TokenType})");
            }
            else if (reader.TokenType == JsonToken.String)
            {
                string value = serializer.Deserialize(reader, Type.GetType("DateTime")) as string;
                time = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                return time;
            }
            else
            {
                throw new Exception($"Unknown mail server type : ({reader.TokenType})");
            }
        }
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }
    }
}
