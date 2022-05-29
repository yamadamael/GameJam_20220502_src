using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gamejam
{
    public class GachaCompleteModel
    {
        public int CompReward;
        public List<int> CompTargets;

        public GachaCompleteModel()
        {
            CompTargets = new List<int>();
        }
    }
}
