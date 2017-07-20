using System;
using Mogre;
using PhysicsEng;
namespace RaceGame
{
    /// <summary>
    /// This class implements the ground of the environment along withthe bounding contrainer of the environment
    /// </summary>
    class Ground
    {
        SceneManager mSceneMgr;
        Entity groundEntity;        
        SceneNode groundNode;       

        int groundWidth = 10;
        int groundHeight = 10;
        
        int uTiles = 10;
        int vTiles = 10;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr">A reference of the scene manager</param>
        public Ground(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            groundWidth = 1000;
            groundHeight = 1000;
            CreateGround();
        }

        /// <summary>
        /// This method initializes the ground mesh and node
        /// </summary>
        private void CreateGround()
        {
            GroundPlane();
            groundNode = mSceneMgr.CreateSceneNode();
            groundNode.AttachObject(groundEntity);
            mSceneMgr.RootSceneNode.AddChild(groundNode);
                  
           
        }

        /// <summary>
        /// This method generate a plane in an Entity which will be used as ground plane
        /// Four wall planes are also genreated to keep the player within bounds
        /// </summary>
        private void GroundPlane()
        {
            Plane plane;
            MeshPtr groundMeshPtr;
            plane = new Plane(Vector3.UNIT_Y, 0);
            groundMeshPtr = MeshManager.Singleton.CreatePlane("ground", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME, plane, groundWidth, groundHeight, 10, 10, true, 1, uTiles, vTiles, Vector3.UNIT_Z);
            
            groundEntity = mSceneMgr.CreateEntity("ground");
            groundEntity.SetMaterialName("Ground");

            Plane plane2;
            plane2 = new Plane(Vector3.UNIT_Z, -500);
            Plane plane3;
            plane3 = new Plane(-Vector3.UNIT_Z, -500);
            Plane plane4;
            plane4 = new Plane(Vector3.UNIT_X, -500);
            Plane plane5;
            plane5 = new Plane(-Vector3.UNIT_X, -500);


            Physics.AddBoundary(plane);
            Physics.AddBoundary(plane2);
            Physics.AddBoundary(plane3);
            Physics.AddBoundary(plane4);
            Physics.AddBoundary(plane5);
                     
        }

        /// <summary>
        /// This method disposes of the scene node and enitity
        /// </summary>
        public void Dispose()
        {
            groundNode.DetachAllObjects();
            groundNode.Parent.RemoveChild(groundNode);
            groundNode.Dispose();
            groundEntity.Dispose();
        }
              
    }
}
