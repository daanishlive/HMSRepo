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
    public class AccomodationsController : Controller
    {
        AccomodationService accomodationService = new Services.AccomodationService();

        AccomodationPackageService accomodationPackageService = new AccomodationPackageService();
        
        // GET: DashBoard/AccomodationPackages

        public ActionResult Index(string searchTerm, int? accomodationPackageID, int? page)
        {
            int recordSize = 5;
            page = page ?? 1;

            AccomodationsListingModel model = new AccomodationsListingModel();

            model.SearchTerm = searchTerm;
            model.AccomodationPackageID = accomodationPackageID;
            model.AccomodationPackages = accomodationPackageService.GetAllAcomodationPackage();

            model.Accomodations = accomodationService.SearchAcomodation(searchTerm, accomodationPackageID, page.Value, recordSize);
            var totalRecords = accomodationService.SearchAcomodationCount(searchTerm, accomodationPackageID);

            model.Pager = new Pager(totalRecords, page, recordSize);

            return View(model);

        }


        [HttpGet]
        public ActionResult Action(int? ID)
        {
            AccomodationActionModel model = new AccomodationActionModel();

            if (ID.HasValue) //we are trying to edit a record
            {
                var accomodation = accomodationService.GetAcomodation(ID.Value);

                model.ID = accomodation.ID;
                model.AccomodationPackageID = accomodation.AccomodationPackageID;
                model.Name = accomodation.Name;
                model.Description = accomodation.Description;
            }

            model.AccomodationPackages = accomodationPackageService.GetAllAcomodationPackage();

            return PartialView("_Action", model);
        }

        [HttpPost]
        public JsonResult Action(AccomodationActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            if (model.ID > 0) //we are trying to edit a record
            {
                var accomodation = accomodationService.GetAcomodation(model.ID);

                accomodation.AccomodationPackageID = model.AccomodationPackageID;
                accomodation.Name = model.Name;
                accomodation.Description = model.Description;

                result = accomodationService.updateAcomodation(accomodation);
            }
            else //we are trying to create a record
            {
                Accomodation accomodation = new Accomodation();

                accomodation.AccomodationPackageID = model.AccomodationPackageID;
                accomodation.Name = model.Name;
                accomodation.Description = model.Description;

                result = accomodationService.saveAccomodationType(accomodation);
            }

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Accomodation." };
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