using PlanZajecProsteUI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PlanZajecProsteUI.Data
{
    public class XMLLoader
    {
        PlanDBContext context = new PlanDBContext();
        TimetableArchive archiveToLoad;

        public void ReadFromXML(string dataPath)
        {
            archiveToLoad = new TimetableArchive();
            XDocument xdoc = XDocument.Load(dataPath);

            //Wczytanie Instances i Metadata
            archiveToLoad.Instances = (
                from inst in xdoc.Root.Element("Instances").Elements("Instance")
                select new Instance
                {
                    IdText = (string)inst.Attribute("Id"),

                    Metadata =
                    (
                        from meta in inst.Elements("MetaData")
                        select new MetaData
                        {
                            Name = (string)meta.Element("Name"),
                            Contributor = (string)meta.Element("Contributor"),
                            Date = (string)meta.Element("Date"),
                            Country = (string)meta.Element("Country"),
                            Description = (string)meta.Element("Description"),
                            Remarks = (string)meta.Element("Remarks")
                        }
                    ).FirstOrDefault(),
                }).ToList();

            foreach (var instance in archiveToLoad.Instances)
            {
                XElement inst = xdoc.Root.Elements("Instances").Elements("Instance").Where(x => (string)x.Attribute("Id") == instance.IdText).First();

                //Wczytanie TimeGroups
                instance.TimeGroups = (
                        from timegroup in inst.Element("Times").Element("TimeGroups").Elements("Week")
                        select new TimeGroup
                        {
                            IdText = (string)timegroup.Attribute("Id"),
                            Name = (string)timegroup.Element("Name"),
                            Type = TimeGroupsType.Week
                        }

                    ).ToList().Concat((
                        from timegroup in inst.Element("Times").Element("TimeGroups").Elements("Day")
                        select new TimeGroup
                        {
                            IdText = (string)timegroup.Attribute("Id"),
                            Name = (string)timegroup.Element("Name"),
                            Type = TimeGroupsType.Day
                        }

                    ).ToList()).ToList().Concat((
                        from timegroup in inst.Element("Times").Element("TimeGroups").Elements("TimeGroup")
                        select new TimeGroup
                        {
                            IdText = (string)timegroup.Attribute("Id"),
                            Name = (string)timegroup.Element("Name"),
                            Type = TimeGroupsType.Other
                        }

                    ).ToList()).ToList();


                //Wczytanie Times
                instance.Times =
                  (
                   from time in inst.Element("Times").Elements("Time")
                   select new Time
                   {
                       IdText = (string)time.Attribute("Id"),
                       Name = (string)time.Element("Name"),
                       TimeGroups = instance.TimeGroups.Where(x => ((string)time.Element("Day")?.Attribute("Reference"))?.Contains(x.IdText) == true || ((string)time.Element("Week")?.Attribute("Reference"))?.Contains(x.IdText) == true).ToList()
                       .Concat(instance.TimeGroups.Where(x => time.Element("TimeGroups")?.Elements("TimeGroup")?.Where(y => ((string)y.Attribute("Reference")).Contains(x.IdText)).Any() == true).ToList()).ToList()
                   }
                  ).ToList();


                //Wczytanie ResourceTypes
                instance.ResourceTypes =
                    (
                        from resType in inst.Element("Resources").Element("ResourceTypes").Elements("ResourceType")
                        select new ResourceType
                        {
                            IdText = (string)resType.Attribute("Id"),
                            Name = (string)resType.Element("Name"),
                        }
                    ).ToList();

                //Wczytanie ResourceGroups
                instance.ResourceGroups =
                   (
                       from resGroup in inst.Element("Resources").Element("ResourceGroups").Elements("ResourceGroup")
                       select new ResourceGroup
                       {
                           IdText = (string)resGroup.Attribute("Id"),
                           Name = (string)resGroup.Element("Name"),
                           Type = instance.ResourceTypes.Where(x => x.IdText.Contains((string)resGroup.Element("ResourceType").Attribute("Reference"))).FirstOrDefault()
                       }
                   ).ToList();

                //Wczytanie Resources
                instance.Resources =
                    (
                     from res in inst.Element("Resources").Elements("Resource")
                     select new Resource
                     {
                         IdText = (string)res.Attribute("Id"),
                         Name = (string)res.Element("Name"),
                         Type = instance.ResourceTypes.Where(x => x.IdText.Contains((string)res.Element("ResourceType").Attribute("Reference"))).FirstOrDefault(),
                         Groups = instance.ResourceGroups.Where(x => res.Element("ResourceGroups")?.Elements("ResourceGroup")?.Where(y => ((string)y.Attribute("Reference")).Contains(x.IdText)).Any() == true).ToList()
                     }
                    ).ToList();

                //Wczytanie EventGroups
                instance.EventGroups = (
                        from course in inst.Element("Events").Element("EventGroups").Elements("Course")
                        select new EventGroup
                        {
                            IdText = (string)course.Attribute("Id"),
                            Name = (string)course.Element("Name"),
                            Type = EventType.Course
                        }

                    ).ToList().Concat((
                        from eventGroup in inst.Element("Events").Element("EventGroups").Elements("EventGroup")
                        select new EventGroup
                        {
                            IdText = (string)eventGroup.Attribute("Id"),
                            Name = (string)eventGroup.Element("Name"),
                            Type = EventType.Other
                        }
                    ).ToList()).ToList();

                instance.Events = (
                        from eventR in inst.Element("Events").Elements("Event")
                        select new Event
                        {
                            IdText = (string)eventR.Attribute("Id"),
                            Color =  eventR.Attribute("Color")?.ToString(),
                            Name = (string)eventR.Element("Name"),
                            Duration = (int)eventR.Element("Duration"),
                            Workload = eventR.Element("Workload")?.ToString() != null ? (int)eventR.Element("Workload") : 0,
                            Time = instance.Times.Where(x => ((string)eventR.Element("Time")?.Attribute("Reference"))?.Contains(x.IdText) == true).FirstOrDefault(),
                            EventResources = (
                                from eRes in eventR.Element("Resources").Elements("Resource")
                                select new EventResource
                                {
                                    Role = (string)eRes.Element("Role"),
                                    Workload = eRes.Element("Workload")?.ToString() != null ? (int)eRes.Element("Workload") : 0,
                                    Resource = instance.Resources.Where(x => ((string)eRes.Attribute("Reference"))?.Contains(x.IdText) == true).FirstOrDefault(),
                                    ResourceType = instance.ResourceTypes.Where(x => ((string)eRes.Element("ResourceType")?.Attribute("Reference"))?.Contains(x.IdText) == true).FirstOrDefault()
                                }
                            ).ToList(),
                            ResourceGroups = instance.ResourceGroups.Where(x => eventR.Element("ResourceGroups")?.Elements("ResourceGroup")?.Where(y => ((string)y.Attribute("Reference")).Contains(x.IdText)).Any() == true).ToList(),
                            EventGroups = instance.EventGroups.Where(x => eventR.Element("EventGroups")?.Elements("EventGroup")?.Where(y => ((string)y.Attribute("Reference")).Contains(x.IdText)).Any() == true).ToList()
                             .Concat(instance.EventGroups.Where(x => ((string)eventR.Element("Course")?.Attribute("Reference"))?.Contains(((string)x.IdText)) == true).ToList()).ToList()
                        }
                    ).ToList();
            }
        }

        public void LoadToDatabase(string path)
        {
            ReadFromXML(path);
            context.Instances.AddOrUpdate(archiveToLoad.Instances.ToArray());         

            context.SaveChanges();            
        }
    }
}