using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;
namespace RaceGame
{
    /// <summary>
    /// Health power up
    /// </summary>
    class HealthPU :PowerUp
    {
       
        protected ModelElement Model;
        PhysObj physObj;
        SceneNode controlNode;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public HealthPU(SceneManager mSceneMgr, Stat health):base(mSceneMgr)
        {
            this.Stat = health;
            increase = 10;
           
        }

        /// <summary>
        /// load  model, attach to physic object
        /// </summary>  
        protected override void LoadModel()
        {
            Model = new ModelElement(mSceneMgr, "boletus.mesh");
           
            Model.GameEntity.SetMaterialName("boletus");
            //mSceneMgr.RootSceneNode.AddChild(Model.GameNode);
            this.gameNode = Model.GameNode;
            gameNode.Scale(0.3f, 0.3f, 0.3f);
            controlNode = mSceneMgr.CreateSceneNode();
            controlNode.AddChild(gameNode);
            mSceneMgr.RootSceneNode.AddChild(controlNode);
            float radius = 5;

             controlNode.Position += radius * Vector3.UNIT_Y;
              gameNode.Position -= radius * Vector3.UNIT_Y;



            physObj = new PhysObj(radius, "Health", 0.5f, 0.8f, 0.5f);
            physObj.SceneNode = controlNode;
            controlNode.Position = (new Vector3((int)Mogre.Math.RangeRandom(-400, 400), 10, (int)Mogre.Math.RangeRandom(-400, 400)));
            physObj.Position = controlNode.Position;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            physObj.AddForceToList(new FrictionForce(physObj));
            Physics.AddPhysObj(physObj);

            
        }

        /// <summary>
        /// checks for collisons with player, sets to remove to true if collsion occurs
        /// </summary>    
        ///  <param name="objName">name of colliding object to check</param>
        public override void Update(FrameEvent evt)
        {
            if (!toRemove)
            {
                
              
                toRemove = (IsCollidingWith("Player"));
               
                base.Update(evt);
            }
        }

        /// <summary>
        /// checks collisions with player
        /// </summary>
        /// <param name="objName"> check collsions with this object</param>
        /// <returns> returns true of colliosn occurs with paramaterstring</returns>
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

        public override void Dispose()
        {

            base.Dispose();
            Physics.RemovePhysObj(physObj);
            physObj = null;
        }
    
    
    

    }
    
}
