using System;
using DataDomain;
using LogicLayer;
using LogicLayerInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Website.Controllers
{
    [Authorize(Roles = "Admin,Moderator")]
    [Route("/Admin/[controller]/[action]")]
    public class AbilityController : Controller
    {
        IAbilityManager _abilityManager;
        List<string> _abilityTypes;

        public AbilityController(IAbilityManager abilityManager)
        {
            _abilityManager = abilityManager;
            _abilityTypes = new List<string>()
            {
                "Ability",
                "Pokemon Power",
                "Support"
            };
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                // Gets all abilities except the none to avoid it being updated
                IEnumerable<AbilityVM> abilities = _abilityManager.GetAllAbilities()
                                                                  .Where(a => !string.Equals(a.AbilityID, "none", StringComparison.OrdinalIgnoreCase));

                return View(abilities);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Could not get a list of all abilites.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            try
            {
                Ability ability = _abilityManager.GetAbilityByAbilityID(id);
                return View(ability);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = $"Could not get the ability '{id}'";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.AbilityTypes = _abilityTypes;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Ability ability)
        {
            ViewBag.AbilityTypes = _abilityTypes;

            if (!ModelState.IsValid)
            {
                return View(ability);
            }
            try
            {
                bool wasAdded = _abilityManager.AddAbility(ability);
                if (wasAdded)
                {
                    return RedirectToAction(nameof(Details), new { id = ability.AbilityID });
                }
                else
                {
                    return View(ability);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Something went wrong when trying to save the new ability." + "\n" +
                                       "Please make sure you haven't already added this ability.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            ViewBag.AbilityTypes = _abilityTypes;

            // get the current ability then return view with the model
            try
            {
                Ability ability = _abilityManager.GetAbilityByAbilityID(id);
                return View(ability);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = $"Could not get the ability '{id}' for editing.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, Ability ability)
        {
            ViewBag.AbilityTypes = _abilityTypes;
            ability.AbilityID = id;

            if (!ModelState.IsValid)
            {
                return View(ability);
            }

            try
            {
                bool wasUpdated = _abilityManager.EditAbility(ability);
                if (wasUpdated)
                {
                    return RedirectToAction(nameof(Details), new { id = ability.AbilityID });
                }
                else
                {
                    return View(ability);
                }

            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Something went wrong when trying to update the ability '" + ability.AbilityID + "'.\n" +
                                       "Please make sure there isn't an ability with the same name and type.";
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

                    _abilityManager.DeactivateAbility(id);
                }
                else
                {
                    _abilityManager.ReactivateAbility(id);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = $"Something went wrong when trying to change the ability '{id}' activation status.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id)
        {
            try
            {
                Ability ability = _abilityManager.GetAbilityByAbilityID(id);
                return View(ability);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = $"Could not get the ability '{id}' for deletion.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                bool wasDeleted = _abilityManager.DeleteAbility(id);

                if (wasDeleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(id);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = $"Something went wrong when trying to delete ability '{id}'.\n" +
                                        "Please make sure there are no cards with this ability before tyring a permanent deletion.";
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
