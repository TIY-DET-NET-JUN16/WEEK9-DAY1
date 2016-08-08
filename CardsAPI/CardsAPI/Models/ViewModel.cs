using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardsAPI.Models
{
    public class ViewModel
    {
        Deck CurrentDeck { get; set; }
        public List<Card> Hand1 { get; set; }
    }
}