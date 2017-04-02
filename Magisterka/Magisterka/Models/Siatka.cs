using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magisterka.Models
{
    public class Siatka
    {
        public int SiatkaId { get; set; }
        public int IloscGodzin { get; set; }
        public Grupa Grupa { get; set; }
        public Prowadzacy Prowadzacy { get; set; }
        public Przedmiot Przedmiot { get; set; }
        public List<Plan> Plany { get; set; }
    }
}