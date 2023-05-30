using DrugStore.Areas.Identity.Data;
using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using X.PagedList;

namespace DrugStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TaiKhoanController : Controller
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext().Created();
        private UserManager<AppNetUser> userManager;
        private SignInManager<AppNetUser> signInManager;
        private readonly IHttpContextAccessor contx;
        public TaiKhoanController(UserManager<AppNetUser> userManager, SignInManager<AppNetUser> signInManager, IHttpContextAccessor contx)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.contx = contx;
            //var user = userManager.Users.ToList();
        }
        // GET: AccountController
        public ActionResult Index(int? page, string? keySearch)
        {
            List<AspNetUser> Users;
            if (!keySearch.IsNullOrEmpty())
            {
                Users = dbContext.AspNetUsers.Where(c => c.Email.Contains(keySearch)||(c.FirstName+c.LastName).Contains(keySearch)|| c.PhoneNumber.Contains(keySearch)).OrderBy(c => c.FirstName+c.LastName).ToList();
            }
            else
            {
                Users = dbContext.AspNetUsers.OrderBy(c => c.FirstName + c.LastName).ToList();
            }

            if (page == null) { page = 1; }
            page = page < 1 ? 1 : page;
            int pageSize = 5;
            return View(Users.ToPagedList((int)page, pageSize));
        }

        // GET: AccountController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
