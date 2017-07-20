using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;
namespace RaceGame
{
    /// <summary>
    /// Shield power up
    /// </summary>
    class ShieldPU : PowerUp
    {
        ModelElement Model;
       
        PhysObj physObj;
        SceneNode controlNode;
        /// <summary>
        /// Constructor
        /// </summary>
        public ShieldPU(SceneManager mSceneMgr, Stat shield)
            : base(mSceneMgr)
        {
            this.Stat = shield;
            increase = 10;
            toRemove = false;
            stat.Decrease(30);
        }
      
        /// <summary>
        /// load shield model, attach to physic object
        /// </summary>    
        protected override void LoadModel()
        {
            Model = new ModelElement(mSceneMgr, "GlassOfBeer.mesh");

            Model.GameEntity.SetMaterialName("GlassOfBeer");
            this.gameNode = Model.GameNode;
            gameNode.Scale(1, 1, 1);
            controlNode = mSceneMgr.CreateSceneNode();
            controlNode.AddChild(gameNode);
            mSceneMgr.RootSceneNode.AddChild(controlNode);
            float radius = 8;




            physObj = new PhysObj(radius, "Shield", 0.5f, 0.8f, 0.5f);
            physObj.SceneNode = controlNode;
            controlNode.Position = (new Vector3((int)Mogre.Math.RangeRandom(-450, 450), 10, (int)Mogre.Math.RangeRandom(-450, 450)));
            physObj.Position = controlNode.Position;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            physObj.AddForceToList(new FrictionForce(physObj));
            Physics.AddPhysObj(physObj);

        }

        /// <summary>
        /// update method, check if need to be removed
        /// </summary>    
        ///  <param name="evt">Mogre Frame Event</param>
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
                    
                    isColliding = true;

                    break;
                }
            }
            return isColliding;
        }

        public override void Dispose()
        {

            base.Dispose();
            Physics.RemovePhysObj(physObj);
            physObj = null;
        }
    
    
    }

}