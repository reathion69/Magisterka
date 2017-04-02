using Magisterka.Data;
using Magisterka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;

namespace Magisterka.Controllers
{
    public class HomeController : Controller
    {
        PlanContext context = new PlanContext();

        public ActionResult Index(string planGrupa, string planProwadzacy)
        {
            ViewBag.planGrupa = new SelectList(context.Grupy.OrderBy(x => x.Nazwa).Select(x => x.Skrot).ToList(), "OS");
            ViewBag.planProwadzacy = new SelectList(context.Prowadzacy.OrderBy(x => x.Nazwisko).Select(x => x.Skrot).ToList());

            List<PlanViewModel> planyVM = new List<PlanViewModel>();

            foreach (var item in context.Plany.Include(x => x.Siatka).ToList())
            {
                PlanViewModel planVM = new PlanViewModel();
                Siatka siatka = context.Siatki.Where(y => y.SiatkaId == item.Siatka.SiatkaId).Include(x => x.Grupa).Include(x => x.Prowadzacy).Include(x => x.Przedmiot).FirstOrDefault();
                Przedmiot przedmiot = context.Przedmioty.Where(y => y.PrzedmiotId == siatka.Przedmiot.PrzedmiotId).Include(y => y.Sale).FirstOrDefault();

                planVM.Godzina = item.Godzina;
                planVM.Dzien = item.Dzien;
                planVM.PlanId = item.PlanId;
                planVM.IloscGodzin = siatka.IloscGodzin;
                planVM.ZakresGodzin = item.Godzina + ":00 - " + (item.Godzina + siatka.IloscGodzin) + ":00";
                planVM.Prowadzacy = siatka.Prowadzacy.Skrot;
                planVM.Przedmiot = siatka.Przedmiot.Skrot;
                planVM.Grupa = siatka.Grupa.Skrot;
                planVM.Sala = przedmiot.Sale.First().Numer;               
                planyVM.Add(planVM);
            }

            if (planGrupa == null && planProwadzacy == null)
            {
                planGrupa = "OS";
            }

            if (!string.IsNullOrEmpty(planGrupa))
            {
                planyVM = planyVM.Where(x => x.Grupa == planGrupa).ToList();
            }

            if (!string.IsNullOrEmpty(planProwadzacy))
            {
                planyVM = planyVM.Where(x => x.Prowadzacy == planProwadzacy).ToList();
            }

            return View(planyVM.OrderBy(x => x.Godzina));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CreateData()
        {
            CreateDataForDB creator = new CreateDataForDB();
            creator.AddToDB();

            ViewBag.Message = "Dodano dane do bazy danych.";

            return View();
        }
    }
}