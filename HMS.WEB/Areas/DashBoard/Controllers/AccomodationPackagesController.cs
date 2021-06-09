using HMS.Enities;
using HMS.Services;
using HMS.WEB.Areas.ViewModels;
using HMS.WEB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.WEB.Areas.DashBoard.Controllers
{
    public class AccomodationPackagesController : Controller
    {
        AccomodationPackageService accomodationPackageService = new AccomodationPackageService();
        AccomodationTypesService accomodationTypesService = new AccomodationTypesService();
        // GET: DashBoard/AccomodationPackages

        public ActionResult Index(string searchTerm,int? accomodationTypeID,int? page)
        {
            
            int recordSize = 3;
            page = page ?? 1;
            AccomodationPacakageListingModel model = new AccomodationPacakageListingModel();
            model.SearchTerm = searchTerm;
            model.AccomodationTypeID = accomodationTypeID;

            model.AcomodationPackages = accomodationPackageService.SearchAcomodationPackage(searchTerm, accomodationTypeID,page.Value,recordSize);
            model.AccomodationTypes = accomodationTypesService.GetAllAccomodationType();
            var totalRecords= accomodationPackageService.SearchAcomodationPackageCount(searchTerm, accomodationTypeID);

            model.Pager = new Pager(totalRecords, page,recordSize);
            return View(model);
            
        }
       

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            AccomodationPacakageActionModel model = new AccomodationPacakageActionModel();
            if (ID.HasValue)
            {
                var accomodationPackage = accomodationPackageService.GetAcomodationPackage(ID.Value);
                model.ID = accomodationPackage.ID;
                model.AccomodationTypeID = accomodationPackage.AccomodationTypeID;
                
                model.Name = accomodationPackage.Name;
                model.NoOfRooms = accomodationPackage.NoOfRooms;
                model.FeePerNight = accomodationPackage.FeePerNight;
            }
            model.AccomodationTypes = accomodationTypesService.GetAllAccomodationType();

            return PartialView("_Action",model);
        }

        [HttpPost]
        public JsonResult Action(AccomodationPacakageActionModel model)
        {
            JsonResult json = new JsonResult();
            var result = false;
            if (model.ID > 0)
            {

                var acomodationPackage = accomodationPackageService.GetAcomodationPackage(model.ID);
                acomodationPackage.AccomodationTypeID = model.AccomodationTypeID;
                acomodationPackage.Name = model.Name;
                acomodationPackage.FeePerNight = model.FeePerNight;
                acomodationPackage.NoOfRooms = model.NoOfRooms;
               
                result = accomodationPackageService.updateAcomodationPackage(acomodationPackage);
            }
            else
            {
                AcomodationPackage acomodationPackage = new AcomodationPackage();
                acomodationPackage.AccomodationTypeID = model.AccomodationTypeID;
                acomodationPackage.Name = model.Name;
                acomodationPackage.AcomodationType = model.AccomodationType;
                acomodationPackage.Name = model.Name;
                acomodationPackage.NoOfRooms = model.NoOfRooms;
                acomodationPackage.FeePerNight = model.FeePerNight;
                result = accomodationPackageService.saveAccomodationType(acomodationPackage);
            }


            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable To Perform Action On Accomodation Package" };
            }

            return json;
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            AccomodationPacakageActionModel model = new AccomodationPacakageActionModel();

            var accomodationType = accomodationPackageService.GetAcomodationPackage(ID);
            model.ID = accomodationType.ID;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(AccomodationPacakageActionModel model)
        {
            JsonResult json = new JsonResult();
            var result = false;
            var acomodationType = accomodationPackageService.GetAcomodationPackage(model.ID);

            result = accomodationPackageService.deleteAccomodationType(acomodationType);

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable To Perform Action On Accomodation Type" };
            }

            return json;
        }

    }
}