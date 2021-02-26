using HMS.Enities;
using HMS.Services;
using HMS.WEB.Areas.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.WEB.Areas.DashBoard.Controllers
{
    public class AccomodationTypesController : Controller
    {
        AccomodationTypesService accomodationTypesService = new AccomodationTypesService();
        // GET: DashBoard/AccomodationTypes
        public ActionResult Index(string searchTerm)
        {
            AccomodationTypeModel model = new AccomodationTypeModel();
              model.searchTerm = searchTerm;
           
            model.AcomodationTypes = accomodationTypesService.SearchAccomodationType(searchTerm);
            return View(model);
        }

        //public ActionResult Listing()
        //{
        //    AccomodationTypeModel model = new AccomodationTypeModel();
        //    model.AcomodationTypes = accomodationTypesService.GetAllAccomodationType();

        //    return PartialView("_Listing", model);
        //}


        [HttpGet]
        public ActionResult Action(int?ID)
        {
            AccomodationTypeActionModel model =new  AccomodationTypeActionModel();
            if (ID.HasValue)
            {
                var accomodationType = accomodationTypesService.GetAccomodationType(ID.Value);
                model.ID = accomodationType.ID;
                model.Name = accomodationType.Name;
                model.Description = accomodationType.Description;

            }
            
            return PartialView("_Action",model  );
        }

        [HttpPost]
        public JsonResult Action(AccomodationTypeActionModel model)
        {
            JsonResult json = new JsonResult();
            var result = false;
            if (model.ID>0)
            {
                var acomodationType = accomodationTypesService.GetAccomodationType(model.ID);
                acomodationType.Name = model.Name;
                acomodationType.Description = model.Description;
                result = accomodationTypesService.updateAccomodationType(acomodationType);
            }
            else
            {
                AcomodationType acomodationType = new AcomodationType();
                acomodationType.Name = model.Name;
                acomodationType.Description = model.Description;
                result = accomodationTypesService.saveAccomodationType(acomodationType);
            }
            
           
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
        [HttpGet]
        public ActionResult Delete(int ID)
        {
            AccomodationTypeActionModel model = new AccomodationTypeActionModel();

            var accomodationType = accomodationTypesService.GetAccomodationType(ID);
            model.ID = accomodationType.ID;
            
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(AccomodationTypeActionModel model)
        {
            JsonResult json = new JsonResult();
            var result = false;
            var acomodationType = accomodationTypesService.GetAccomodationType(model.ID);
            
            result = accomodationTypesService.deleteAccomodationType(acomodationType);

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