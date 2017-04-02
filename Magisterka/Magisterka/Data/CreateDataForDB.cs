using Magisterka.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Magisterka.Data
{
    public class CreateDataForDB
    {
        PlanContext context = new PlanContext();

        public void AddToDB()
        {
            DodajGrupy();

            context.SaveChanges();
            DodajProwadzacych();

            context.SaveChanges();
            DodajPrzedmiotySale();

            context.SaveChanges();
            DodajSiatki();

            context.SaveChanges();
            DodajPlan();

            context.SaveChanges();
        }

        private void DodajGrupy()
        {
            context.Grupy.AddOrUpdate(i => i.Skrot,
              new Grupa
              {
                  Nazwa = "Oprogramowanie Systemowe",
                  Skrot = "OS",
                  Liczebnosc = 25,
              },
               new Grupa
               {
                   Nazwa = "Bazy Danych",
                   Skrot = "BD",
                   Liczebnosc = 21,
               },
                new Grupa
                {
                    Nazwa = "Grafika Trójwymiarowa",
                    Skrot = "GT",
                    Liczebnosc = 15,
                }

            );
        }

        private void DodajProwadzacych()
        {
            context.Prowadzacy.AddOrUpdate(i => i.Skrot,
                new Prowadzacy
                {
                    Nazwisko = "Myszor",
                    Imie = "Dariusz",
                    Skrot = "DM",
                    StopienNaukowy = "dr"
                },
                new Prowadzacy
                {
                    Nazwisko = "Paduch",
                    Imie = "Jarosław",
                    Skrot = "JP",
                    StopienNaukowy = "mgr"
                },
                  new Prowadzacy
                  {
                      Nazwisko = "Skowronek",
                      Imie = "Marcin",
                      Skrot = "MS",
                      StopienNaukowy = "dr"
                  }
                );
        }        

        private void DodajPrzedmiotySale()
        {
            Sala sala1 = new Sala
            {
                Numer = 1,
                Nazwa = "Sala komputerowa"
            };
            Sala sala2 = new Sala
            {
                Numer = 2,
                Nazwa = "Sala komputerowa"
            };
            Sala sala3 = new Sala
            {
                Numer = 3,
                Nazwa = "Sala komputerowa"
            };

            context.Przedmioty.AddOrUpdate(i => i.Skrot,
                new Przedmiot
                {
                    Nazwa = "Platforma .NET",
                    Skrot = "PNET",
                    Sale = new HashSet<Sala>() { sala1}
                },
                 new Przedmiot
                 {
                     Nazwa = "Modelowanie Cyfrowe",
                     Skrot = "MC",
                     Sale = new HashSet<Sala>() { sala2 }
                 },
                  new Przedmiot
                  {
                      Nazwa = "Projektowanie Robotów LEGO",
                      Skrot = "LEGO",
                      Sale = new HashSet<Sala>() { sala3 }
                  }
                  );
        }

        private void DodajSiatki()
        {
            context.Siatki.AddOrUpdate(
                new Siatka
                {
                    Przedmiot = context.Przedmioty.Where(x => x.Skrot == "MC").FirstOrDefault(),
                    Grupa = context.Grupy.Where(x => x.Skrot == "OS").FirstOrDefault(),
                    Prowadzacy = context.Prowadzacy.Where(x => x.Skrot == "MS").FirstOrDefault(),
                    IloscGodzin = 3
                },
                new Siatka
                {
                    Przedmiot = context.Przedmioty.Where(x => x.Skrot == "MC").FirstOrDefault(),
                    Grupa = context.Grupy.Where(x => x.Skrot == "GT").FirstOrDefault(),
                    Prowadzacy = context.Prowadzacy.Where(x => x.Skrot == "MS").FirstOrDefault(),
                    IloscGodzin = 3
                }, new Siatka
                {
                    Przedmiot = context.Przedmioty.Where(x => x.Skrot == "MC").FirstOrDefault(),
                    Grupa = context.Grupy.Where(x => x.Skrot == "BD").FirstOrDefault(),
                    Prowadzacy = context.Prowadzacy.Where(x => x.Skrot == "MS").FirstOrDefault(),
                    IloscGodzin = 3
                },
                new Siatka
                {
                    Przedmiot = context.Przedmioty.Where(x => x.Skrot == "PNET").FirstOrDefault(),
                    Grupa = context.Grupy.Where(x => x.Skrot == "OS").FirstOrDefault(),
                    Prowadzacy = context.Prowadzacy.Where(x => x.Skrot == "DM").FirstOrDefault(),
                    IloscGodzin = 4
                }, 
                new Siatka
                {
                    Przedmiot = context.Przedmioty.Where(x => x.Skrot == "PNET").FirstOrDefault(),
                    Grupa = context.Grupy.Where(x => x.Skrot == "GT").FirstOrDefault(),
                    Prowadzacy = context.Prowadzacy.Where(x => x.Skrot == "DM").FirstOrDefault(),
                    IloscGodzin = 2
                },
                new Siatka
                {
                    Przedmiot = context.Przedmioty.Where(x => x.Skrot == "PNET").FirstOrDefault(),
                    Grupa = context.Grupy.Where(x => x.Skrot == "BD").FirstOrDefault(),
                    Prowadzacy = context.Prowadzacy.Where(x => x.Skrot == "DM").FirstOrDefault(),
                    IloscGodzin = 2
                },
                new Siatka
                {
                    Przedmiot = context.Przedmioty.Where(x => x.Skrot == "LEGO").FirstOrDefault(),
                    Grupa = context.Grupy.Where(x => x.Skrot == "OS").FirstOrDefault(),
                    Prowadzacy = context.Prowadzacy.Where(x => x.Skrot == "JP").FirstOrDefault(),
                    IloscGodzin = 1
                },
                new Siatka
                {
                    Przedmiot = context.Przedmioty.Where(x => x.Skrot == "LEGO").FirstOrDefault(),
                    Grupa = context.Grupy.Where(x => x.Skrot == "BD").FirstOrDefault(),
                    Prowadzacy = context.Prowadzacy.Where(x => x.Skrot == "JP").FirstOrDefault(),
                    IloscGodzin = 1
                },
                new Siatka
                {
                    Przedmiot = context.Przedmioty.Where(x => x.Skrot == "LEGO").FirstOrDefault(),
                    Grupa = context.Grupy.Where(x => x.Skrot == "GT").FirstOrDefault(),
                    Prowadzacy = context.Prowadzacy.Where(x => x.Skrot == "JP").FirstOrDefault(),
                    IloscGodzin = 1
                }
                );

        }

        private void DodajPlan()
        {
            context.Plany.AddOrUpdate(
                new Plan
                {
                    Dzien = 1,
                    Godzina = 8,
                    Siatka = context.Siatki.Where(x => x.SiatkaId == 1).FirstOrDefault()
                },
                new Plan
                {
                    Dzien = 1,
                    Godzina = 13,
                    Siatka = context.Siatki.Where(x => x.SiatkaId == 4).FirstOrDefault()
                },
                 new Plan
                 {
                     Dzien = 1,
                     Godzina = 11,
                     Siatka = context.Siatki.Where(x => x.SiatkaId == 7).FirstOrDefault()
                 },
                 new Plan
                 {
                     Dzien = 1,
                     Godzina = 8,
                     Siatka = context.Siatki.Where(x => x.SiatkaId == 9).FirstOrDefault()
                 },
                 new Plan
                 {
                     Dzien = 1,
                     Godzina = 9,
                     Siatka = context.Siatki.Where(x => x.SiatkaId == 5).FirstOrDefault()
                 }, 
                 new Plan
                 {
                     Dzien = 1,
                     Godzina = 11,
                     Siatka = context.Siatki.Where(x => x.SiatkaId == 2).FirstOrDefault()
                 },
                 new Plan
                 {
                     Dzien = 1,
                     Godzina = 14,
                     Siatka = context.Siatki.Where(x => x.SiatkaId == 3).FirstOrDefault()
                 },
                 new Plan
                 {
                     Dzien = 1,
                     Godzina = 10,
                     Siatka = context.Siatki.Where(x => x.SiatkaId == 8).FirstOrDefault()
                 },
                 new Plan
                 {
                     Dzien = 1,
                     Godzina = 11,
                     Siatka = context.Siatki.Where(x => x.SiatkaId == 6).FirstOrDefault()
                 }
                );

        }
    }
}