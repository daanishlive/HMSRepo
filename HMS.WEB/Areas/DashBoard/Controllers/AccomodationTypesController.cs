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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Listing()
        {
            AccomodationTypeModel model = new AccomodationTypeModel();
            model.AcomodationTypes = accomodationTypesService.GetAllAccomodationType();

            return PartialView("_Listing",model);
        }
        [HttpGet]
        public ActionResult Action()
        {
            AccomodationTypeActionModel model =new  AccomodationTypeActionModel();
            return PartialView("_Action",model  );
        }

        [HttpPost]
        public JsonResult Action(AccomodationTypeActionModel model)
        {
            JsonResult json = new JsonResult();
            AcomodationType acomodationType = new AcomodationType();
            acomodationType.Name = model.Name;
            acomodationType.Description = model.Description;
            var result= accomodationTypesService.saveAccomodationType(acomodationType);
            if (result)
            {
                json.Data = new { Success = true };
            }
        else
	{
                json.Data = new { Success = false, Message = "Unable To Save Accomodation Type" };
	}

            return json;
        }
    }
}