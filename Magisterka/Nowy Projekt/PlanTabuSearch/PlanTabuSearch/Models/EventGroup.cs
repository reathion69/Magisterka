using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanTabuSearch.Models
{
    public class EventGroup
    {
        public int Id { get; set; }
        public string IdText { get; set; }
        public string Name { get; set; }
        public int MyProperty { get; set; }
        public EventType Type { get; set; }


        public List<Event> Event { get; set; }
    }

    public enum EventType
    {
        Course,
        Other
    }
}