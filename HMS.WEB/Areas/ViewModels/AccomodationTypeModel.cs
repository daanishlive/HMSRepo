using HMS.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.WEB.Areas.ViewModels
{
    public class AccomodationTypeModel
    {
        public string searchTerm { get; set; }

        public IEnumerable<AcomodationType> AcomodationTypes { get; set; }
    }

    public class AccomodationTypeActionModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}