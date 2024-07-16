using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Pass_MAUI.Models
{
    public class SettingModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
    public class AppSettingModel
    {
        public List<SettingModel> AppSettings { get; set; } = new List<SettingModel>();
    }
}
