using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CardsAPI.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace CardsAPI.Controllers
{
    public class CardController : Controller
    {
        HttpClient client;
        //The URL of the WEB API Service
        string url = "http://deckofcardsapi.com/api/deck/";
        Deck currentDeck;

        public CardController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ActionResult> Index()
        {
            if (currentDeck == null || currentDeck.remaining == 0)
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url + "new/shuffle/?deck_count=1");
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

        public async Task<ActionResult> DealCard()
        {
            var test = Request.QueryString["deck_id"];

            HttpResponseMessage responseMessage = await client.GetAsync(url + test + "/draw/?count=1");

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var currentDraw = JsonConvert.DeserializeObject<CardsAPIViewModel>(responseData);

                return View(currentDraw);
            }

            return View("Error");
        }
    }
}