using CardsAPIInClass.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CardsAPIInClass.Controllers
{
    public class CardController : Controller
    {
        HttpClient client = new HttpClient();
        string url = "http://deckofcardsapi.com/api/deck";
        Deck currentDeck;

        public CardController()
        {
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: Card
        public async Task<ActionResult> Index()
        {
            if (currentDeck == null || currentDeck.remaining == 0)
            {
                HttpResponseMessage responseMessage = await 
                    client.GetAsync(url + "/new/shuffle/?deck_count=1");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                    currentDeck = JsonConvert.DeserializeObject<Deck>(responseData);
                    return View(currentDeck);
                }
                return View("Error");
            }

            return View(currentDeck);
        }

        public async Task<ActionResult> DrawACard()
        {
            var deckId = Request.QueryString["deck_id"];

            HttpResponseMessage responseMessage = await
                client.GetAsync(url + "/" + deckId + "/draw/?count=1");

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var viewModel = JsonConvert.DeserializeObject<ViewModel>(responseData);

                if (viewModel.remaining == 0)
                    RedirectToAction("Index");

                return View(viewModel);
            }
            return View("Error");
        }
    }
}