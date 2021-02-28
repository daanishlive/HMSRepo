using HMS.Enities;
using HMS.WEB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.WEB.Areas.ViewModels
{
    public class AccomodationsListingModel
    {
        public IEnumerable<Accomodation> Accomodations { get; set; }

        public int? AccomodationPackageID { get; set; }
        public IEnumerable<AcomodationPackage> AccomodationPackages { get; set; }
        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
    }

    public class AccomodationActionModel
    {
        public int ID { get; set; }

        public int AccomodationPackageID { get; set; }
        public AcomodationPackage AccomodationPackage { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<AcomodationPackage> AccomodationPackages { get; set; }
    }
}