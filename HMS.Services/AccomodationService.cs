using HMS.DB;
using HMS.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
   public class AccomodationService
    { 
     public IEnumerable<Accomodation> GetAllAcomodation()
    {
        var context = new HMSContext();
        return context.Accomodations.ToList();
    }

    public IEnumerable<Accomodation> SearchAcomodation(string searchTerm, int? accomodationTypeID, int page, int recordSize)
    {
        var context = new HMSContext();
        var Accomodations = context.Accomodations.AsQueryable();
        if (!string.IsNullOrEmpty(searchTerm))
        {
                Accomodations = Accomodations.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
        }
        if (accomodationTypeID.HasValue && accomodationTypeID.Value > 0)
        {
                Accomodations = Accomodations.Where(a => a.AccomodationPackageID == accomodationTypeID.Value);
        }
        var skip = (page - 1) * recordSize;

        return Accomodations.OrderBy(x => x.AccomodationPackageID).Skip(skip).Take(recordSize).ToList();
    }

    public int SearchAcomodationCount(string searchTerm, int? accomodationTypeID)
    {
        var context = new HMSContext();
        var Accomodations = context.Accomodations.AsQueryable();
        if (!string.IsNullOrEmpty(searchTerm))
        {
                Accomodations = Accomodations.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
        }
        if (accomodationTypeID.HasValue && accomodationTypeID.Value > 0)
        {
                Accomodations = Accomodations.Where(a => a.AccomodationPackageID == accomodationTypeID.Value);
        }


        return Accomodations.Count();
    }

    public Accomodation GetAcomodation(int ID)
    {
        using (var context = new HMSContext())
        {
            return context.Accomodations.Find(ID);
        }


    }

    public bool saveAccomodationType(Accomodation acomodation)
    {
        var context = new HMSContext();
        context.Accomodations.Add(acomodation);
        return context.SaveChanges() > 0;
    }

    public bool updateAcomodation(Accomodation acomodation)
    {
        var context = new HMSContext();
        context.Entry(acomodation).State = System.Data.Entity.EntityState.Modified;
        return context.SaveChanges() > 0;
    }

    public bool deleteAccomodationType(Accomodation acomodation)
    {
        var context = new HMSContext();
        context.Entry(acomodation).State = System.Data.Entity.EntityState.Deleted;
        return context.SaveChanges() > 0;
    }
}
}
