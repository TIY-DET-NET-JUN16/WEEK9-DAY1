using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Helpers;

namespace CardsAPI.Models
{
    public class Deck
    {
        public string deck_id { get; set; }
        public int remaining { get; set; }
        public bool success { get; set; }
        public bool shuffled { get; set; }
    }
}