using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FakeTrello.Models
{
    public class Card
    {
        [Key]
        public int CardId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ApplicationUser Owner { get; set; }
       
        public List BelongsTo { get; set; }

        public List<ApplicationUser> AttachedUsers { get; set; }

        public List<Collaborator> Collaborators { get; set; }
    }
}