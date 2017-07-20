using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaceGame
{
    /// <summary>
    /// stats of items stored on each level
    /// </summary>
    class LevelStats
    {

        private int numGems;
      private  int numHealthPu;
       private int numShieldPU;
      private  int numLivesPU;
      private  int numEnemies;
        private int numCollectableGuns;

       

        public int NumGems
        {
            get
            {
                return numGems;
            }

            set
            {
                numGems = value;
            }
        }

        public int NumHealthPu
        {
            get
            {
                return numHealthPu;
            }

            set
            {
                numHealthPu = value;
            }
        }

        public int NumShieldPU
        {
            get
            {
                return numShieldPU;
            }

            set
            {
                numShieldPU = value;
            }
        }

        public int NumLivesPU
        {
            get
            {
                return numLivesPU;
            }

            set
            {
                numLivesPU = value;
            }
        }

        public int NumEnemies
        {
            get
            {
                return numEnemies;
            }

            set
            {
                numEnemies = value;
            }
        }

        public int NumCollectableGuns
        {
            get
            {
                return numCollectableGuns;
            }

            set
            {
                numCollectableGuns = value;
            }
        }
    }
}
