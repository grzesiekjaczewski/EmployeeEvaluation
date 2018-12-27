using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using EmployeeEvaluation.Models;

namespace EmployeeEvaluation.Controllers
{
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext _db = new ApplicationDbContext();

        public AdminController()
        {
        }

        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdminUserList(string id)
        {
            var userList = new List<UsersAndRights>();
            if (id == null || id == "")
            {
                var userListOrg = UserManager.Users.OrderBy(m => m.UserName).ToList();
                foreach (var userListOrgItem in userListOrg)
                {
                    UsersAndRights usersAndRights = new UsersAndRights();
                    usersAndRights.Id = userListOrgItem.Id;
                    usersAndRights.UserName = userListOrgItem.UserName;
                    usersAndRights.IsAdmin = UserManager.IsInRole(userListOrgItem.Id, "Admin");
                    usersAndRights.IsHRManager = UserManager.IsInRole(userListOrgItem.Id, "HR Manager");
                    usersAndRights.IsManager = UserManager.IsInRole(userListOrgItem.Id, "Manager");
                    usersAndRights.IsEmployee = UserManager.IsInRole(userListOrgItem.Id, "Pracownik");
                    userList.Add(usersAndRights);
                }
            }
            else
            {
                var userListOrg = UserManager.Users.Where(m => m.UserName.ToLower().IndexOf(id.ToLower()) > -1).OrderBy(m => m.UserName).ToList();
                foreach (var userListOrgItem in userListOrg)
                {
                    UsersAndRights usersAndRights = new UsersAndRights();
                    usersAndRights.Id = userListOrgItem.Id;
                    usersAndRights.UserName = userListOrgItem.UserName;
                    usersAndRights.IsAdmin = UserManager.IsInRole(userListOrgItem.Id, "Admin");
                    usersAndRights.IsHRManager = UserManager.IsInRole(userListOrgItem.Id, "HR Manager");
                    usersAndRights.IsManager = UserManager.IsInRole(userListOrgItem.Id, "Manager");
                    usersAndRights.IsEmployee = UserManager.IsInRole(userListOrgItem.Id, "Pracownik");
                    userList.Add(usersAndRights);
                }
            }

            return View(userList);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdminUserRolesList(Guid? id)
        {
            ApplicationUser user = UserManager.FindById(id.ToString());
            var userRoles = UserManager.GetRoles(id.ToString()).ToList();
            var roles = _db.Roles.OrderBy(x => x.Name).ToList();
            List<Role> activeRoles = new List<Role>();

            foreach (var role in roles)
            {
                Role activeRole = new Role();
                activeRole.Id = role.Id;
                activeRole.Name = role.Name;
                if (UserManager.IsInRole(id.ToString(), role.Name))
                {
                    activeRole.ActiveForUser = true;
                }
                else
                {
                    activeRole.ActiveForUser = false;
                }
                activeRoles.Add(activeRole);
            }

            ViewBag.User = user;
            return View(activeRoles);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Message()
        {
            ViewBag.Message = "Nie można usunąć jedynego adminstratora!";
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminUserRolesList(FormCollection forms)
        {
            var userId = Request["UserId"];
            var roles = _db.Roles.OrderBy(x => x.Name).ToList();
            bool message = false;

            int admins = 0;
            foreach (var user in _db.Users)
            {
                if (UserManager.IsInRole(user.Id, "Admin"))
                {
                    admins++;
                }
            };

            foreach (var role in roles)
            {
                var roleVal = Request[role.Name];
                if (roleVal == null)
                {
                    if (role.Name != "Admin" || admins > 1)
                    {
                        UserManager.RemoveFromRole(userId, role.Name);
                    }
                    if (role.Name == "Admin" && admins <= 1 && UserManager.IsInRole(userId, "Admin"))
                    {
                        message = true;
                    }
                }
                else
                {
                    UserManager.AddToRole(userId, role.Name);
                }
            }
            if (message)
            {
                return RedirectToAction("Message");
            }
            return RedirectToAction("AdminUserList");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}