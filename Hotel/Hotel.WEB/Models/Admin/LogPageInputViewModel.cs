﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.WEB.Models.Admin
{
    public class LogPageInputViewModel
    {
        public string UserNameSubStr { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public bool SearchErrors { get; set; }
    }
}