using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magisterka.Models
{
    public class Prowadzacy
    {
        public int ProwadzacyId { get; set; }
        public string Nazwisko { get; set; }
        public string Imie { get; set; }
        public string StopienNaukowy { get; set; }
        public string Skrot { get; set; }
        public List<Siatka> Siatki { get; set; }

    }
}