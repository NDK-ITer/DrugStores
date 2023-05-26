using DrugStore.Areas.Identity.Data;
using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DrugStore.Areas.Identity.Pages.Account.Manage
{
    [Authorize]
    public class BillModel : PageModel
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext().Created();
        private readonly UserManager<AppNetUser> userManager;
        private readonly SignInManager<AppNetUser> signInManager;

        public List<HoaDon> dsHD()
        {
            List<HoaDon> dsHD = null;
            if (signInManager.IsSignedIn(User))
            {
                dsHD = dbContext.HoaDons.Where(c => c.Id == userManager.GetUserId(User)).ToList();
            }
            return dsHD;
        }
        public BillModel(UserManager<AppNetUser> userManager,
            SignInManager<AppNetUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

    }
}
