using HMS.Enities;
using HMS.Services;
using HMS.WEB.Areas.ViewModels;
using HMS.WEB.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HMS.WEB.Areas.DashBoard.Controllers
{
    public class RolesController : Controller
    {
        private HMSSignInManager _signInManager;
        private HMSUserManager _userManager;
        private HMSRolesManager _roleManager;
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
        public HMSRolesManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<HMSRolesManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public RolesController()
        {
        }

        public RolesController(HMSUserManager userManager, HMSSignInManager signInManager,HMSRolesManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        AccomodationService accomodationService = new AccomodationService();

        AccomodationPackageService accomodationPackageService = new AccomodationPackageService();

        // GET: DashBoard/AccomodationPackages

        public ActionResult Index(string searchTerm, string role, int? page)
        {
            int recordSize = 5;
            page = page ?? 1;

            RolesListingModel model = new RolesListingModel();

            model.SearchTerm = searchTerm;
            model.Roles = SearchRole(searchTerm,page.Value,recordSize);
            
            var totalRecords = SearchRoleCount(searchTerm);

            model.Pager = new Pager(totalRecords, page, recordSize);

            return View(model);

        }

        public IEnumerable<IdentityRole> SearchRole(string searchTerm, int page, int recordSize)
        {

            var users = RoleManager.Roles.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            
            var skip = (page - 1) * recordSize;

            return users.OrderBy(x => x.Name).Skip(skip).Take(recordSize).ToList();
       
        }

        public int SearchRoleCount(string searchTerm)
        {

            var users = RoleManager.Roles.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            return users.Count();
            
        }


        [HttpGet]
        public async Task<ActionResult> Action(string ID)
        {
            RolesActionModel model = new RolesActionModel();

            if (!string.IsNullOrEmpty(ID)) //we are trying to edit a record
            {
                var user = await RoleManager.FindByIdAsync(ID);

                model.ID = user.Id;
                model.Name = user.Name;
            }

            return PartialView("_Action", model);
        }

        [HttpPost]
        public async Task<JsonResult> Action(RolesActionModel model)
        {
            JsonResult json = new JsonResult();

            IdentityResult result = null;

            if (!string.IsNullOrEmpty(model.ID)) //we are trying to edit a record
            {
                var role = await RoleManager.FindByIdAsync(model.ID);
                role.Name = model.Name;
               

                result = await RoleManager.UpdateAsync(role);

            }
            else //we are trying to create a record
            {
                var role = new IdentityRole();

                role.Name = model.Name;

                result = await RoleManager.CreateAsync(role);
            }

            json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };

            return json;
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string ID)
        {
            RolesActionModel model = new RolesActionModel();
            var role = await RoleManager.FindByIdAsync(ID);
            model.ID = role.Id;
           
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(RolesActionModel model)
        {
            JsonResult json = new JsonResult();
            IdentityResult result = null;

            if (!string.IsNullOrEmpty(model.ID))
            {
                var role = await RoleManager.FindByIdAsync(model.ID);
                result = await RoleManager.DeleteAsync(role);
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid Role" };
            }

            return json;
        }

    }
}