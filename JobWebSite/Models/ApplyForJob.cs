using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobWebSite.Models
{
    public class ApplyForJob
    {
        public int Id { get; set; }

        [Display(Name = "الرسالة ")]
        public string Message { get; set; }

        [Display(Name = "وقت التقديم")]
        public DateTime ApplyDate { get; set; }
        public int JobId { get; set; }
        public string UserId { get; set; }



        // many to many relation
        public virtual Job Job { get; set; } // if made by me so put virtual 
        public virtual ApplicationUser User { get; set; } // if built-in so don't have to put virtual 


    }
}