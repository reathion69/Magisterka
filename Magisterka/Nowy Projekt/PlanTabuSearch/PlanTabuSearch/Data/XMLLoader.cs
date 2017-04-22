using PlanTabuSearch.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace PlanTabuSearch.Data
{
    public class XMLLoader
    {
        PlanDBContext context = new PlanDBContext();

        public void LoadToDatabase()
        {
            context.ResourceTypes.AddOrUpdate(i => i.IdText,
              new ResourceType
              {
                 IdText = "R",
                 Name = "Room"
              },
              new ResourceType
              {
                  IdText = "T",
                  Name = "Teacher"
              },
               new ResourceType
               {
                   IdText = "G",
                   Name = "Groups"
               }

            );

            context.SaveChanges();
        }
    }
}