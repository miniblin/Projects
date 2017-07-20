using System;
using Mogre;
using System.Collections.Generic;
using PhysicsEng;

namespace RaceGame
{
    /// <summary>
    /// This class implements a robot
    /// </summary>
    class Robot: Enemy
    {
        Player player;
        PhysObj physObj;
        SceneNode controlNode;

      
        SceneManager mSceneMgr;     // A reference to the scene manager

        Entity robotEntity;         // The entity which will contain the robot mesh
        SceneNode robotNode;        // The node of the scene graph for the robot

        Radian angle;           // Angle for the mesh rotation
        Vector3 direction;      // Direction of motion of the mesh for a single frame
        float radius;           // Radius of the circular trajectory of the mesh
        
        float maxTime ;        // Time when the animation have to be changed
        Timer time;                         // Timer for animation changes
        AnimationState animationState;      // Animation state, retrieves and store an animation from an Entity
        bool animationChanged;              // Flag which tells when the mesh animation has changed
        string animationName;
        EnemyStats stats;

        public List<Projectile> projectiles;
        public List<Projectile> projectilesToRemove;
        
        // Name of the animation to use
        
        /// <summary>
        /// Write only. This property allows to change the animation 
        /// passing the name of one of the animations in the animation state set
        /// </summary>
        public string AnimationName
        {
            set
            {
                HasAnimationChanged(value);
                if (IsValidAnimationName(value))
                    animationName = value;
               // else

                   // animationName = "Idle";
            }
        }

        /// <summary>
        /// Read only. This property gets the postion of the robot in the scene
        /// </summary>
        public Vector3 Position
        {
            get { return robotNode.Position; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr">A reference to the scene manager</param>
        public Robot(SceneManager mSceneMgr, Player player)
        {
            this.mSceneMgr = mSceneMgr;
            Load();
            AnimationSetup();
            this.player = player;
            stats = new EnemyStats();
            projectiles = new List<Projectile>();
            projectilesToRemove = new List<Projectile>();
        }
      

        /// <summary>
        /// This method loads the mesh and attaches it to a node and to the schenegraph
        /// </summary>
        private void Load()
        {
            robotEntity = mSceneMgr.CreateEntity("robot.mesh");
            robotNode = mSceneMgr.CreateSceneNode();
            robotNode.AttachObject(robotEntity);
            
            controlNode = mSceneMgr.CreateSceneNode();
            mSceneMgr.RootSceneNode.AddChild(controlNode);
            controlNode.AddChild(robotNode);
            
            maxTime= Mogre.Math.RangeRandom(3000, 6000);
            float radius = 7;
            controlNode.Position += radius * Vector3.UNIT_Y;
            robotNode.Position -= radius * Vector3.UNIT_Y;
            controlNode.SetPosition((int)Mogre.Math.RangeRandom(-480, 400), 50, (int)Mogre.Math.RangeRandom(-480, 400));
            physObj = new PhysObj(radius, "Robot", 0.5f, 0.7f, 0.5f);
            physObj.SceneNode = controlNode;
            physObj.Position = controlNode.Position;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            physObj.AddForceToList(new FrictionForce(physObj));
            Physics.AddPhysObj(physObj);
            
           
        }

        /// <summary>
        /// This method detaches the robot node from the scene graph and destroies it and the robot enetity
        /// </summary>
        public override void Dispose()
        {
            foreach (CannonBall cBall in projectiles)
            {
                cBall.Dispose();
               
            }

            foreach (CannonBall ball in projectilesToRemove)
            {
                projectiles.Remove(ball);
                ball.Dispose();

            }
            robotNode.RemoveAllChildren();
            robotNode.Parent.RemoveChild(robotNode);
            robotNode.DetachAllObjects();
            robotNode.Dispose();
            robotEntity.Dispose();

            Physics.RemovePhysObj(physObj);
            physObj = null;
        }

        /// <summary>
        /// This methods set the position of the robot
        /// </summary>
        /// <param name="position"></param>
        public void setPosition(Vector3 position)
        {
            controlNode.Translate(position);
        }

        
        /// <summary>
        /// This method set up all the field needed for animation
        /// </summary>
        private void AnimationSetup()
        {
            radius = 0.1f;
            direction = Vector3.ZERO;
            angle = 0f;

            time = new Timer();
            PrintAnimationNames();
            animationChanged = false;
            animationName = "Walk";
            LoadAnimation();
        }

        /// <summary>
        /// This method this method makes the mesh move in circle
        /// </summary>
        /// <param name="evt">A frame event which can be used to tune the animation timings</param>
        private void CircularMotion(FrameEvent evt)
        {
         
           
          float xdir = player.Model.GameNode.Position.x - controlNode.Position.x;
           float zdir = player.Model.GameNode.Position.z - controlNode.Position.z;
           
           
           Vector3 playerDirection = new Vector3(xdir, 0, zdir);
           Vector3 playerLookDir = new Vector3(player.Model.GameNode.Position.x, controlNode.Position.y, player.Model.GameNode.Position.z);
           controlNode.LookAt(playerLookDir,Node.TransformSpace.TS_WORLD);
            playerDirection.Normalise();
            Degree d = 90;
            controlNode.Yaw(d);
           
         
            physObj.Velocity = new Vector3(playerDirection.x*3, physObj.Velocity.y,playerDirection.z*3);
             
             
              
        }

        /// <summary>
        /// This method sets the animationChanged field to true whenever the animation name changes
        /// </summary>
        /// <param name="newName"> The new animation name </param>
        private void HasAnimationChanged(string newName)
        {
            if (newName != animationName)
                animationChanged = true;
        }

        /// <summary>
        /// This method prints on the console the list of animation tags
        /// </summary>
        private void PrintAnimationNames()
        {
            AnimationStateSet animStateSet = robotEntity.AllAnimationStates;                    // Getd the set of animation states in the Entity
            AnimationStateIterator animIterator = animStateSet.GetAnimationStateIterator();     // Iterates through the animation states

            while (animIterator.MoveNext())                                                     // Gets the next animation state in the set
            {
                                                 // Print out the animation name in the current key
            }
        }

        /// <summary>
        /// This method deternimes whether the name inserted is in the list of valid animation names
        /// </summary>
        /// <param name="newName">An animation name</param>
        /// <returns></returns>
        private bool IsValidAnimationName(string newName)
        {
            bool nameFound = false;

            AnimationStateSet animStateSet = robotEntity.AllAnimationStates;
            AnimationStateIterator animIterator = animStateSet.GetAnimationStateIterator();

            while (animIterator.MoveNext() && !nameFound)
            {
                if (newName == animIterator.CurrentKey)
                {
                    nameFound = true;
                }
            }

            return nameFound;
        }

        /// <summary>
        /// This method changes the animation name randomly
        /// </summary>
        private void changeAnimationName()
        {
            switch ((int)Mogre.Math.RangeRandom(0, 4.5f))       // Gets a random number between 0 and 4.5f
            {
                case 0:
                    {
                        animationName = "Walk";
                        break;
                    }
                case 1:
                    {
                        animationName = "Shoot";
                        break;
                    }
                case 2:
                    {
                        animationName = "Idle";
                        break;
                    }
                case 3:
                    {
                        animationName = "Slump";
                        break;
                    }
                case 4:
                    {
                        animationName = "Die";
                        break;
                    }
            }
        }

        /// <summary>
        /// This method loads the animation from the animation name
        /// </summary>
        private void LoadAnimation()
        {
            animationState = robotEntity.GetAnimationState(animationName);
            animationState.Loop = true;
            animationState.Enabled = true;
        }

        /// <summary>
        /// This method puts the mesh in motion
        /// </summary>
        /// <param name="evt">A frame event which can be used to tune the animation timings</param>
        private void AnimateMesh(FrameEvent evt)
        {
            if (time.Milliseconds > maxTime)
            {
                changeAnimationName();
               // time.Reset();
            }

            if (animationChanged)
            {
                LoadAnimation();
                animationChanged = false;
            }

            animationState.AddTime(evt.timeSinceLastFrame);
        }

        /// <summary>
        /// This method animates the robot mesh
        /// </summary>
        /// <param name="evt">A frame event which can be used to tune the animation timings</param>
        public void Animate(FrameEvent evt)
        {
            CircularMotion(evt);

            AnimateMesh(evt);
        }

        /// <summary>
        /// This method adds a child to the robot node
        /// </summary>
        /// <param name="child">The scene node to be set as a child</param>
        public void AddChild(SceneNode child)
        {
            robotNode.AddChild(child);
        }

        /// <summary>
        /// This method moves the robot in the given direction
        /// </summary>
        /// <param name="direction">The direction along which move the robot</param>
        public  override void Move(Vector3 direction)
        {
          
            controlNode.Translate(direction);
        }

        /// <summary>
        /// This method rotate the robot accordingly  with the given angles
        /// </summary>
        /// <param name="angles">The angles by which rotate the robot along each main axis</param>
        public void Rotate(Vector3 angles)
        {
            controlNode.Yaw(angles.x);
           
        }

        /// <summary>
        /// updates robot health, collisions, projectiles if robot is hit by projectile its health is reduced appropriately
        /// </summary>    
        ///  <param name="evt">Mogre frame event</param>
        public override void Update(FrameEvent evt)
        {
            Animate(evt);
            base.Update(evt);
        
            if (IsCollidingWith("CannonBall")||IsCollidingWith("Bomb"))
            {
                stats.Health.Decrease((int)CannonBall.HealthDamage);
            }

            if (stats.Health.Value <= 0)
            {
                toRemove = true;
            }
           // Console.WriteLine(time.Milliseconds - maxTime);
            if (time.Milliseconds > maxTime)
            {
                Fire();
            }

            foreach (CannonBall cBall in projectiles)
            {

                cBall.Update(evt);
                if (cBall.toRemove)
                {
                    projectilesToRemove.Add(cBall);
                }
            }

            foreach (CannonBall ball in projectilesToRemove)
            {
                projectiles.Remove(ball);
                ball.Dispose();
              
            }
            projectilesToRemove.Clear();

        
        }

        /// <summary>
        /// checks for collisons with projectiles, sets to remove to true if collsion occurs
        /// </summary>    
        ///  <param name="objName">name of colliding object to check</param>
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
        /// Fire weapon, creates new cannonball
        /// </summary>    
        public void Fire()
        {
            CannonBall cannonBall = new CannonBall(mSceneMgr);
            Vector3 playerDirection = player.Model.GameNode.Position - controlNode.Position;            
            playerDirection.Normalise();
            cannonBall.SetPosition(controlNode.Position + 40 * playerDirection+(Vector3.UNIT_Y*20));
            cannonBall.physObj.Velocity = (playerDirection* 100);
            projectiles.Add(cannonBall);
            time.Reset();
        }
        
    }


}
