using DataDomain;
using LogicLayerInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{
    [Authorize(Roles = "Admin,Moderator")]
    [Route("/Admin/[controller]/[action]")]
    public class PokemonRuleController : Controller
    {
        private readonly IRuleManager _ruleManager;

        public PokemonRuleController(IRuleManager ruleManager)
        {
            _ruleManager = ruleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                IEnumerable<PokemonRule> rules = _ruleManager.GetAllRules();
                return View(rules);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Could not get a list of all pokemon card rules.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            try
            {
                var rule = _ruleManager.GetRuleByRuleID(id);
                return View(rule);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Could not get a list of all pokemon card rules.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PokemonRule rule)
        {
            if (!ModelState.IsValid)
            {
                return View(rule);
            }

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
        public IActionResult Edit(string id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, PokemonRule rule)
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
        public IActionResult Delete(string id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id, PokemonRule rule)
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

        public IActionResult Activate(string id, bool active)
        {
            try
            {
                if (!active)
                {
                    _ruleManager.DeactivateRule(id);
                }
                else
                {
                    _ruleManager.ReactivateRule(id);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = $"Something went wrong when trying to change the rule '{id}' activation status.";
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
