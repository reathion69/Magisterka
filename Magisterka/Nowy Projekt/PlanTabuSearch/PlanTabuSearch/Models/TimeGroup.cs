using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanTabuSearch.Models
{
    public class TimeGroup
    {
        public int Id { get; set; }
        public string IdText { get; set; }
        public string Name { get; set; }
        public TimeGroupsType Type { get; set; }
        public List<Time> Times { get; set; }
    }

    public enum TimeGroupsType
    {
        Week,
        Day,
        Other
    }
}