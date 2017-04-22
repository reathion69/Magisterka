using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanTabuSearch.Models
{
    public class ResourceGroup
    {
        public int Id { get; set; }
        public string IdText { get; set; }
        public string Name { get; set; }
        public ResourceType Type { get; set; }

        public List<Resource> Resources { get; set; }
        public List<Event> Events { get; set; }
    }
}