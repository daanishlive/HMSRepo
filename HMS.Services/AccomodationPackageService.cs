using HMS.DB;
using HMS.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
   public class AccomodationPackageService
    {
        public IEnumerable<AcomodationPackage> GetAllAcomodationPackage()
        {
            var context = new HMSContext();
            return context.AcomodationPackages.ToList();
        }

        public IEnumerable<AcomodationPackage> SearchAcomodationPackage(string searchTerm,int? accomodationTypeID,int page,int recordSize)
        {
            var context = new HMSContext();
            var AcomodationPackages = context.AcomodationPackages.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                AcomodationPackages = AcomodationPackages.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            if (accomodationTypeID.HasValue&& accomodationTypeID.Value>0)
            {
                AcomodationPackages = AcomodationPackages.Where(a => a.AccomodationTypeID == accomodationTypeID.Value);
            }
            var skip = (page - 1) * recordSize;

            return AcomodationPackages.OrderBy(x=>x.AccomodationTypeID).Skip(skip).Take(recordSize).ToList();
        }

        public int  SearchAcomodationPackageCount(string searchTerm, int? accomodationTypeID)
        {
            var context = new HMSContext();
            var AcomodationPackages = context.AcomodationPackages.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                AcomodationPackages = AcomodationPackages.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            if (accomodationTypeID.HasValue && accomodationTypeID.Value > 0)
            {
                AcomodationPackages = AcomodationPackages.Where(a => a.AccomodationTypeID == accomodationTypeID.Value);
            }


            return AcomodationPackages.Count();
        }

        public AcomodationPackage GetAcomodationPackage(int ID)
        {
            using (var context = new HMSContext())
            {
                return context.AcomodationPackages.Find(ID);
            }
            
            
        }

        public bool saveAccomodationType(AcomodationPackage acomodationPackage)
        {
            var context = new HMSContext();
            context.AcomodationPackages.Add(acomodationPackage);
            return context.SaveChanges() > 0;
        }

        public bool updateAcomodationPackage(AcomodationPackage acomodationPackage)
        {
            var context = new HMSContext();
            context.Entry(acomodationPackage).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges() > 0;
        }

        public bool deleteAccomodationType(AcomodationPackage acomodationPackage)
        {
            var context = new HMSContext();
            context.Entry(acomodationPackage).State = System.Data.Entity.EntityState.Deleted;
            return context.SaveChanges() > 0;
        }
    }
}
