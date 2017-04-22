using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magisterka.Models
{
    public class PlanViewModel
    {
        public int PlanId { get; set; }
        public int Dzien { get; set; }
        public int Godzina { get; set; }
        public int IloscGodzin { get; set; }
        public string ZakresGodzin { get; set; }
        public string Grupa { get; set; }
        public string Prowadzacy { get; set; }
        public string Przedmiot { get; set; }
        public int Sala { get; set; }
    }
}