using PlanZajecProsteUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanZajecProsteUI.Code
{
    public class EvaluationFunction
    {
        const int resourceEmptyPenalthy = 50;
        const int resourceConflictPenalthy = 10;
        const int eventIsSplitPenalthy = 23;
        const int isWindowPenalthy = 2;

        public static int EvaluateInstance(Instance instance)
        {
            int rating = 0;

            rating += CheckResourceConflict(instance);
            rating += CheckWidnowsExistForClass(instance);

            return rating;
        }

        public static int EvaluateInstanceWithRaport(Instance instance)
        {
            int rating = 0;

            int resConflict = CheckResourceConflict(instance);
            int windowExist = CheckWidnowsExistForClass(instance);
            rating += resConflict;
            rating += windowExist;
            System.Diagnostics.Debug.WriteLine("+++ Resources Conflict: " + resConflict + " *** Window Exists: " + windowExist);
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

        static int CheckWidnowsExistForClass(Instance instance)
        {
            List<Resource> Classes = instance.Resources.Where(x => x.Type.Name.Contains("Class")).ToList();
            List<TimeGroup> days = instance.TimeGroups.Where(x => x.Type == TimeGroupsType.Day).ToList();
            int rating = 0;

            foreach (var classObj in Classes)
            {
                foreach (var day in days)
                {
                    List<Time> times = instance.Times.Where(x => x.TimeGroups.Any(y => y == day)).ToList();
                    bool start = false;
                    bool end = false;
                    int size = 0;
                    int durationIterator = 0;
                    foreach (var time in times)
                    {
                        if (durationIterator <= 0)
                        {
                            Event ev = instance.Events.Where(x => x.Time == time && x.EventResources.Any(y => y.Resource == classObj)).FirstOrDefault();
                            if (ev != null)
                            {
                                durationIterator = ev.Duration;
                                start = true;
                                if (end)
                                {
                                    end = false;
                                    rating += size * isWindowPenalthy;
                                    size = 0;
                                }
                            }
                            else
                            {
                                if (start)
                                {
                                    end = true;
                                    size++;
                                }
                            }

                        }

                        durationIterator--;
                    }
                }
            }

            return rating;
        }

        static int CheckResourceConflict(Instance instance)
        {
            int rating = 0;
            Time timeForDuration2 = null;
            Time timeForDuration3 = null;
            foreach (var time in instance.Times)
            {
                List<Event> eventsOnTime = instance.Events.Where(x => x.Time == time).ToList();

                // Implementacja eventów o długości dłuższej niż 1 (maksymalna długość zaimplementowana to 3) 
                // ***********************************************
                List<Event> eventsToAddOnDuration2 = null;
                List<Event> eventsToAddOnDuration3 = null;

                if (timeForDuration2 != null)
                    eventsToAddOnDuration2 = (instance.Events.Where(x => x.Time == timeForDuration2 && (x.Duration == 2 || x.Duration == 3)).ToList());
                if (timeForDuration3 != null)
                    eventsToAddOnDuration3 = (instance.Events.Where(x => x.Time == timeForDuration3 && x.Duration == 3).ToList());


                if (timeForDuration2 != null && eventsToAddOnDuration2.Any() && time.TimeGroups.Where(x => x.Type == TimeGroupsType.Day).FirstOrDefault() != timeForDuration2.TimeGroups.Where(x => x.Type == TimeGroupsType.Day).FirstOrDefault())
                    rating += eventsToAddOnDuration2.Count * eventIsSplitPenalthy;

                if (timeForDuration3 != null && eventsToAddOnDuration3.Any() && time.TimeGroups.Where(x => x.Type == TimeGroupsType.Day).FirstOrDefault() != timeForDuration3.TimeGroups.Where(x => x.Type == TimeGroupsType.Day).FirstOrDefault())
                    rating += eventsToAddOnDuration3.Count * eventIsSplitPenalthy;
                if (eventsToAddOnDuration2 != null)
                    eventsOnTime.AddRange(eventsToAddOnDuration2);
                if (eventsToAddOnDuration3 != null)
                    eventsOnTime.AddRange(eventsToAddOnDuration3);
                // ***********************************************

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

                timeForDuration3 = timeForDuration2;
                timeForDuration2 = time;
            }

            return rating;
        }
    }
}