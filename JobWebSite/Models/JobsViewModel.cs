﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobWebSite.Models
{
    public class JobsViewModel
    {

        public string JobTitle { get; set; }
        public IEnumerable<ApplyForJob> items { get; set; }


    }
}