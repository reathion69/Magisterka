using PlanZajecProsteUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace PlanZajecProsteUI.Code
{
    public class SolutionManager
    {

        const int iteration = 1000;
        const int neighborhoodSize = 300;
        const int tabuDuration = 200;


        PlanDBContext context = new PlanDBContext();
        public Instance instanceToResolve;
        public Instance bestInstance;
        Instance actualInstance;
        int bestRating = 0;

        public delegate void FunctionWithString(string s);
        public FunctionWithString WriteOnConsol;

        public delegate void FunctionWithBool(bool b);
        public FunctionWithBool EnableButtons;


        public void ResolveSimpleProblem(int Id)
        {
            EnableButtons(false);
            int resultIteration = 0;

            LoadEntityFromDB(Id);
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

                if (i % 10 == 0)
                    WriteOnConsol(Environment.NewLine + "Iteration: " + i + ", Actual best result: " + bestRating);
            }


            PrintSolutionOnConsol(instanceToResolve);
            PrintSolutionOnConsol(bestInstance);
            System.Diagnostics.Debug.WriteLine("&&& Result iteration: " + resultIteration);
            WriteOnConsol("\n &&& Result iteration: " + resultIteration);
            EnableButtons(true);
        }

        public void PrintSolutionOnConsol(Instance instanceToPrint)
        {
            Time timeForDuration2 = null;
            Time timeForDuration3 = null;
            foreach (var time in instanceToPrint.Times)
            {
                System.Diagnostics.Debug.WriteLine(time.Name + ":");
                foreach (var ev in instanceToPrint.Events)
                {
                    if (ev.Time == time)
                    {
                        System.Diagnostics.Debug.WriteLine(ev.Name);
                    }

                    if (ev.Time == timeForDuration2 && (ev.Duration == 2 || ev.Duration == 3))
                    {
                        System.Diagnostics.Debug.WriteLine(ev.Name + "Dur 2");
                    }

                    if (ev.Time == timeForDuration3 &&  ev.Duration == 3)
                    {
                        System.Diagnostics.Debug.WriteLine(ev.Name + "Dur 3");
                    }
                }

                System.Diagnostics.Debug.WriteLine("");

                timeForDuration3 = timeForDuration2;
                timeForDuration2 = time;
            }

            System.Diagnostics.Debug.WriteLine("*** Rating: " + EvaluationFunction.EvaluateInstance(instanceToPrint));
        }

        public void SaveBestInstance()
        {
            bestInstance.IdText += " Solved with result: " + bestRating;

            context.Instances.AddOrUpdate(bestInstance);
            context.SaveChanges();
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