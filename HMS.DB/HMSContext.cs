using HMS.Enities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DB
{
    public class HMSContext : IdentityDbContext<HMSUser>
    {
        public HMSContext() : base("GoldFlakeContext")
        {
        }
        public static HMSContext Create()
        {
            return new HMSContext();
        }
        public DbSet<AcomodationType> AcomodationTypes { get; set; }
        public DbSet<AcomodationPackage> AcomodationPackages { get; set; }
        public DbSet<Accomodation> Accomodations { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
