using System;
using Mogre;

namespace RaceGame
{
    class Cube
    {
        ManualObject manual;
        SceneManager mSceneMgr;
        /// <summary>
        /// create a cube of set dimensions
        /// </summary>
        /// <param name="mSceneMgr"></param>
        public Cube(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
        }

        /// <summary>
        /// get cube mesh of set height width and depth at a popsition
        /// </summary>
        /// <param name="position">startposition of cube</param>
        /// <param name="cubeName">name of cube</param>
        /// <param name="materialName"> a material</param>
        /// <param name="width">width of cube</param>
        /// <param name="height">height of cube</param>
        /// <param name="depth">depth of cubbe</param>
        /// <returns></returns>
        public MeshPtr getCube(Vector3 position,string cubeName, string materialName, float width, float height, float depth)
        {
            manual = mSceneMgr.CreateManualObject(cubeName);
            manual.Begin(materialName, RenderOperation.OperationTypes.OT_TRIANGLE_LIST);

            // --- Fills the Vertex buffer and define the texture coordinates for each vertex ---
            
            //--- Vertex 0 ---
            manual.Position(new Vector3(width, height, depth));
            manual.TextureCoord(1, 3);

            //Texture coordinates here!

            //--- Vertex 1 ---
            manual.Position(new Vector3(width, position.y, depth));
            manual.TextureCoord(3, 5);

            //Texture coordinates here!

            //--- Vertex 2 ---
            manual.Position(new Vector3(width, height, position.z));
            manual.TextureCoord(2, 3);

            //Texture coordinates here!

            //--- Vertex 3 ---
            manual.Position(new Vector3(width,position.y, position.z));
            manual.TextureCoord(3, 3);

            //Texture coordinates here!


            //--- Vertex 4 ---
            manual.Position(new Vector3(position.x, height,depth));
            manual.TextureCoord(0,2);
            //Texture coordinates here!

            //--- Vertex 5 ---
            manual.Position(new Vector3(position.x, position.y, depth));
            manual.TextureCoord(3, 1);
            //Texture coordinates here!

            //--- Vertex 6 ---
            manual.Position(new Vector3(position.x, height, position.z));
            manual.TextureCoord(2, 2);
            //Texture coordinates here!

            //--- Vertex 7 ---
            manual.Position(new Vector3(position.x, position.y, position.z));
            manual.TextureCoord(3, 2);
           
            //Texture coordinates here!


            // --- Fills the Index Buffer ---
            //--------Face 1----------
            manual.Index(2);
            manual.Index(1);
            manual.Index(0);

            manual.Index(3);
            manual.Index(1);
            manual.Index(2);

            //--------Face 2----------
            manual.Index(5);
            manual.Index(6);
            manual.Index(4);

            manual.Index(5);
            manual.Index(7);
            manual.Index(6);

            //--------Face 3----------
            manual.Index(1);
            manual.Index(4);
            manual.Index(0);

            manual.Index(5);
            manual.Index(4);
            manual.Index(1);

            //--------Face 4----------
            manual.Index(4);
            manual.Index(6);
            manual.Index(0);

            manual.Index(6);
            manual.Index(2);
            manual.Index(0);

            //--------Face 5----------
            manual.Index(3);
            manual.Index(2);
            manual.Index(6);

            manual.Index(7);
            manual.Index(3);
            manual.Index(6);

            //--------Face 5----------
            manual.Index(7);
            manual.Index(1);
            manual.Index(3);

            manual.Index(7);
            manual.Index(5);
            manual.Index(1);
           //manual.SetMaterialName(1, "WallMat");
           // manual.SetMaterialName(2, "WallMat");
           // manual.SetMaterialName(3, "WallMat");
           // manual.SetMaterialName(4, "WallMat");
           // manual.SetMaterialName(5, "WallMat");
            manual.End();
          
            return manual.ConvertToMesh(cubeName); 
        }

        
    }
}
