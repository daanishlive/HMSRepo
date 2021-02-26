using HMS.DB;
using HMS.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
   public class AccomodationTypesService
    {
        public IEnumerable<AcomodationType> GetAllAccomodationType()
        {
            var context = new HMSContext();
           return context.AcomodationTypes.ToList();
        }

        public IEnumerable<AcomodationType> SearchAccomodationType(string searchTerm)
        {
            var context = new HMSContext();
            var acomodationTypes= context.AcomodationTypes.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                acomodationTypes = acomodationTypes.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower())); 
            }
            return acomodationTypes.ToList();
        }

        public AcomodationType GetAccomodationType(int ID)
        {
            var context = new HMSContext();
            return context.AcomodationTypes.Find(ID);
        }

        public bool saveAccomodationType(AcomodationType acomodationType)
        {
            var context = new HMSContext();
            context.AcomodationTypes.Add(acomodationType);
            return context.SaveChanges() > 0;
        }

        public bool updateAccomodationType(AcomodationType acomodationType)
        {
            var context = new HMSContext();
            context.Entry(acomodationType).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges() > 0;
        }

        public bool deleteAccomodationType(AcomodationType acomodationType)
        {
            var context = new HMSContext();
            context.Entry(acomodationType).State = System.Data.Entity.EntityState.Deleted;
            return context.SaveChanges() > 0;
        }
    }
}
