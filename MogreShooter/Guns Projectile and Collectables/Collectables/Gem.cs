using System;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class Gem : Collectable
    {
        protected Stat score;
        protected int increase;
        protected bool toRemove;
        public bool ToRemove
        {
            get { return toRemove; }
        }
        protected Gem(SceneManager mSceneMgr, Stat score)
        {
            this.mSceneMgr = mSceneMgr;
            this.score = score;
        }
        public int Increase
        {
            get { return increase; }
        }

        protected virtual void LoadModel()
        {
            // The link with to phisics engine goes here
            // (ignore until week 8) ...
        }

        public override void Update(FrameEvent evt)
        {
            Animate(evt);

        
          
       

       
            // Collision detection with the player goes here
            // (ignore until week 8) ...

            base.Update(evt);
        }

        protected virtual bool IsCollidingWith(string objName)
        {
            return false;
        }

        public override void Animate(FrameEvent evt)
        {
            gameNode.Yaw(Mogre.Math.AngleUnitsToRadians(20) * evt.timeSinceLastFrame);
        }
       

        public override void Dispose()
        {
            
            base.Dispose();
        }

        
    }
}
