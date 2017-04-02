using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Magisterka.Models
{
    public class PlanContext : DbContext
    {
        public DbSet<Grupa> Grupy { get; set; }
        public DbSet<Plan> Plany { get; set; }
        public DbSet<Siatka> Siatki { get; set; }
        public DbSet<Prowadzacy> Prowadzacy { get; set; }
        public DbSet<Przedmiot> Przedmioty { get; set; }
        public DbSet<Sala> Sale { get; set; }
    }
}