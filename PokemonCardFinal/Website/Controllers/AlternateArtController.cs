using DataDomain;
using LogicLayer;
using LogicLayerInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Website.Controllers
{
    [Authorize]
    [Route("Admin/[controller]/[action]")]
    public class AlternateArtController : Controller
    {
        IAltArtManager _altArtManager;

        public AlternateArtController(IAltArtManager altArtManager)
        {
            _altArtManager = altArtManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Index()
        {
            try
            {
                IEnumerable<AlternateArt> altArts = _altArtManager.GetAllAlternateArt();
                return View(altArts);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Could not get a list of all alternate arts.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
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

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Moderator")]
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
                    return RedirectToAction(nameof(Details), new { id = alternateArt.AlternateArtID });
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

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Moderator")]
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
                    return RedirectToAction(nameof(Details), new { id = alternateArt.AlternateArtID });
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

        // POST: AlternateArtController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Activate(string id, bool active)
        {
            bool result = false;
            try
            {
                if (!active)
                {

                    result = _altArtManager.DeactivateAlternateArt(id);
                }
                else
                {
                    result = _altArtManager.ReactivateAlternateArt(id);
                }

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
                ViewBag.DisplayError = $"Something went wrong when trying to change alternate art '{id}' active status.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
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
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Delete(string id, AlternateArt alternateArt)
        {
            try
            {
                bool wasDeleted = _altArtManager.DeleteAlternateArt(id);

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
                ViewBag.DisplayError = $"Something went wrong when trying to delete alternate art '{id}'.\n" +
                                        "Please make sure there are no cards with this alternate art before tyring a permanent deletion.";
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
