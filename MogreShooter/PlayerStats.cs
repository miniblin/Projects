using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaceGame
{
    /// <summary>
    /// store all stats for the player
    /// </summary>
    class PlayerStats:CharacterStats
    {
        protected Score score; 
        public Score Score
        {
            get { return score; }
        }


        public Stat Health
        {
            get { return health; }
        }

        public Stat Shield
        {
            get { return shield; }
        }
        public Stat Lives
        {
            get { return lives; }
        }

      

        
        protected override void InitStats()
        {
            base.InitStats();
            score = new Score();
            score.InitValue(0);
            health.InitValue(50);
            shield.InitValue(50);
            lives.InitValue(3);
        }
    }
}
