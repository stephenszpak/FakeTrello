using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; //added using for [Key]
using System.Linq;
using System.Web;

// Entity is our Object Relational Mapper (ORM)

namespace FakeTrello.Models
{
    public class TrelloUser // <---Entity
    {
        [Key] // hover over red squiggly and add using DataAnnotations
        public int TrelloUserId { get; set; } //Primary Key

        [MinLength(10)]
        [MaxLength(60)] // can stack properties applying multiple annotations above the object you want to annotate
        public string Email { get; set; }

        [MinLength(10)]
        [MaxLength(60)]
        public string Username { get; set; }

        [MinLength(10)]
        [MaxLength(60)]
        public string FullName { get; set; }

        public ApplicationUser BaseUser { get; set; } // 1 to 1 relationship (ApplicationUser)

        public ICollection <Board> Boards { get; set; }  // 1 to Many (Boards) relationship

        public ICollection<EntityLink> AttributingUsers { get; set; }
    }
}