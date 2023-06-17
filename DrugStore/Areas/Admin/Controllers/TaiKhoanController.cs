using DrugStore.Areas.Admin.Data;
using DrugStore.Areas.Identity.Data;
using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using X.PagedList;

namespace DrugStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Manager")]
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



        // GET: AccountController/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            ViewBag.RoleId = new SelectList(dbContext.AspNetRoles, "Id", "Name");
            var user = dbContext.AspNetUsers.Find(id);
            var role = dbContext.AspNetUserRoles.Where(x=>x.UserId==id).FirstOrDefault();

            var userinput = new UserInput() { 
            Id=user.Id,
            UserName= user.UserName,
            Idrole=role.RoleId,
            Userlock= user.LockoutEnd > DateTime.Now ? true : false,    
            
            };

            return View(userinput);
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserInput userInput)
        {
            var user = dbContext.AspNetUsers.Find(userInput.Id);
           
            user.LockoutEnd= userInput.Userlock ? DateTime.Now.AddYears(100) : DateTime.Now;
            user.UpdateDate=DateTime.Now;
            


            var role = dbContext.AspNetUserRoles.Where(x => x.UserId == userInput.Id).FirstOrDefault();
            if (role.RoleId != userInput.Idrole)
            {
                dbContext.AspNetUserRoles.Remove(role);
                dbContext.SaveChanges();

                var roleuser = new AspNetUserRoles()
                {
                    UserId = user.Id,
                    RoleId = userInput.Idrole,
                };
                dbContext.AspNetUserRoles.Add(roleuser);
                dbContext.SaveChanges();
            }
            dbContext.SaveChanges();


            return RedirectToAction("Index", "TaiKhoan");
        }

        
    }
}
