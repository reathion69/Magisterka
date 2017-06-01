using PlanZajecProsteUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanZajecProsteUI.Code
{
    public class TabuSearch
    {
        public static int NeighborhoodSize;
        public static int TabuDuration;
        public static void GenerateStartSolutionForTimes(Instance instance)
        {
            Random r = new Random();
            foreach (var ev in instance.Events)
            {
                int randomIndex = r.Next(instance.Times.Count);
                ev.Time = instance.Times[randomIndex];
               // index++;
            }

            System.Diagnostics.Debug.WriteLine("Generated start solution.");
        }

        public static List<TabuItem> GenerateAvaiableMoveList(Instance instance)
        {
            List<TabuItem> avaiableList = new List<TabuItem>();

            foreach (var itemX in instance.Events)
            {
                foreach (var itemY in instance.Times)
                {
                    avaiableList.Add(new TabuItem(instance.Events.IndexOf(itemX), instance.Times.IndexOf(itemY), 0));
                }
            }


            System.Diagnostics.Debug.WriteLine("Generated available moves list. Avaiable moves: " + avaiableList.Count);

            return avaiableList;
        }

        public static void UpdateTabuList(List<TabuItem> tabuList, List<TabuItem> availableList)
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
                availableList.Add(item);
            }
        }

        public static Instance SelectBestInstanceFromNeighborhood(Instance instance, List<TabuItem> tabuList, List<TabuItem> availableList)
        {
            Random r = new Random();
            List<Instance> neighborhood = new List<Instance>();
            List<TabuItem> usedItems = new List<TabuItem>();
            for (int i = 0; i < NeighborhoodSize; i++)
            {
                if (availableList.Count > 0)
                {
                    TabuItem item = availableList.ElementAt(r.Next(availableList.Count));
                    availableList.Remove(item);
                    usedItems.Add(item);
                    Instance copy = (Instance)instance.Clone();
                    copy.Events[item.IndexX].Time = copy.Times[item.IndexY];

                    neighborhood.Add(copy);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Non moves available.");
                }
            }

            var inst = SelectBestInstanceFromGeneratedNeighborhood(neighborhood);

            var itemToTabu = usedItems[neighborhood.IndexOf(inst)];
            availableList.AddRange(usedItems);
            availableList.Remove(itemToTabu);
            itemToTabu.TabuDuration = TabuDuration;
            tabuList.Add(itemToTabu);

            return inst;
        }

        static Instance SelectBestInstanceFromGeneratedNeighborhood(List<Instance> neighborhood)
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