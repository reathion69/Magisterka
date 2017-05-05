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
        Instance bestInstance;
        Instance actualInstance;
        int bestRating = 0;
        int iteration = 100;
        public void ResolveSimpleProblem()
        {
            LoadEntityFromDB();
            List<TabuItem> tabuList = new List<TabuItem>();
            TabuSearch.GenerateStartSolutionForTimes(instanceToResolve);
            bestInstance = instanceToResolve;
            actualInstance = instanceToResolve;
            bestRating = EvaluationFunction.EvaluateInstance(bestInstance);

            for (int i = 0; i < iteration; i++)
            {
                var neighberhood = TabuSearch.GenerateNeighborhood(actualInstance, tabuList);
                var tempInstance = TabuSearch.SelectBestInstanceFromNeighborhood(neighberhood);
                if (tempInstance != null)
                    actualInstance = tempInstance;

                int rating = EvaluationFunction.EvaluateInstance(actualInstance);
                if (bestRating > rating)
                {
                    bestInstance = actualInstance;
                    bestRating = rating;
                }

                TabuSearch.UpdateTabuList(tabuList);
            }


            PrintSolutionOnConsol(instanceToResolve);
            PrintSolutionOnConsol(bestInstance);
        }

        public void PrintSolutionOnConsol(Instance instanceToPrint)
        {
            foreach (var time in instanceToPrint.Times)
            {
                System.Diagnostics.Debug.WriteLine(time.Name + ":");
                foreach (var ev in instanceToPrint.Events)
                {
                    if (ev.Time == time)
                    {
                        System.Diagnostics.Debug.WriteLine(ev.Name);
                    }
                }
                System.Diagnostics.Debug.WriteLine("");
            }

            System.Diagnostics.Debug.WriteLine("*** Rating: " + EvaluationFunction.EvaluateInstance(instanceToPrint));
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