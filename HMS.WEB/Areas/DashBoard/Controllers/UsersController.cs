using HMS.Enities;
using HMS.Services;
using HMS.WEB.Areas.ViewModels;
using HMS.WEB.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HMS.WEB.Areas.DashBoard.Controllers
{
    public class UsersController : Controller
    {
        private HMSSignInManager _signInManager;
        private HMSUserManager _userManager;

        public UsersController()
        {
        }

        public UsersController(HMSUserManager userManager, HMSSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public HMSSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<HMSSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public HMSUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<HMSUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        AccomodationService accomodationService = new AccomodationService();

        AccomodationPackageService accomodationPackageService = new AccomodationPackageService();

        // GET: DashBoard/AccomodationPackages

        public ActionResult Index(string searchTerm, string roleID, int? page)
        {
            int recordSize = 1;
            page = page ?? 1;

            UsersListingModel model = new UsersListingModel();

            model.SearchTerm = searchTerm;
            model.RoleID = roleID;
            // model.AccomodationPackages = accomodationPackageService.GetAllAcomodationPackage();

            model.Users = SearchUser(searchTerm, roleID, page.Value, recordSize);
            var totalRecords = SearchUserCount(searchTerm, roleID);

            model.Pager = new Pager(totalRecords, page, recordSize);

            return View(model);

        }

        public IEnumerable<HMSUser> SearchUser(string searchTerm, string roleID, int page, int recordSize)
        {

            var users = UserManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            }
            if (!string.IsNullOrEmpty(roleID))
            {
                users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            }
            var skip = (page - 1) * recordSize;

            return users.OrderBy(x => x.Email).Skip(skip).Take(recordSize).ToList();
        }

        public int SearchUserCount(string searchTerm, string roleID)
        {

            var users = UserManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            }
            if (!string.IsNullOrEmpty(roleID))
            {
                ////users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            }
           

            return users.Count();
        }


        [HttpGet]
        public async Task<ActionResult> Action(string ID)
        {
            UserActionModel model = new UserActionModel();

            if (!string.IsNullOrEmpty(ID)) //we are trying to edit a record
            {
                var user =await UserManager.FindByIdAsync(ID);

                model.ID = user.Id;
                model.FullName = user.FullName;
                model.Email = user.Email;
                model.UserName = user.UserName;
                model.Country = user.Country;
                model.City = user.City;
                model.Address = user.Address;
               
            }

            return PartialView("_Action", model);
        }

        [HttpPost]
        public async Task<JsonResult> Action(UserActionModel model)
        {
            JsonResult json = new JsonResult();

             IdentityResult result= null;

            if (!string.IsNullOrEmpty(model.ID)) //we are trying to edit a record
            {
                var user = await UserManager.FindByIdAsync(model.ID);

                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.Country = model.Country;
                user.City = model.City;
                user.Address = model.Address;

                result = await UserManager.UpdateAsync(user);
               
            }
            else //we are trying to create a record
            {
                var user = new HMSUser();

                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.Country = model.Country;
                user.City = model.City;
                user.Address = model.Address;

                result = await UserManager.CreateAsync(user);
            }

            json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };

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