using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;
namespace RaceGame
{
    /// <summary>
    /// this is the blue gem class. a collecttable that procvides the player witha points boost
    /// </summary>    
    class BlueGem : Gem
    {
        ModelElement Model;
        PhysObj physObj;
        SceneNode controlNode;

        /// <summary>
        /// constructor sets the score and loads the gem model
        /// </summary>
        /// <param name="msceneMgr">Mogre scene manager</param>
        /// <param name="score">the stat that the gem will update</param>
        public BlueGem(SceneManager mSceneMgr, Stat score)
            : base(mSceneMgr, score)
        {
            increase = 10;
            LoadModel();
        }
       
        /// <summary>
        /// loads the gem model, creates a phys object and attatches the gem to it
        /// </summary>        
        protected override void LoadModel()
        {
            remove = false;
            base.LoadModel();
            Model = new ModelElement(mSceneMgr, "Gem.mesh");

            Model.GameEntity.SetMaterialName("blue");
            Model.GameNode.Scale(new Vector3(3f, 3f, 3f));
            Model.GameEntity.GetMesh().BuildEdgeList();
            gameNode = Model.GameNode;

            controlNode = mSceneMgr.CreateSceneNode();
            controlNode.AddChild(gameNode);
            mSceneMgr.RootSceneNode.AddChild(controlNode);
            float radius = 7;
            controlNode.Position += radius * Vector3.UNIT_Y;
            Model.GameNode.Position -= radius * Vector3.UNIT_Y;
            controlNode.SetPosition((int)Mogre.Math.RangeRandom(-480, 400), 100, (int)Mogre.Math.RangeRandom(-480, 400));

            //phys/////////////////////////////
            physObj = new PhysObj(radius, "Gem", 0.5f, 0.8f, 0.5f);
            physObj.SceneNode = controlNode;
            physObj.Position = controlNode.Position;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            physObj.AddForceToList(new FrictionForce(physObj));
            Physics.AddPhysObj(physObj);

            //phys//////////////////////////
           
        }

        /// <summary>
        /// checks for collisons with player, sets to remove to true if collsion occurs
        /// </summary>    
        ///  <param name="objName">name of colliding object to check</param>
        protected override bool IsCollidingWith(string objName)
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
        /// update metho
        /// </summary>    
        ///  <param name="evt">Mogre Frame Event</param>
        public override void Update(FrameEvent evt)
        {
           
            toRemove = IsCollidingWith("Player");
            base.Update(evt);
        }
        /// <summary>
        /// dispose of models and physobjects
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            Model.Dispose();
            Physics.RemovePhysObj(physObj);
            physObj = null;


            controlNode.DetachAllObjects();
            controlNode.Dispose();


        }

    }
}

