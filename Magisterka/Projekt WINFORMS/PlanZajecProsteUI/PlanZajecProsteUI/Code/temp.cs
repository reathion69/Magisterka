using PlanZajecProsteUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace PlanZajecProsteUI.Code
{
    public class tmp
    {

        public int iteration = 1000;
        public int neighborhoodSize = 300;
        public int tabuDuration = 800;


        PlanDBContext context = new PlanDBContext();
        public Instance instanceToResolve;
        public Instance bestInstance;
        Instance actualInstance;
        int bestRating = 0;

        public delegate void FunctionWithString(string s);
        public FunctionWithString WriteOnConsol;

        public delegate void FunctionWithBool(bool b);
        public FunctionWithBool EnableButtons;

        List<int> raport = new List<int>();

        public void ResolveSimpleProblem(int Id)
        {
            int resultIteration = 0;
            LoadEntityFromDB(Id); //wczytanie danych z bazy.

            List<TabuItem> tabuList = new List<TabuItem>();
            List<TabuItem> availableList = new List<TabuItem>();
            actualInstance = (Instance)instanceToResolve.Clone();
            
            //Generowanie rozwiązania początkowego
            TabuSearch.GenerateStartSolutionForTimes(actualInstance);
            //Generowanie dostępnych ruchów
            availableList = TabuSearch.GenerateAvaiableMoveList(actualInstance);
            //Ocena rozwiązania startowego
            bestRating = EvaluationFunction.EvaluateInstance(actualInstance);
            for (int i = 0; i < iteration; i++)
            {
                //Generacja sąsiedztwa i wybór najlepszego sąsiada.
                var tempInstance = TabuSearch.SelectBestInstanceFromNeighborhood
                                        (actualInstance, tabuList, availableList);
                if (tempInstance != null)
                    actualInstance = tempInstance;

                //Tworzenie raportu i porównanie najlepszych rozwiązań.
                int rating = EvaluationFunction.EvaluateInstance(actualInstance);
                raport.Add(rating);
                if (bestRating > rating)
                {
                    bestInstance = actualInstance;
                    bestRating = rating;
                }

                //Warunek końca.
                if (bestRating <= 0)
                {
                    resultIteration = i;
                    break;
                }

                TabuSearch.UpdateTabuList(tabuList, availableList);
            }
                     
            EvaluationFunction.EvaluateInstanceWithRaport(bestInstance);
            SaveRaportToFile();
        }

        public void SaveRaportToFile()
        {
            string text = "";
            System.IO.StreamWriter file = new System.IO.StreamWriter("D:\\raportPlan.txt");
            foreach (var item in raport)
            {
                text += item.ToString() + "\n";
            }

            file.WriteLine(text);

            file.Close();
        }

        public void EvaluateBestInstanceWithRaport()
        {
            EvaluationFunction.EvaluateInstanceWithRaport(bestInstance);
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

                    if (ev.Time == timeForDuration3 && ev.Duration == 3)
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

        }
    }

    public class temp2
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
            }            
        }

        public static List<TabuItem> GenerateAvaiableMoveList(Instance instance)
        {
            List<TabuItem> avaiableList = new List<TabuItem>();

            foreach (var itemX in instance.Events)
            {
                foreach (var itemY in instance.Times)
                {
                    avaiableList.Add(new TabuItem
                    (instance.Events.IndexOf(itemX), instance.Times.IndexOf(itemY), 0));
                }
            }            

            return avaiableList;
        }

        public static void UpdateTabuList
        (List<TabuItem> tabuList, List<TabuItem> availableList)
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

        public static Instance SelectBestInstanceFromNeighborhood
        (Instance instance, List<TabuItem> tabuList, List<TabuItem> availableList)
        {
            Random r = new Random();
            List<Instance> neighborhood = new List<Instance>();
            List<TabuItem> usedItems = new List<TabuItem>();
            //Generacja sąsiedztwa
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

            //Wybranie najelpszego sąsiada
            var inst = SelectBestInstanceFromGeneratedNeighborhood(neighborhood);

            var itemToTabu = usedItems[neighborhood.IndexOf(inst)];
            availableList.AddRange(usedItems);
            availableList.Remove(itemToTabu);
            itemToTabu.TabuDuration = TabuDuration;
            tabuList.Add(itemToTabu);

            return inst;
        }

        static Instance SelectBestInstanceFromGeneratedNeighborhood
        (List<Instance> neighborhood)
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