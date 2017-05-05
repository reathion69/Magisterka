using PlanTabuSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanTabuSearch.Code
{
    public class TabuSearch
    {
        static int NeighborhoodSize = 5;
        static int TabuDuration = 20;
        public static void GenerateStartSolutionForTimes(Instance instance)
        {
            Random r = new Random();
            int index = 0;
            foreach (var ev in instance.Events)
            {
               // int randomIndex = r.Next(instance.Times.Count);
                ev.Time = instance.Times[index/ instance.Times.Count];
                index++;

            }
        }

        public static void UpdateTabuList(List<TabuItem> tabuList)
        {
            List<TabuItem> toDelete = new List<TabuItem>();
            foreach (var item in tabuList)
            {
                item.TabuDuration--;
                if (item.TabuDuration < 0)
                    toDelete.Add(item);
            }
            foreach (var item in toDelete)
            {
                tabuList.Remove(item);
            }
        }

        public static List<Instance> GenerateNeighborhood(Instance instance, List<TabuItem> tabuList)
        {
            Random r = new Random();
            List<Instance> neighborhood = new List<Instance>();
            for (int i = 0; i < NeighborhoodSize; i++)
            {
                int x = 0;
                int y = 0;
                do
                {
                    x = r.Next(instance.Events.Count);
                    do
                    {
                        y = r.Next(instance.Events.Count);
                    } while (y == x);

                } while (tabuList.Where(t => t.IndexX == x && t.IndexY == y).Any());

                tabuList.Add(new TabuItem(x, y, TabuDuration));

                Instance copy = (Instance)instance.Clone();
                Time a = copy.Events[x].Time;
                copy.Events[x].Time = copy.Events[y].Time;
                copy.Events[y].Time = a;

                neighborhood.Add(copy);
            }

            return neighborhood;
        }

        public static Instance SelectBestInstanceFromNeighborhood(List<Instance> neighborhood)
        {
            int bestIndex = -1;
            int bestRating = -1;
             foreach (var instance in neighborhood)
            {
                int tempRating = EvaluationFunction.EvaluateInstance(instance);
                if (bestRating < 0 || bestRating > tempRating)
                {
                    bestRating = tempRating;
                    bestIndex = neighborhood.IndexOf(instance);
                }
            }

            return bestIndex < 0 ? null : neighborhood[bestIndex];
        }
    }
}