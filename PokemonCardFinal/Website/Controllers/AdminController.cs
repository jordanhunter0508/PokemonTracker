using DataDomain;
using LogicLayerInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Website.Models;

namespace Website.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IBoosterManager _boosterManager;
        private readonly ISeriesManager _seriesManager;

        public AdminController(IBoosterManager boosterManager, ISeriesManager seriesManager)
        { 
            _boosterManager = boosterManager;
            _seriesManager = seriesManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            try
            {
                var allBoosters = _boosterManager.GetBoosters().OrderBy(b => b.ReleaseDate);
                var allSeries = _seriesManager.GetAllSeries();

                List<SeriesWithBoosters> vm = new List<SeriesWithBoosters>();

                // Gets a list of boosters for each series
                foreach (var series in allSeries)
                {
                    vm.Add(new SeriesWithBoosters()
                    {
                        Series = series,
                        Boosters = allBoosters.Where(b => string.Equals(b.SeriesID, series.SeriesID, StringComparison.OrdinalIgnoreCase))
                                              .OrderByDescending(b => b.ReleaseDate).ToList()
                    });
                }

                return View(vm);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Could not get a list of all series and boosters.";
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
