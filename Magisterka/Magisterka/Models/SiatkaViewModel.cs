using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magisterka.Models
{
    public class SiatkaViewModel
    {
        public int SiatkaId { get; set; }
        public int IloscGodzin { get; set; }
        public string Grupa { get; set; }
        public string Prowadzacy { get; set; }
        public string Przedmiot { get; set; }
    }
}