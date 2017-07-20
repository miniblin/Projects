using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;
namespace RaceGame
{
    /// <summary>
    /// cannonball class - cannonball projectile
    /// </summary>
    class CannonBall : Projectile    {
        ModelElement Model;        
        public PhysObj physObj;

        /// <summary>
        /// constructor initialises health and damage and speed
        /// </summary>
        /// <param name="mSceneMgr"></param>                       
        public CannonBall(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            healthDamage = 10;
            shieldDamage = 10;
            speed = 30;
            Load();
        }

       /// <summary>
       /// load model and attach to physics object
       /// </summary>
        protected override void Load()
        {
            base.Load();
            Model = new ModelElement(mSceneMgr, "Bomb.mesh");
           
            Model.GameEntity.SetMaterialName("WallMat");
            Model.GameNode.Scale(new Vector3(3f, 3f, 3f));
            gameNode = Model.GameNode;
          
            
            mSceneMgr.RootSceneNode.AddChild(gameNode);
           
            float radius = 8;
        
            gameNode.SetPosition(0,50,50);


           
            

            physObj = new PhysObj(radius, "CannonBall", 0.1f, 0.7f, 0.5f);
            physObj.SceneNode = gameNode;
            physObj.Position = gameNode.Position;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            physObj.AddForceToList(new FrictionForce(physObj));
            Physics.AddPhysObj(physObj);
            
          //  mSceneMgr.RootSceneNode.AddChild(Model.GameNode);
        }


        /// <summary>
        /// check if need ro remove collided objects
        /// </summary>
        /// <param name="evt"></param>
        public override void Update(FrameEvent evt)
        {
            if (!toRemove)
            {
                
               
                toRemove = (IsCollidingWith("Player")|| IsCollidingWith("Robot"));
                if (!remove && time.Milliseconds > 2*maxTime)
                {
                    toRemove = true;
                    //Dispose();
                    //remove = true;
                }
                base.Update(evt);
            }
        }

        public bool toRemove=false;

        /// <summary>
        /// checkcs collsions with objects, sets remove to true if collsion occurs
        /// </summary>
        /// <param name="objName">object to chck collsions against</param>
        /// <returns></returns>
        protected bool IsCollidingWith(string objName)
        {
            bool isColliding = false;
            foreach (Contacts c in physObj.CollisionList)
            {
                if (c.colliderObj.ID == objName || c.collidingObj.ID == objName)
                {
              
                    
                    isColliding = true;

                    break;
                }
            }
            return isColliding;
        }

        /// <summary>
        /// dispose of models and physobjects
        /// </summary>
        public override void Dispose()
        {

            base.Dispose();
            Physics.RemovePhysObj(physObj);
            physObj = null;
        }
    }
}
