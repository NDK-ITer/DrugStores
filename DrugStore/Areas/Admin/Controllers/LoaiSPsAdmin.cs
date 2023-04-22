using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Areas.Admin.Controllers
{
    public class LoaiSPsAdmin : Controller
    {
        // GET: LoaiSPsAdmin
        public ActionResult Index()
        {
            return View();
        }

        // GET: LoaiSPsAdmin/Details/5
        public ActionResult Details(string id)
        {
            return View();
        }

        // GET: LoaiSPsAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiSPsAdmin/Create
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

        // GET: LoaiSPsAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoaiSPsAdmin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
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

        // GET: LoaiSPsAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoaiSPsAdmin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
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
