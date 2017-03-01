using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FakeTrello.Models
{
    public class EntityLink
    {
        public ApplicationUser TrelloUser { get; set; }

        public int CardId { get; set; }
    }
}