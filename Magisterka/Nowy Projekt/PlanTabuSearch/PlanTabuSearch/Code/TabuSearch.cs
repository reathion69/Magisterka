using PlanTabuSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanTabuSearch.Code
{
    public class TabuSearch
    {
        public static void GenerateStartSolutionForTimes(Instance instance)
        {
            Random r = new Random();
            foreach (var ev in instance.Events)
            {
                int randomIndex = r.Next(instance.Times.Count);
                ev.Time = instance.Times[randomIndex];
            }
        }
    }
}