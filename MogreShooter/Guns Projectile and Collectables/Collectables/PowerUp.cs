using System;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    abstract class PowerUp:Collectable
    {
        public bool toRemove;
        protected Stat stat;
        public Stat Stat
        {
            set { stat = value; }
        }

        protected PowerUp(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            LoadModel();
          
        }

        protected int increase;
        virtual protected void LoadModel() { 
        }

        public override void Update(FrameEvent evt)
        {

            // Collision detection with the player goes here
            // (ignore until week 8) ...
        }


    }
}
