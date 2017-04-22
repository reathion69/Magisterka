using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magisterka.Models
{
    public class Przedmiot
    {
        public Przedmiot()
        {
            this.Sale = new HashSet<Sala>();
        }

        public int PrzedmiotId { get; set; }
        public string Nazwa { get; set; }
        public string Skrot { get; set; }
        public ICollection<Sala> Sale { get; set; }
        public List<Siatka> Siatki { get; set; }

    }
}