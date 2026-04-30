using System;
using DataDomain;
using LogicLayerInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Models;

namespace Website.Controllers
{
    public class BoosterController : Controller
    {
        private readonly IBoosterManager _boosterManager;
        private readonly ISeriesManager _seriesManager;
        private readonly ISearchManager _searchManager;

        public BoosterController(IBoosterManager boosterManager, ISearchManager searchManager,
                                 ISeriesManager seriesManager)
        {
            _boosterManager = boosterManager;
            _searchManager = searchManager;
            _seriesManager = seriesManager;
        }


        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                // Get each active series and booster
                var activeBoosters = _boosterManager.GetActiveBoosters();
                var activeSeries = _seriesManager.GetSeriesImagePaths();

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

        [HttpGet]
        public IActionResult Details(string id)
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
                return View(vm);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Could not get a list all cards.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Moderator")]
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
        [Authorize(Roles = "Admin,Moderator")]
        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Moderator")]
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
                _boosterManager.ActivateBooster(id, active);
                _boosterManager.ActivateCardsByBoosterID(id, active);

                return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = $"Could not change the boosters activation status.";
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
