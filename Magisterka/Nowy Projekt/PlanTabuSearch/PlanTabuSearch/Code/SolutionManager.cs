using PlanTabuSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PlanTabuSearch.Code
{
    public class SolutionManager
    {
        PlanDBContext context = new PlanDBContext();
        Instance instanceToResolve;
        int rating = 0;

        public void ResolveSimpleProblem()
        {
            LoadEntityFromDB();
            TabuSearch.GenerateStartSolutionForTimes(instanceToResolve);
            rating = EvaluationFunction.EvaluateInstance(instanceToResolve);
            PrintSolutionOnConsol();
        }

        public void PrintSolutionOnConsol()
        {
            foreach (var time in instanceToResolve.Times)
            {
                System.Diagnostics.Debug.WriteLine(time.Name + ":");
                foreach (var ev in instanceToResolve.Events)
                {
                    if (ev.Time == time)
                    {
                        System.Diagnostics.Debug.WriteLine(ev.Name);
                    }
                }
                System.Diagnostics.Debug.WriteLine("");
            }

            System.Diagnostics.Debug.WriteLine("*** Rating: " + rating);
        }

        public void LoadEntityFromDB()
        {
            instanceToResolve = context.Instances.Where(x => x.Id == 1)
                                       .Include(x => x.Metadata)
                                       .Include(x => x.Resources
                                               .Select(y => y.Groups))
                                       .Include(x => x.ResourceTypes)
                                       .Include(x => x.TimeGroups
                                               .Select(y => y.Times))
                                       .Include(x => x.Times)
                                       .Include(x => x.Events
                                               .Select(y => y.EventResources))
                                       .Include(x => x.EventGroups
                                               .Select(y => y.Events))
                                       .Include(x => x.ResourceGroups
                                               .Select(y => y.Events))
                                       .ToList().FirstOrDefault();


            int a = 1;
        }
    }
}