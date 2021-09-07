using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobWebSite.Models
{
    public class Job
    {
        public int ID { get; set; }


        [DisplayName("أسم الوظيفة")]
        public string JobTitle { get; set; }


        [DisplayName("وصف الوظيفة")]
        [AllowHtml]
        public string JobContent { get; set; }


        [DisplayName("صورة الوظيفة")]
        public string JobImage { get; set; }


        [DisplayName("نوع الوظيفة")]
        public int CategoryId { get; set; } // must have the same formate ModelNameId
        public string UserId { get; set; } // must have the same formate ModelNameId


        public virtual Category Category { get; set; } // not  have to be the  Model Name
        public virtual ApplicationUser User { get; set; } // not  have to be the  Model Name





    }
}