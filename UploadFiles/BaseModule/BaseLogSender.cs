using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModule
{
    public abstract class BaseLogSender
    {
        public abstract void change_settings();
        public abstract bool upload_copylog(List<LogEntry> log_list);
    }
}
