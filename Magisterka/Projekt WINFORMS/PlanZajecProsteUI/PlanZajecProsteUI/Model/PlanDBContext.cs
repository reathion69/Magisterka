using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PlanZajecProsteUI.Models
{
    public class PlanDBContext: DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<EventGroup> EventGroups { get; set; }
        public DbSet<EventResource> EventResources { get; set; }
        public DbSet<Instance> Instances { get; set; }
        public DbSet<MetaData> MetaDatas { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<ResourceGroup> ResourceGroups { get; set; }
        public DbSet<ResourceType> ResourceTypes { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<TimeGroup> TimeGroups { get; set; }
    }
}