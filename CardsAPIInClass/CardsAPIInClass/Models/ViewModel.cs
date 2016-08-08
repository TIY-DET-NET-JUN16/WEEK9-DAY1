using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardsAPIInClass.Models
{
    public class ViewModel
    {
        public bool success { get; set; }
        public List<Card> cards { get; set; }
        public string deck_id { get; set; }
        public int remaining { get; set; }
    }
}