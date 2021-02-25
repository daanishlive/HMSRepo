using HMS.Enities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DATA
{
    public class HMSContext : DbContext
    {
        public HMSContext() : base("HMSDATABASE")
        {
        }
        public DbSet<AcomodationType> AcomodationTypes { get; set; }
        public DbSet<AccomodationPackage> AccomodationPackages { get; set; }
        public DbSet<Accomodation> Accomodations { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
