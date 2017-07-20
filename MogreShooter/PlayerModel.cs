using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    /// <summary>
    /// player model class assembles a model for the plpayer and attacches it to a physics objbect
    /// </summary>
    class PlayerModel:CharacterModel
    {

        ModelElement Model;
        ModelElement HullGroup;
        ModelElement WheelsGroup;
        ModelElement GunsGroup;
       
        ModelElement Power;
        ModelElement Hull;
        ModelElement Sphere;

        public PhysObj physObj;
        SceneNode controlNode;

        /// <summary>
        /// 
        /// player contructor calls methods to load and assemble the player model
        /// </summary>
        /// <param name="mSceneMgr"></param>
        public PlayerModel(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            LoadModelElements();
            AssembleModel();
            gameNode = Model.GameNode;

        }
        /// <summary>
        /// load indivudal model elements of the player
        /// </summary>
        protected override void LoadModelElements()
        {
            Model = new ModelElement(mSceneMgr);
            HullGroup = new ModelElement(mSceneMgr);
            WheelsGroup = new ModelElement(mSceneMgr);
            GunsGroup = new ModelElement(mSceneMgr);
            Power = new ModelElement(mSceneMgr, "PowerCells.mesh");
            Power.GameEntity.SetMaterialName("RainbowSphere");
            Hull = new ModelElement(mSceneMgr, "Main.mesh");
            Hull.GameEntity.SetMaterialName("BatteredRobot");
            Hull.GameEntity.GetMesh().BuildEdgeList();
            Sphere = new ModelElement(mSceneMgr, "Sphere.mesh");
            Sphere.GameEntity.SetMaterialName("RainbowSphere");

            controlNode = mSceneMgr.CreateSceneNode();
           

            base.LoadModelElements();
        }
        /// <summary>
        /// assemble player models together in scene graph. attactth to a physics object
        /// </summary>
        protected override void AssembleModel()
        {
            
            Model.GameNode.AddChild(HullGroup.GameNode);
            HullGroup.GameNode.AddChild(GunsGroup.GameNode);
            HullGroup.GameNode.AddChild(WheelsGroup.GameNode);
            HullGroup.GameNode.AddChild(Power.GameNode);
            HullGroup.GameNode.AddChild(Hull.GameNode);
            WheelsGroup.GameNode.AddChild(Sphere.GameNode);
           
            
            //physics/////////////////////////////////////////////////
            float radius = 10;
          
            mSceneMgr.RootSceneNode.AddChild(Model.GameNode);
        
          
            Model.GameNode.SetPosition(0, 100, -100);



           

           

            physObj = new PhysObj(radius, "Player", 0.5f, 0.5f, 2f);
            physObj.SceneNode = Model.GameNode;
            physObj.Position = Model.GameNode.Position;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            physObj.AddForceToList(new FrictionForce(physObj));
            Physics.AddPhysObj(physObj);
          /////////////////////  Physics////////////////////////////////////////////
           

            

            /////////////////////////////////////////////////////////////







            base.AssembleModel();
        }

        public override void DisposeModel()
        {
            Model.Dispose();
            HullGroup.Dispose();
            WheelsGroup.Dispose();
            GunsGroup.Dispose();
            Power.Dispose();
            Hull.Dispose();
            Sphere.Dispose();

            base.DisposeModel();
        }
        public void AttachGun(Gun gun)
        {
            if(GunsGroup.GameNode.NumChildren()!=0){
                GunsGroup.GameNode.RemoveAllChildren();
            }
            GunsGroup.GameNode.AddChild(gun.GameNode);
        }


      

      }
}
