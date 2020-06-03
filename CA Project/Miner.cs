using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CA_Project
{
    class Miner
    {
        public string name;
        public ProgressBar bar;
        public int value, valueMax;
        public float mineSpeed;
        public float mineRate;
        public int cost;
        public int earnedGold;
        public bool isActive;

        public Miner()
        {
        }
        public override string ToString()
        {
            return string.Format("{0} has earned {1} gold at {2} ", name, earnedGold, DateTime.Now) ;
        }
    }
}
