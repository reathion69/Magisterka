using PlanTabuSearch.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PlanTabuSearch.Data
{
    public class XMLLoader
    {
        PlanDBContext context = new PlanDBContext();
        TimetableArchive archiveToLoad;

        public void ReadFromXML(string dataPath)
        {
            archiveToLoad = new TimetableArchive();
            XDocument xdoc = XDocument.Load(dataPath);

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
                       .Concat(instance.TimeGroups.Where(x => time.Elements("TimeGroup")?.Where(y => ((string)y.Attribute("Reference")).Contains(x.IdText)).Any() == true).ToList()).ToList()
                   }
                  ).ToList();
            }
        }

        public void LoadToDatabase()
        {
            ReadFromXML("ArtificialSudoku4x4.xml");
            context.Instances.AddOrUpdate(archiveToLoad.Instances.ToArray());
            foreach (var instance in archiveToLoad.Instances)
            {
                context.TimeGroups.AddOrUpdate(instance.TimeGroups.ToArray());
                context.Times.AddOrUpdate(instance.Times.ToArray());
            }

            context.SaveChanges();
        }
    }
}