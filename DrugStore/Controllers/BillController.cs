using DrugStore.Areas.Identity.Data;
using DrugStore.Models;
using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DrugStore.Controllers
{
    public class BillController : Controller
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext().Created();
        private readonly UserManager<AppNetUser> userManager;
        private readonly SignInManager<AppNetUser> signInManager;
        public BillController(UserManager<AppNetUser> userManager, SignInManager<AppNetUser> signInManager) 
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [Authorize]
        public IActionResult Index(List<HoaDon> dshd)
        {
            if (dshd.IsNullOrEmpty())
            {
                dshd = dbContext.HoaDons.Where(c => c.Id == userManager.GetUserId(User)).OrderByDescending(c=>c.NgayLap).ToList();
            }
            return View(dshd);
        }

                 
        public IActionResult Detail(Guid id)
        {
            return View(dbContext.HoaDons.Find(id));
        }
    }
}
