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

        const int iteration = 300;
        const int neighborhoodSize = 400;
        const int tabuDuration = 50;


        PlanDBContext context = new PlanDBContext();
        Instance instanceToResolve;
        Instance bestInstance;
        Instance actualInstance;
        int bestRating = 0;

        public void ResolveSimpleProblem()
        {
            int resultIteration = 0;

            LoadEntityFromDB(4);
            TabuSearch.NeighborhoodSize = neighborhoodSize;
            TabuSearch.TabuDuration = tabuDuration;

            List<TabuItem> tabuList = new List<TabuItem>();
            List<TabuItem> availableList = new List<TabuItem>();
            TabuSearch.GenerateStartSolutionForTimes(instanceToResolve);
            availableList = TabuSearch.GenerateAvaiableMoveList(instanceToResolve);
            bestInstance = instanceToResolve;
            actualInstance = instanceToResolve;
            bestRating = EvaluationFunction.EvaluateInstance(bestInstance);
            for (int i = 0; i < iteration; i++)
            {
                var tempInstance = TabuSearch.SelectBestInstanceFromNeighborhood(actualInstance, tabuList, availableList);
                if (tempInstance != null)
                    actualInstance = tempInstance;

                int rating = EvaluationFunction.EvaluateInstance(actualInstance);
                if (bestRating > rating)
                {
                    bestInstance = actualInstance;
                    bestRating = rating;
                }
                
                if (bestRating<=0)
                {
                    resultIteration = i;
                    break;
                }

                TabuSearch.UpdateTabuList(tabuList, availableList);
            }


            PrintSolutionOnConsol(instanceToResolve);
            PrintSolutionOnConsol(bestInstance);
            System.Diagnostics.Debug.WriteLine("&&& Result iteration: " + resultIteration);
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

        public void LoadEntityFromDB(int InstanceId)
        {
            instanceToResolve = context.Instances.Where(x => x.Id == InstanceId)
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