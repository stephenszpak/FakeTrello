using FakeTrello.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FakeTrello.DAL // DAL = Data Access Layer
{
    public class FakeTrelloContext : ApplicationDbContext
    {
   
        public virtual DbSet<TrelloUser> Users { get; set; }
 
    }
}