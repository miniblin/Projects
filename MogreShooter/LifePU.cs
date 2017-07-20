using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class LifePU : PowerUp
    {

        /// <summary>
        /// Life power up
        /// </summary>
        protected ModelElement Model;
        PhysObj physObj;
        SceneNode controlNode;
        /// <summary>
        /// Constructor
        /// </summary>
        public LifePU(SceneManager mSceneMgr, Stat life)
            : base(mSceneMgr)
        {
            this.Stat = life;
            increase = 1;
        }

        /// <summary>
        /// load  model, attach to physic object
        /// </summary>
        protected override void LoadModel()
        {
            Model = new ModelElement(mSceneMgr, "heart.mesh");

            Model.GameEntity.SetMaterialName("Heart");
           
            this.gameNode = Model.GameNode;
            gameNode.Scale(10, 10, 10);

            controlNode = mSceneMgr.CreateSceneNode();
            controlNode.AddChild(gameNode);
            mSceneMgr.RootSceneNode.AddChild(controlNode);
            float radius = 8;

          
            physObj = new PhysObj(radius, "Life", 0.5f, 0.8f, 0.5f);
            physObj.SceneNode = controlNode;
            controlNode.Position = (new Vector3((int)Mogre.Math.RangeRandom(-450, 450), 10, (int)Mogre.Math.RangeRandom(-450, 450)));
            physObj.Position = controlNode.Position;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            physObj.AddForceToList(new FrictionForce(physObj));
            Physics.AddPhysObj(physObj);


           
        }

       /// <summary>
       /// removes neccesary objects after collsions have occured
       /// </summary>
       /// <param name="evt"></param>
        public override void Update(FrameEvent evt)
        {
            if (!toRemove)
            {
                
              
                toRemove = (IsCollidingWith("Player"));
               
                base.Update(evt);
            }
        }

        /// <summary>
        /// checks for collisons with player, sets to remove to true if collsion occurs
        /// </summary>    
        ///  <param name="objName">name of colliding object to check</param>
        protected bool IsCollidingWith(string objName)
        {
            bool isColliding = false;
            foreach (Contacts c in physObj.CollisionList)
            {
                if (c.colliderObj.ID == objName || c.collidingObj.ID == objName)
                {
                     stat.Increase(increase);
                    //  Console.Out.WriteLine("colliding with gem");
                    isColliding = true;

                    break;
                }
            }
            return isColliding;
        }

        /// <summary>
        /// dispose of objects and physobj
        /// </summary>
        public override void Dispose()
        {

            base.Dispose();
            Physics.RemovePhysObj(physObj);
            physObj = null;
        }
    
    
    


    }

}