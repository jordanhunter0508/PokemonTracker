using DataDomain;
using LogicLayer;
using LogicLayerInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Website.Controllers
{
    [Authorize(Roles = "Admin,Moderator")]
    [Route("Admin/[controller]/[action]")]
    public class AlternateArtController : Controller
    {
        IAltArtManager _altArtManager;

        public AlternateArtController(IAltArtManager altArtManager)
        {
            _altArtManager = altArtManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                IEnumerable<AlternateArt> altArts = _altArtManager.GetAllAlternateArt()
                                                                  .Where(a => !string.Equals(a.AlternateArtID,"none",StringComparison.OrdinalIgnoreCase));
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
        public IActionResult Details(string id)
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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AlternateArt alternateArt)
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
        public IActionResult Edit(string id)
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
        public IActionResult Edit(string id, AlternateArt alternateArt)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Activate(string id, bool active)
        {
            try
            {
                if (!active)
                {

                    _altArtManager.DeactivateAlternateArt(id);
                }
                else
                {
                    _altArtManager.ReactivateAlternateArt(id);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = $"Something went wrong when trying to change alternate art '{id}' active status.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id, AlternateArt alternateArt)
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
