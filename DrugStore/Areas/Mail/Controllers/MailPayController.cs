using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Areas.Mail.Controllers
{
    [Area("Mail")]
    public class MailPayController : Controller
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext().Created();
        public IActionResult Index(Guid id)
        {
            HoaDon hoadon = dbContext.HoaDons.Find(id);

            return View(hoadon);
        }
    }
}
