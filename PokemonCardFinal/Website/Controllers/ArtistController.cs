using System.Xml.Linq;
using DataDomain;
using LogicLayer;
using LogicLayerInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{
    [Authorize(Roles = "Admin,Moderator")]
    [Route("/Admin/[controller]/[action]")]
    public class ArtistController : Controller
    {
        private readonly IArtistManager _artistManager;

        public ArtistController(IArtistManager artistManager)
        { 
            _artistManager = artistManager;
        }

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                IEnumerable<Artist> artists = _artistManager.GetAllArtists().OrderBy(a => a.GivenName);
                return View(artists);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Could not get a list of all artists.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return View(artist);
            }
            try
            {
                bool wasAdded = _artistManager.AddArtist(artist.GivenName, artist.Surname);

                if (wasAdded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(artist);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Could not create a new element type.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                var artist = _artistManager.GetArtistByArtistID(id);
                return View(artist);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Could not get an artist to edit.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return View(artist);
            }
            try
            {
                bool wasUpdated = _artistManager.EditArtist(artist.ArtistID,artist.GivenName,artist.Surname);

                if (wasUpdated)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(artist);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Could not update the artist.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Activate(int id, bool active)
        {
            try
            {
                if (!active)
                {

                    _artistManager.DeactivateArtist(id);
                }
                else
                {
                    _artistManager.ReactivateArtist(id);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Something went wrong when trying to change an artist's active status.";
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
