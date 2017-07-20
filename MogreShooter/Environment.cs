using System;
using Mogre;

namespace RaceGame
{
    /// <summary>
    /// This class implements the game environment
    /// </summary>
    class Environment
    {
        SceneManager mSceneMgr;             // This field will contain a reference of the scene manages
        RenderWindow mWindow;               // This field will contain a reference to the rendering window
        Light light;
        Ground ground;                      // This field will contain an istance of the ground object

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr">A reference to the scene manager</param>
        public Environment(SceneManager mSceneMgr, RenderWindow mWindow)
        {
            this.mSceneMgr = mSceneMgr;
            this.mWindow = mWindow;

            Load();                                 // This method loads  the environment
        }

        /// <summary>
        /// load sky,fog shadows and lights
        /// </summary>
        private void Load()
        {
            SetSky();
            SetFog();

            SetLights();
            SetShadows();
            ground = new Ground(mSceneMgr);
        }

        /// <summary>
        /// This method dispose of any object instanciated in this class
        /// </summary>
        public void Dispose()
        {
            ground.Dispose();
        }

        /// <summary>
        /// create a skybox
        /// </summary>
        private void SetSky()
        {
            Plane sky = new Plane(Vector3.NEGATIVE_UNIT_Y, -100);
            mSceneMgr.SetSkyPlane(true, sky, "Sky", 10, 5, true, 0.5f, 100, 100,
                ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME);

        }

        /// <summary>
        /// create fog
        /// </summary>
        private void SetFog()
        {
            ColourValue fadeColour = new ColourValue(0.9f, 0.9f, 1f);
            //mSceneMgr.SetFog(FogMode.FOG_LINEAR, fadeColour, 0.1f, 100, 1000);
            //mSceneMgr.SetFog(FogMode.FOG_EXP, fadeColour, 0.001f);
            mSceneMgr.SetFog(FogMode.FOG_EXP2, fadeColour, 0.0015f);
            mWindow.GetViewport(0).BackgroundColour = fadeColour;
        }

        private void SetLights()
        {
            mSceneMgr.AmbientLight = new ColourValue(0.3f, 0.3f, 0.3f);                 // Set the ambient light in the scene

            light = mSceneMgr.CreateLight();                                            // Set an instance of a light;

            //light.Type = Light.LightTypes.LT_DIRECTIONAL;                               // Sets the light to be a directional Light
            light.Direction = Vector3.NEGATIVE_UNIT_Y;                                  // Sets the light direction

            light.DiffuseColour = ColourValue.Red;                                      // Sets the color of the light
            light.Position = new Vector3(0, 130, 0);                                    // Sets the position of the light

            // light.Type = Light.LightTypes.LT_POINT;                                   // Sets the light to be a point light

            light.Type = Light.LightTypes.LT_SPOTLIGHT;                               // Sets the light to be a spot light
            light.SetSpotlightRange(Mogre.Math.PI / 4, Mogre.Math.PI / 2, 0.001f);    // Sets the spot light parametes

            float range = 1000;                                                       // Sets the light range
            float constantAttenuation = 0.0f;                                            // Sets the constant attenuation of the light [0, 1]
            float linearAttenuation = 0.0f;                                              // Sets the linear attenuation of the light [0, 1]
            float quadraticAttenuation = 0.0001f;                                     // Sets the quadratic  attenuation of the light [0, 1]

            light.SetAttenuation(range, constantAttenuation, linearAttenuation, quadraticAttenuation); // Not applicable to directional ligths
        }

        /// <summary>
        /// create shadows
        /// </summary>
        private void SetShadows()
        {
            // mSceneMgr.ShadowTechnique = ShadowTechnique.SHADOWTYPE_STENCIL_MODULATIVE;
            mSceneMgr.ShadowTechnique = ShadowTechnique.SHADOWTYPE_STENCIL_ADDITIVE;
        }
    }
}
