using PlanTabuSearch.Data;
using PlanTabuSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlanTabuSearch.Controllers
{
    public class HomeController : Controller
    {
        PlanDBContext context = new PlanDBContext();
        public ActionResult Index()
        {
            return View();
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

        public ActionResult LoadFromXML()
        {
            XMLLoader loader = new XMLLoader();
            loader.LoadToDatabase();

            ViewBag.Message = "Dodano dane do bazy danych.";

            return View();
        }
    }
}