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
                IEnumerable<PokemonRule> rules = _ruleManager.GetAllRules()
                                                             .Where(r => !string.Equals(r.RuleID,"none", StringComparison.OrdinalIgnoreCase));
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
                bool wasAdded = _ruleManager.AddRule(rule);
                if (wasAdded)
                {
                    return RedirectToAction(nameof(Details), new { id = rule.RuleID });
                }
                else
                {
                    return View(rule);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Could not save the new rule.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            try
            {
                var rule = _ruleManager.GetRuleByRuleID(id);
                return View(rule);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Could not get a pokemon card rule to edit.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, PokemonRule rule)
        {
            rule.RuleID = id;
            if (!ModelState.IsValid)
            {
                return View(rule);
            }

            try
            {
                bool wasUpdated = _ruleManager.EditRule(rule);
                if (wasUpdated)
                {
                    return RedirectToAction(nameof(Details), new { id = rule.RuleID });
                }
                else
                {
                    return View(rule);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Could not update the pokemon card rule.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
