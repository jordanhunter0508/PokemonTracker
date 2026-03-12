using DataDomain;
using LogicLayer;
using LogicLayerInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Website.Controllers
{
    public class AlternateArtController : Controller
    {
        IAltArtManager _altArtManager;

        public AlternateArtController()
        {
            _altArtManager = new AltArtManager();
        }

        // GET: AlternateArtController
        public ActionResult Index()
        {
            try
            {
                IEnumerable<AlternateArt> altArts = _altArtManager.GetActiveAlternateArts().Items;
                return View(altArts);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Could not get a list of active alternate arts.";
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: AbilityController
        // Displays a list of all deactive abilities by default
        public ActionResult DeactivatedList()
        {
            try
            {
                IEnumerable<AlternateArt> altArts = _altArtManager.GetDeactiveAlternateArts().Items;
                return View(altArts);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Could not get a list of deactivated alternate arts.";
                return RedirectToAction("Error", "Home");

            }
        }

        // GET: AlternateArtController/Details/5
        public ActionResult Details(string id)
        {
            try
            {
                AlternateArt altArt = _altArtManager.GetAlternateArtByID(id);
                return View(altArt);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = $"Could not get the alternate art {id}.";
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: AlternateArtController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AlternateArtController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AlternateArt alternateArt)
        {
            if (!ModelState.IsValid)
            {
                return View(alternateArt);
            }

            try
            {
                bool wasAdded = _altArtManager.AddAlternateArt(alternateArt);
                if (wasAdded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(alternateArt);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Something went wrong when trying to save the new alternate art." + "\n" +
                                       "Please make sure you haven't already added this alternate art.";
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: AlternateArtController/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                AlternateArt alternateArt = _altArtManager.GetAlternateArtByID(id);
                return View(alternateArt);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = $"Could not get the alternate art '{id}' for editing.";
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: AlternateArtController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, AlternateArt alternateArt)
        {
            alternateArt.AlternateArtID = id;

            if (!ModelState.IsValid)
            {
                return View(alternateArt);
            }

            try
            {
                bool wasUpdated = _altArtManager.EditAlternateArt(alternateArt);
                if (wasUpdated)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(alternateArt);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = $"Something went wrong when trying to update the alternate art '{alternateArt.AlternateArtID}'.";
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: AlternateArtController/Delete/5
        public ActionResult Deactivate(string id)
        {
            try
            {
                AlternateArt alternateArt = _altArtManager.GetAlternateArtByID(id);
                return View(alternateArt);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = $"Could not get the alternate art '{id}' for deactivation.";
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: AlternateArtController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deactivate(string id, AlternateArt alternateArt)
        {
            try
            {
                bool result = _altArtManager.DeactivateAlternateArt(id);

                if (result)
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
                ViewBag.DisplayError = $"Something went wrong when trying to deactivate alternate art '{id}'.";
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: AlternateArtController/Delete/5
        public ActionResult Reactivate(string id)
        {
            try
            {
                AlternateArt alternateArt = _altArtManager.GetAlternateArtByID(id);
                return View(alternateArt);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = $"Could not get the alternate art '{id}' for reactivation.";
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: AlternateArtController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reactivate(string id, AlternateArt alternateArt)
        {
            try
            {
                bool result = _altArtManager.ReactivateAlternateArt(id);

                if (result)
                {
                    return RedirectToAction(nameof(DeactivatedList));
                }
                else
                {
                    return View(id);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = $"Something went wrong when trying to reactivate alternate art '{id}'.";
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: AlternateArtController/Delete/5
        public ActionResult Delete(string id)
        {
            try
            {
                AlternateArt alternateArt = _altArtManager.GetAlternateArtByID(id);
                return View(alternateArt);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = $"Could not get the alternate art '{id}' for deletion.";
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: AlternateArtController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, AlternateArt alternateArt)
        {
            try
            {
                bool wasDeleted = _altArtManager.DeleteAlternateArt(id);

                if (wasDeleted)
                {
                    return RedirectToAction(nameof(DeactivatedList));
                }
                else
                {
                    return View(id);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = $"Something went wrong when trying to delete alternate art '{id}'.\n" +
                                        "Please make sure there are no cards with this alternate art before tyring a permanent deletion.";
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
