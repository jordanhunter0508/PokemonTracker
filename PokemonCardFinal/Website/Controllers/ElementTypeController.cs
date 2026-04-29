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
    public class ElementTypeController : Controller
    {
        private readonly IElementManager _elementManager;

        public ElementTypeController(IElementManager elementManager)
        {
            _elementManager = elementManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                IEnumerable<ElementType> elements = _elementManager.GetElementTypes();
                return View(elements);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Could not get a list of all element types.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            try
            {
                var element = _elementManager.GetElementTypeByElementTypeID(id);
                return View(element);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Could not get an element type's details.";
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
        public IActionResult Create(ElementType element)
        {
            if (!ModelState.IsValid)
            {
                return View(element);
            }
            try
            {
                bool wasAdded = _elementManager.AddElementType(element.ElementTypeID, element.Description);

                if (wasAdded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(element);
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
        public IActionResult Edit(string id)
        {
            try
            {
                var element = _elementManager.GetElementTypeByElementTypeID(id);
                return View(element);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Could not get an element type's details for editing.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, ElementType element)
        {
            if (!ModelState.IsValid)
            {
                return View(element);
            }
            try
            {
                bool wasUpdated = _elementManager.EditElementDescritpionByElementTypeID(element.ElementTypeID,element.Description);

                if (wasUpdated)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(element);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Could not update the element type.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Activate(string id, bool active)
        {
            try
            {
                _elementManager.ActivateElementType(id, active);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = $"Could not change the element type's activation status.";
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
