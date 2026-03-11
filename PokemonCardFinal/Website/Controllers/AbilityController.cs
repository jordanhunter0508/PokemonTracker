using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataDomain;
using LogicLayerInterfaces;
using LogicLayer;

namespace Website.Controllers
{
    public class AbilityController : Controller
    {
        IAbilityManager _abilityManager;
        public AbilityController() 
        {
            _abilityManager = new AbilityManager();
        }

        // GET: AbilityController/List
        public ActionResult List()
        {
            try
            {
                IEnumerable<Ability> abilities = _abilityManager.GetAbilities();
                return View(abilities);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Could not get a list of abilites.";
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: AbilityController/Details/5
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
            return View();
        }

        // POST: AbilityController/Create
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

        // GET: AbilityController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AbilityController/Edit/5
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

        // GET: AbilityController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AbilityController/Delete/5
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
