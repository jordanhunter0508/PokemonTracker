using DataDomain;
using LogicLayerInterfaces;
using Microsoft.AspNetCore.Mvc;
using Website.Models;

namespace Website.Controllers
{
    public class BoosterController : Controller
    {
        private readonly IBoosterManager _boosterManager;
        private readonly ISearchManager _searchManager;

        public BoosterController(IBoosterManager boosterManager, ISearchManager searchManager)
        {
            _boosterManager = boosterManager;
            _searchManager = searchManager;
        }

        // GET: BoosterController
        public ActionResult Index()
        {
            try
            {
                // Get each active series and booster
                var activeBoosters = _boosterManager.GetActiveBoosters();
                var activeSeries = _boosterManager.GetSeriesImagePaths();

                List<SeriesWithBoosters> vm = new List<SeriesWithBoosters>();

                // Gets a list of boosters for each series
                foreach (var series in activeSeries)
                {
                    vm.Add(new SeriesWithBoosters()
                    {
                        Series = series,
                        Boosters = activeBoosters.Where(b => string.Equals(b.SeriesID, series.SeriesID, StringComparison.OrdinalIgnoreCase))
                                                   .ToList()
                    });
                }

                return View(vm);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Could not get a list all cards.";
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: BoosterController/Details/5
        public ActionResult Details(string id)
        {
            try
            {
                FilterOption filter = new FilterOption()
                {
                    BoosterID = id,
                };

                BoosterDetailsVM vm = new BoosterDetailsVM();
                vm.Booster = _boosterManager.GetBoosterByBoosterID(id);   
                vm.Cards = _searchManager.GetCards(filter)
                                         .OrderBy(c => c.BoosterNumber).ToList();
                Console.WriteLine(filter.BoosterID);
                return View(vm);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Could not get a list all cards.";
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: BoosterController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BoosterController/Create
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

        // GET: BoosterController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BoosterController/Edit/5
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

        // GET: BoosterController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BoosterController/Delete/5
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
