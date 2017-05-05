using PlanTabuSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanTabuSearch.Code
{
    public class EvaluationFunction
    {
        const int resourceEmptyPenalthy = 50;
        const int resourceConflictPenalthy = 10;

        public static int EvaluateInstance(Instance instance)
        {
            int rating = 0;

            rating += CheckResourceConflict(instance);

            return rating;
        }

        static int CheckResourceEmpty(Instance instance)
        {
            int rating = 0;
            foreach (var time in instance.Times)
            {
                List<Event> eventsOnTime = instance.Events.Where(x => x.Time == time).ToList();
                foreach (var ev in eventsOnTime)
                {
                    foreach (var res in ev.EventResources)
                    {
                        if (res.Resource == null)
                            rating += resourceEmptyPenalthy;
                    }
                }
            }

            return rating;
        }

        static int CheckResourceConflict(Instance instance)
        {
            int rating = 0;
            foreach (var time in instance.Times)
            {
                List<Event> eventsOnTime = instance.Events.Where(x => x.Time == time).ToList();
                foreach (var ev in eventsOnTime)
                {
                    foreach (var res in ev.EventResources)
                    {
                        if (res.Resource != null)
                        {
                            var resList = eventsOnTime.Where(x => x.EventResources.Where(y => y.Resource == res.Resource).Any()).ToList();
                            if (resList.Count > 1)
                                rating += (resList.Count - 1) * resourceConflictPenalthy;
                        }
                    }
                }
            }

            return rating;
        }
    }
}