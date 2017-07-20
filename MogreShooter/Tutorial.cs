using Mogre;
using Mogre.TutorialFramework;
using System;
using PhysicsEng;
using System.Collections.Generic;
namespace RaceGame
{
    /// <summary>
    /// This is the root class of the application, from here levels, the input manager and player are loaded and the core update loop run.
    /// </summary>
    class Tutorial : BaseApplication
    {
        Physics physics;
        SceneNode cameraNode;
        RaceGame.Player player;
        RaceGame.InputsManager inputsManager = RaceGame.InputsManager.Instance;
        RaceGame.GameInterface gameHMD;
        Level level;


        public static void Main()
        {
            new Tutorial().Go();            // This method starts the rendering loop
        }


        /// <summary>
        /// This method create the initial scene, the physics are started along with input manager, camera ,player ,stats and interface
        /// </summary>
        protected override void CreateScene()

        {
            physics = new Physics();
            player = new RaceGame.Player(mSceneMgr);
            inputsManager.PlayerController = (RaceGame.PlayerController)player.Controller;
            cameraNode = mSceneMgr.CreateSceneNode();
            cameraNode.AttachObject(mCamera);
            player.Model.GameNode.AddChild(cameraNode);
            RaceGame.PlayerStats playerStats = player.Stats;

            level = new Level(mSceneMgr, player, mWindow);
            gameHMD = new RaceGame.GameInterface(mSceneMgr, mWindow, playerStats, player.PlayerArmoury, level);
            //  wall4.SetMaterialName("RainbowSpehere");
            physics.StartSimTimer();
        }

        /// <summary>
        /// This method destrois the scene
        /// </summary>
        protected override void DestroyScene()
        {
            level.clearLevel();
            cameraNode.DetachAllObjects();
            player.Model.Dispose();
            cameraNode.Dispose();
            gameHMD.Dispose();
            physics.Dispose();

        }

        /// <summary>
        /// This method create a new camera and places is behind the player
        /// </summary>
        protected override void CreateCamera()
        {
            mCamera = mSceneMgr.CreateCamera("PlayerCam");
            mCamera.Position = new Vector3(0, 30, -75);
            mCamera.LookAt(new Vector3(0, 0, 0));
            mCamera.NearClipDistance = 5;
            mCamera.FarClipDistance = 1000;
            mCamera.FOVy = new Degree(70);
            mCameraMan = new CameraMan(mCamera);
            mCameraMan.Freeze = true;


        }

        /// <summary>
        /// This method create a new viewport
        /// </summary>
        protected override void CreateViewports()
        {
            Viewport viewport = mWindow.AddViewport(mCamera);
            viewport.BackgroundColour = ColourValue.Black;
            mCamera.AspectRatio = viewport.ActualWidth / viewport.ActualHeight;

        }

        /// <summary>
        /// This method update the scene after a frame has finished rendering
        /// </summary>
        /// <param name="evt"></param>
        protected override void UpdateScene(FrameEvent evt)
        {
            if (level.levelRunning && !gameHMD.gameOver)
            {
                physics.UpdatePhysics(0.01f);
                player.Update(evt);
                mCamera.LookAt(player.Position);
                base.UpdateScene(evt);
                gameHMD.Update(evt);
                level.Update(evt);
            }

            else
            {
                gameHMD.DisplayGameOver();
            }
        }



        /// <summary>
        /// This method set create a frame listener to handle events before, during or after the frame rendering
        /// </summary>
        protected override void CreateFrameListeners()
        {
            base.CreateFrameListeners();
            mRoot.FrameRenderingQueued += new FrameListener.FrameRenderingQueuedHandler(inputsManager.ProcessInput);
        }

        /// <summary>
        /// This method initilize the inputs reading from keyboard adn mouse
        /// </summary>
        protected override void InitializeInput()
        {
            base.InitializeInput();
            int windowHandle;
            mWindow.GetCustomAttribute("WINDOW", out windowHandle);
            inputsManager.InitInput(ref windowHandle);

        }
    }
}