using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;
namespace RaceGame
{
    class Bomb : Projectile
    {
        ModelElement Model;
       
        public PhysObj physObj;
       

        /// <summary>
        /// Contructs the bomb model and initialises helath and shield variables
        /// </summary>
        /// <param name="mSceneMgr">Mogre sceneManager</param>
        public Bomb(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            healthDamage = 10;
            shieldDamage = 10;
            
            Load();
        }

        /// <summary>
        /// Loads model and creates a physics object
        /// </summary>
        protected override void Load()
        {
            base.Load();
            Model = new ModelElement(mSceneMgr, "Bomb.mesh");

            Model.GameEntity.SetMaterialName("WallMat2");
            Model.GameNode.Scale(new Vector3(3f, 3f, 3f));
            gameNode = Model.GameNode;
            mSceneMgr.RootSceneNode.AddChild(gameNode);
            
            float radius = 7;
            gameNode.SetPosition(0, 50, 50);
            
            physObj = new PhysObj(radius, "Bomb", 0.5f, 0.7f, 0.5f);
            physObj.SceneNode = gameNode;
            physObj.Position = gameNode.Position;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            physObj.AddForceToList(new FrictionForce(physObj));
            Physics.AddPhysObj(physObj);
            
        }
        /// <summary>
        /// update checkc collsions and time since creation
        /// </summary>
        /// <param name="evt"></param>
         public override void Update(FrameEvent evt)
        {
            if (!toRemove)
            {
                
               
                toRemove = (IsCollidingWith("Player")|| IsCollidingWith("Robot"));
                if (!remove && time.Milliseconds > 3*maxTime)
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
        /// check collsions with player or robot
        /// </summary>
        /// <param name="objName"></param>
        /// <returns>returns true if collsion occurs</returns>
        protected bool IsCollidingWith(string objName)
        {
            bool isColliding = false;
            foreach (Contacts c in physObj.CollisionList)
            {
                if (c.colliderObj.ID == objName || c.collidingObj.ID == objName)
                {
              
                    //  Console.Out.WriteLine("colliding with gem");
                    isColliding = true;

                    break;
                }
            }
            return isColliding;
        }
        /// <summary>
        /// Dispose Method removes all models and clears memory
        /// </summary>
        public override void Dispose()
        {

            base.Dispose();
            Physics.RemovePhysObj(physObj);
            physObj = null;
        }
    
    }
}