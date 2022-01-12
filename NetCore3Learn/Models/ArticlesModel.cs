using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore3Learn.Models
{
    public class ArticlesModel
    {//[Id],[Title],[Body],[CoverPhoto],[CreateDate],[DayOfWeek],[Tags]
        public Guid MyProperty { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string CoverPhoto { get; set; }
        public DateTime CreateDate { get; set; }
        public int DayOfWeek { get; set; }
        public string Tags { get; set; }


    }
}
