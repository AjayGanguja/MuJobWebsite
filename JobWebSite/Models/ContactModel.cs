using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobWebSite.Models
{
    public class ContactModel
    {
        [Required]
        [Display(Name = "الاسم")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "البريد الاكترونى")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "الموضوع")]
        public string Subject { get; set; }
        [Required]
        [Display(Name = "الرسالة")]
        public string Meassage { get; set; }
    }
}