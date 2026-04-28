using LogicLayerInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{

    [Authorize(Roles = "Admin,Moderator")]
    [Route("Admin/[controller]/[action]")]
    public class SeriesController : Controller
    {
        private readonly ISeriesManager _seriesManager;

        public SeriesController(ISeriesManager seriesManager) 
        {
            _seriesManager = seriesManager;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id, IFormCollection collection)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Activate(string id, bool active)
        {
            try
            {
                _seriesManager.ActivateSeries(id, active);
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
    }
}
