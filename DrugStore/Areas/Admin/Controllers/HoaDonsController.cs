using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HoaDonsController : Controller
    {
        // GET: HoaDonsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: HoaDonsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HoaDonsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HoaDonsController/Create
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

        // GET: HoaDonsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HoaDonsController/Edit/5
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

        // GET: HoaDonsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HoaDonsController/Delete/5
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
