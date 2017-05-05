using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanTabuSearch.Models
{
    public class Instance: ICloneable
    {
        public int Id { get; set; }
        public string IdText { get; set; }
        public MetaData Metadata { get; set; }
        public List<Time> Times { get; set; }
        public List<TimeGroup> TimeGroups { get; set; }
        public List<Resource> Resources { get; set; }
        public List<ResourceType> ResourceTypes { get; set; }
        public List<ResourceGroup> ResourceGroups { get; set; }
        public List<Event> Events { get; set; }
        public List<EventGroup> EventGroups { get; set; }

        public object Clone()
        {
            Instance copy = (Instance)this.MemberwiseClone();

            List<Event> EventsCopy = new List<Event>();
            foreach (var item in Events)
            {
                EventsCopy.Add((Event)item.Clone());
            }

            copy.Events = EventsCopy;
            return copy;
        }
    }
}