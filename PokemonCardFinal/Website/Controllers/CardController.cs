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

        private const int PageSize = 24;

        public CardController(ICardManager cardManager,
                              IBoosterManager boosterManager,
                              IElementManager elementManager)
        {
            _cardManager = cardManager;
            _boosterManager = boosterManager;
            _elementManager = elementManager;
        }

        // GET: CardController
        [HttpGet]
        public ActionResult Index(string boosterFilter, string rarityFilter,
                                  string cardTypeFilter, string elementFilter,
                                  int pageNumber = 1)
        {
            // Set filters to ViewBag to display the
            // correct one in the select box when the page loads
            ViewBag.BoosterFilter = boosterFilter;
            ViewBag.RarityFilter = rarityFilter;
            ViewBag.CardTypeFilter = cardTypeFilter;
            ViewBag.ElementFilter = elementFilter;

            try
            {
                // Used to fill the select boxes for filters
                ViewBag.BoosterIDs = _boosterManager.GetBoosterIDs();
                ViewBag.Rarities = new string[] { "Common", "Full Art", "Gallery", "Illustration Rare", "Rare", "Secret Rare", "Ultra Rare", "Uncommon" };
                ViewBag.CardTypes = new string[] { "Item", "Pokemon", "Stage", "Trainer" };
                ViewBag.Elements = _elementManager.GetElementTypes().Select(e => e.ElementTypeID);

                // Save Filter options
                FilterOption filterOption = new FilterOption();
                if (!String.IsNullOrWhiteSpace(boosterFilter))
                {
                    filterOption.BoosterID = boosterFilter;
                }

                if (!String.IsNullOrWhiteSpace(rarityFilter))
                {
                    filterOption.Rarity = rarityFilter;
                }

                if (!String.IsNullOrWhiteSpace(cardTypeFilter))
                {
                    filterOption.CardType = cardTypeFilter;
                }

                if (!String.IsNullOrWhiteSpace(elementFilter))
                {
                    filterOption.ElementTypeID = elementFilter;
                }

                var cards = _cardManager.GetCardsPaginated(filterOption, pageNumber, PageSize);
                var results = BuildCardListVM(cards.Items);

                ViewBag.PageNumber = cards.PageNumber;
                ViewBag.PageSize = cards.PageSize;
                ViewBag.TotalCount = cards.TotalCount;
                ViewBag.TotalPages = cards.TotalPages;

                return View(results);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "Could not get a list all cards.";
                return RedirectToAction("Error", "Home");
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


        // GET: CardController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                CardVM cardVm = _cardManager.GetCardVM(id);
                return View(cardVm);
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
    }
}
