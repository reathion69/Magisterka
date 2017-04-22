using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magisterka.Models
{
    public class Plan
    {
        public int PlanId { get; set; }
        public int Dzien { get; set; }
        public int Godzina { get; set; }
        public Siatka Siatka { get; set; }
    }
}