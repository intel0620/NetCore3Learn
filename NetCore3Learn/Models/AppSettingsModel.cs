using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore3Learn.Models
{
    public class AppSettingsModel
    {
        public string SiteUrl { get; set; }
        public Guid DefaultId { get; set; }
        public int StartIndex { get; set; }
    }
}
