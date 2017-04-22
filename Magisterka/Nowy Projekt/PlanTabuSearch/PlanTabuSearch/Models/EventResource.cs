using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanTabuSearch.Models
{
    public class EventResource
    {
        public int Id { get; set; }
        public Resource Resource { get; set; }
        public string Role { get; set; }
        public ResourceType ResourceType { get; set; }
        public int Workload { get; set; }

        public List<Event> Events { get; set; }
    }
}