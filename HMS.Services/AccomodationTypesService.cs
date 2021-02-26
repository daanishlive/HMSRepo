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

        public bool saveAccomodationType(AcomodationType acomodationType)
        {
            var context = new HMSContext();
            context.AcomodationTypes.Add(acomodationType);
            return context.SaveChanges() > 0;
        }
    }
}
