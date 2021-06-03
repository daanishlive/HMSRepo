﻿using HMS.WEB.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.WEB.Areas.ViewModels
{
    public class RolesListingModel
    {
        public IEnumerable<IdentityRole> Roles { get; set; }
        public string SearchTerm { get; set; }
        public Pager Pager { get; set; }
    }

    public class RolesActionModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
}