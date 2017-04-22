using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanTabuSearch.Models
{
    public class Time
    {
        public int Id { get; set; }
        public string IdText { get; set; }
        public string Name { get; set; }
        public List<TimeGroup> TimeGroups { get; set; }
    }
}