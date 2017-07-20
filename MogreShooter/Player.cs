using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;
namespace RaceGame
{
    /// <summary>
    /// player class contains armoury, model and updates the player when nessecary. 
    /// check for collisions with other objects.
    ///  moves the player object
    /// </summary>
    class Player : Character
    {
        SceneManager mSceneMgr;

       
        private Armoury playerArmoury;
        public bool PlayerDead;
        public Armoury PlayerArmoury
        {
            get { return playerArmoury; }
        }

        private PlayerStats stats;
        public PlayerStats Stats
        {
            get { return stats; }
        }


        /// <summary>
        /// constructor initalise the layer object
        /// </summary>
        /// <param name="mSceneMgr"></param>
        public Player(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            InitialisePlayer();
            playerArmoury = new Armoury();
            PlayerDead = false;
            // playerArmoury.ChangeGun(new Cannon(mSceneMgr));
        }


        /// <summary>
        /// check active gun and shoot
        /// </summary>
        public override void Shoot()
        {
            if (playerArmoury.ActiveGun != null)
            {

                playerArmoury.ActiveGun.Fire();
            }
        }

        /// <summary>
        /// initalise player model and physics
        /// </summary>
        private void InitialisePlayer()
        {
            model = new PlayerModel(mSceneMgr);
            controller = new PlayerController(this);
            stats = new PlayerStats();
            stats.Lives.Decrease(2);

        }

        /// <summary>
        /// move player
        /// </summary>
        /// <param name="direction">direction in which to move</param>
        public override void Move(Vector3 direction)
        {
            //Console.Out.WriteLine("Player position" + model.GameNode.Position);
            ((PlayerModel)model).physObj.Velocity = (direction / 5f);

        }
        int gun = 1;


        /// <summary>
        /// update payer  movement amoury and all other player related objects
        /// </summary>
        /// <param name="evt">Mogre frame event</param>
        public override void Update(Mogre.FrameEvent evt)
        {
           
            if (playerArmoury.GunChanged)
            {
                ((PlayerModel)model).AttachGun(playerArmoury.ActiveGun);

                playerArmoury.GunChanged = false;
            }


            if (((PlayerController)controller).changeGun)
            {

                playerArmoury.SwapGun(gun++);
                ((PlayerController)controller).changeGun = false;
            }
            model.Animate(evt);
            controller.Update(evt);
            playerArmoury.Update(evt);
            base.Update(evt);


            if (IsCollidingWith("CannonBall") || IsCollidingWith("Bomb"))
            {
                if (stats.Shield.Value > 0)
                {
                    stats.Shield.Decrease((int)CannonBall.ShieldDamage);
                }
                else
                {
                    stats.Health.Decrease((int)CannonBall.HealthDamage);
                }

            }
            if (stats.Health.Value <= 0)
            {
                stats.Lives.Decrease(1);
                stats.Health.Reset();

                if (stats.Lives.Value <= 0)
                {
                    PlayerDead = true;
                }

            }
        }


        /// <summary>
        /// check collsions with other object passed as parameter
        /// </summary>
        /// <param name="objName">object to chedk collsion against</param>
        /// <returns>return true of collsion occurs</returns>
        protected bool IsCollidingWith(string objName)
        {
            bool isColliding = false;
            foreach (Contacts c in ((PlayerModel)model).physObj.CollisionList)
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



    }


}
