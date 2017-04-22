using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magisterka.Models
{
    public class Grupa
    {
        public int GrupaId { get; set; }
        public string Nazwa { get; set; }
        public string Skrot { get; set; }
        public int Liczebnosc { get; set; }
        public List<Siatka> Siatki { get; set; }
    }
}