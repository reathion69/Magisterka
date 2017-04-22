using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanTabuSearch.Models
{
    public class MetaData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Contributor { get; set; }
        public DateTime Date { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
    }
}