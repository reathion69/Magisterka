using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanTabuSearch.Models
{
    public class Event : ICloneable
    {
        public int Id { get; set; }
        public string IdText { get; set; }
        public string Color { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public int Workload { get; set; }
        public List<EventGroup> EventGroups { get; set; }
        public Time Time { get; set; }
        public List<EventResource> EventResources { get; set; }
        public List<ResourceGroup> ResourceGroups { get; set; }

        public object Clone()
        {
            return (Event)this.MemberwiseClone();
        }
    }
}