﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Enities
{
   public  class Accomodation
    {
        public int ID { get; set; }
        public int AccomodationPackageID { get; set; }
        public AcomodationPackage AccomodationPackage { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}