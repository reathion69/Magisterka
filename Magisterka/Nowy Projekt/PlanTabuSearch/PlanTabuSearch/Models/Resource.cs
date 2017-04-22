using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanTabuSearch.Models
{
    public class Resource
    {
        public int Id { get; set; }
        public string IdText { get; set; }
        public string Name { get; set; }
        public ResourceType Type { get; set; }
        public List<ResourceGroup> Groups { get; set; }
    }
}