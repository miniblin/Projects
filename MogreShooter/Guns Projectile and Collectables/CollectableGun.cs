using System;
using Mogre;
using PhysicsEng;

namespace RaceGame
{

    /// <summary>
    /// Collectable gun adds gun to players armoury when a collsion occurs
    /// </summary>
    class CollectableGun:Collectable
    {
        Gun gun;
        PhysObj physObj;
        SceneNode controlNode;
        public bool toRemove;
        public Gun Gun
        {
            get { return gun; }
        }
        
        Armoury playerArmoury;

        public Armoury PlayerArmoury
        {
            set { playerArmoury = value; }
        }

        /// <summary>
        /// contructor takes gun and armoury and initaliises them to localy stored varibables
        /// </summary>
        /// <param name="mSceneMgr">Mogre Scene Manager</param>
        /// <param name="gun"> Weapon</param>
        /// <param name="playerArmoury"> player armoury where weapons will be stored</param>
        public CollectableGun(SceneManager mSceneMgr, Gun gun, Armoury playerArmoury)
        {
            // Initialize here the mSceneMgr, the gun and the playerArmoury fields to the values passed as parameters
            this.mSceneMgr = mSceneMgr;
            this.gun = gun;
            this.playerArmoury = playerArmoury;
           
            
            // Initialize the gameNode here, scale it by 1.5f using the Scale funtion, and add as its child the gameNode contained in the Gun object.
            gameNode = mSceneMgr.CreateSceneNode();
          
            gameNode.AddChild(gun.GameNode);
            mSceneMgr.RootSceneNode.AddChild(gameNode);
            float radius = 8;
            gameNode.Position = (new Vector3(100, 100, 100));
            physObj = new PhysObj(radius, "collectableGun", 0.1f, 0.8f, 0.5f);
            physObj.SceneNode = gameNode;
            physObj.Position = gameNode.Position;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            physObj.AddForceToList(new FrictionForce(physObj));
            Physics.AddPhysObj(physObj);
           
        }

        /// <summary>
        /// Check for collsions with player
        /// and dispose of when collsion occurs
        /// </summary>
        /// <param name="evt">Mogre Frame event</param>
        public override void Update(FrameEvent evt)
        {
            Animate(evt);
            toRemove = false;
           foreach (Contacts c in physObj.CollisionList)
           {

               if (c.colliderObj.ID == "Player" || c.collidingObj.ID == "Player")
                {

                    toRemove = true;
                  
                    break;
                }
            }

           if (toRemove)
           {
               Console.Out.WriteLine("colliding with gun");
               (gun.GameNode.Parent).RemoveChild(gun.GameNode.Name);   //detach the gun model from the current node
               playerArmoury.AddGun(gun);
              // Physics.RemovePhysObj(physObj);
               
               Dispose();

           }
          
            base.Update(evt);
        }


        /// <summary>
        /// rotates the collectable gun
        /// </summary>
        /// <param name="evt">Mogre frame event </param>
        public override void Animate(FrameEvent evt)
        {
            gameNode.Rotate(new Quaternion(Mogre.Math.AngleUnitsToRadians(evt.timeSinceLastFrame*10), Vector3.UNIT_Y));
        }


        public override void Dispose()
        {
            base.Dispose();
            
            Physics.RemovePhysObj(physObj);
            physObj = null;


            gameNode.DetachAllObjects();
            gameNode.Dispose();


        }
    }
}
