using DataDomain;
using LogicLayerInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Website.Models;

namespace Website.Controllers
{
    public class CardController : Controller
    {
        private readonly ICardManager _cardManager;
        private readonly IBoosterManager _boosterManager;
        private readonly IElementManager _elementManager;
        private readonly IArtistManager _artistManager;
        private readonly IAbilityManager _abilityManager;

        private const int PageSize = 24;

        public CardController(ICardManager cardManager, IBoosterManager boosterManager,
                              IElementManager elementManager, IArtistManager artistManager, 
                              IAbilityManager abilityManager)
        {
            _cardManager = cardManager;
            _boosterManager = boosterManager;
            _elementManager = elementManager;
            _artistManager = artistManager;
            _abilityManager = abilityManager;
        }

        // GET: CardController
        [HttpGet]
        public ActionResult Index(FilterOption filterOption, string filterTitle = "All", int pageNumber = 1)
        {
            ViewBag.FilterOption = filterOption;
            ViewBag.FilterTitle = filterTitle.Replace("-"," ");

            try
            {
                // Used to fill the select boxes for filters
                ViewBag.BoosterIDs = _boosterManager.GetBoosterIDs();
                ViewBag.Rarities = new string[] { "Common", "Full Art", "Gallery", "Illustration Rare", "Rare", "Secret Rare", "Ultra Rare", "Uncommon" };
                ViewBag.CardTypes = new string[] { "Item", "Pokemon", "Stage", "Trainer" };
                ViewBag.Elements = _elementManager.GetElementTypes().Select(e => e.ElementTypeID);

                var cards = _cardManager.GetCardsPaginated(filterOption, pageNumber, PageSize);
                ViewBag.PageNumber = cards.PageNumber;
                ViewBag.PageSize = cards.PageSize;
                ViewBag.TotalCount = cards.TotalCount;
                ViewBag.TotalPages = cards.TotalPages;

                var results = BuildCardListVM(cards.Items);
                return View(results);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Could not get a list all cards.";
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: CardController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                CardVM cardVM = _cardManager.GetCardVM(id);

                if (cardVM == null)
                {
                    TempData["ErrorMessage"] = "Card not found";
                    return NotFound();
                }

                // Gets Card related components for display
                ViewBag.Booster = _boosterManager.GetBoosterByBoosterID(cardVM.BoosterID);
                ViewBag.Ability = _abilityManager.GetAbilityByAbilityID(cardVM.AbilityID);
                ViewBag.ArtistName = _artistManager.GetArtistByArtistID(cardVM.ArtistID).Name;

                List<string> costs = new List<string>();

                for (int i = 0; i < cardVM.Moves.Count; i++)
                {
                    string cost = string.Concat(
                        cardVM.Moves[i].Costs.Select(c => new string(c.ElementType[0], c.Quantity))
                    );

                    costs.Add(cost.ToUpper());
                }

                ViewBag.MoveCosts = costs;

                return View(cardVM);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Could not find card.";
                return RedirectToAction("Error", "Home");
            }

        }

        // GET: CardController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CardController/Create
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

        // GET: CardController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CardController/Edit/5
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

        // GET: CardController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CardController/Delete/5
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

        private IEnumerable<CardListViewModel> BuildCardListVM(IEnumerable<Card> cards)
        {
            // Get distinct booster IDs
            var boosterIDs = cards.Select(c => c.BoosterID).Distinct();

            // Build a dictionary where the key is the boosterID,
            // and the value is the Booster object
            var boosterDict = boosterIDs
                .Select(id => _boosterManager.GetBoosterByBoosterID(id))
                .ToDictionary(b => b.BoosterID);

            var results = cards.Select(card => new CardListViewModel
            {
                Card = card,
                Booster = boosterDict.TryGetValue(card.BoosterID, out var booster) ? booster : null
            }).ToList();

            return results;
        }

        [HttpGet]
        public IActionResult test() { return View(); }
    }
}
