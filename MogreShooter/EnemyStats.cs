using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaceGame
{
    /// <summary>
    /// class tracks enemy stats
    /// </summary>
    class EnemyStats: CharacterStats
    {
        public Stat Health
        {
            get { return health; }
        }

       
      



        /// <summary>
        /// initialise enemy health to 50
        /// </summary>
        protected override void InitStats()
        {
            base.InitStats();
            
            health.InitValue(20);
       
        }
    }
}
