using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FakeTrello.Models
{
    public class Board
    {
        [Key]
        public int BoardId { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }
        
        [MaxLength(255)]
        public string URL { get; set; }

        //auxiliary (i.e. not required to drive relationship, but helps readiblity)
        public TrelloUser Owner { get; set; }

        public ICollection<List> Lists { get; set; } //one to many linking to boards(one) to lists (many)
    }
}