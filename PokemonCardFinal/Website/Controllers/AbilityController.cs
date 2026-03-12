using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataDomain;
using LogicLayerInterfaces;
using LogicLayer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Website.Controllers
{
    public class AbilityController : Controller
    {
        IAbilityManager _abilityManager;
        List<string> _abilityTypes;

        public AbilityController()
        {
            _abilityManager = new AbilityManager();
            _abilityTypes = new List<string>()
            {
                "Ability",
                "Pokemon Power",
                "Support"
            };
        }

        // GET: AbilityController
        // Displays a list of all active abilities by default
        public ActionResult Index()
        {
            try
            {
                IEnumerable<Ability> abilities = _abilityManager.GetActiveAbilities().Items;
                return View(abilities);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Could not get a list of active abilites.";
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: AbilityController
        // Displays a list of all deactive abilities by default
        public ActionResult DeactivatedList()
        {
            try
            {
                IEnumerable<Ability> abilities = _abilityManager.GetDeactiveAbilities().Items;
                return View(abilities);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Could not get a list of deactivated abilites.";
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: AbilityController/Details/AbilityID
        public ActionResult Details(string id)
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

        // GET: AbilityController/Create
        public ActionResult Create()
        {
            ViewBag.AbilityTypes = _abilityTypes;
            return View();
        }

        // POST: AbilityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ability ability)
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
                    return RedirectToAction(nameof(Index));
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

        // GET: AbilityController/Edit/5
        public ActionResult Edit(string id)
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

        // POST: AbilityController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Ability ability)
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
                    return RedirectToAction(nameof(Index));
                }
                else 
                {
                    return View(ability);
                }
                
            }
            catch(Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Something went wrong when trying to update the ability '"+ ability.AbilityID + "'.\n" +
                                       "Please make sure there isn't an ability with the same name and type.";
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: AbilityController/Deactivate/5
        public ActionResult Deactivate(string id)
        {
            try
            {
                Ability ability = _abilityManager.GetAbilityByAbilityID(id);
                return View(ability);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = $"Could not get the ability '{id}' for deactivation.";
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: AbilityController/Deactivate/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deactivate(string id, IFormCollection collection)
        {
            try
            {
                bool wasDeleted = _abilityManager.DeactivateAbility(id);
                
                if (wasDeleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(id);
                }
            }
            catch(Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = $"Something went wrong when trying to deactivate the ability '{id}'.";
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: AbilityController/Deactivate/5
        public ActionResult Reactivate(string id)
        {
            try
            {
                Ability ability = _abilityManager.GetAbilityByAbilityID(id);
                return View(ability);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = $"Could not get the ability '{id}' for reactivation.";
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: AbilityController/Deactivate/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reactivate(string id, IFormCollection collection)
        {
            try
            {
                bool wasDeleted = _abilityManager.ReactivateAbility(id);
                
                if (wasDeleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(id);
                }
            }
            catch(Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = $"Something went wrong when trying to reactivate the ability '{id}'.";
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: AbilityController/Deactivate/5
        public ActionResult Delete(string id)
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

        // POST: AbilityController/Deactivate/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
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
                                        "Please make sure there are no cards with this ability before tyring a permant deletion.";
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
