using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magisterka.Models
{
    public class Sala
    {
        public Sala()
        {
            this.Przedmioty = new HashSet<Przedmiot>();
        }

        public int SalaId { get; set; }
        public int Numer { get; set; }
        public string Nazwa { get; set; }
        public ICollection<Przedmiot> Przedmioty { get; set; }
    }
}