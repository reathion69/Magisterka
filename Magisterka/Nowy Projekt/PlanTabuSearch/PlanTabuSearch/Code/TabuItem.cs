using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanTabuSearch.Code
{
    public class TabuItem
    {
        public int IndexX;
        public int IndexY;
        public int TabuDuration;

        public TabuItem(int x, int y, int duration)
        {
            IndexX = x;
            IndexY = y;
            TabuDuration = duration;
        }
    }
}