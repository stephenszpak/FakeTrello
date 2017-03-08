using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FakeTrello.Models
{
    public class EntityLink
    {
        [Key]
        public int EntityId { get; set; }
        public ApplicationUser TrelloUser { get; set; }

        public int CardId { get; set; }
    }
}