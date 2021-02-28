using HMS.Enities;
using HMS.WEB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.WEB.Areas.ViewModels
{
    public class AccomodationPacakageListingModel
    {
        public string searchTerm { get; set; }
        public IEnumerable<AcomodationPackage> AcomodationPackages { get; set; }
        public IEnumerable<AcomodationType> AcomodationTypes { get; set; }
        public int? AccomodationTypeID { get; set; }
        public Pager Pager { get; set; }
    }

    public class AccomodationPacakageActionModel
    {

        public int ID { get; set; }
        public int AccomodationTypeID { get; set; }
        public AcomodationType AcomodationType { get; set; }
        public string Name { get; set; }
        public int NoOfRooms { get; set; }
        public decimal FeePerNight { get; set; }
        public IEnumerable<AcomodationType> AcomodationTypes { get; set; }
    }
}