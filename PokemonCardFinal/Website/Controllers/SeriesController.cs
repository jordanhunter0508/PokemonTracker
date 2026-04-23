using LogicLayerInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{
    [Authorize]
    [Route("Admin/[controller]/[action]")]
    public class SeriesController : Controller
    {
        private readonly ISeriesManager _seriesManager;

        public SeriesController(ISeriesManager seriesManager) 
        {
            _seriesManager = seriesManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Activate(string id, bool active)
        {
            try
            {
                _seriesManager.ActivateSeries(id,active);
                _seriesManager.ActivateBoostersBySeriesID(id, active);
                _seriesManager.ActivateCardsBySeriesID(id, active);

                return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = $"Could not change the series activation status.";
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: SeriesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SeriesController/Create
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

        // GET: SeriesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SeriesController/Edit/5
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

        // GET: SeriesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SeriesController/Delete/5
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
