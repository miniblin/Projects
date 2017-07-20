using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
namespace RaceGame
{
    /// <summary>
    /// Cannon gun class- cannon is a forwardd firing gun
    /// </summary>
    class Cannon:Gun
    {
        ModelElement Model;
        float coolDown;
        float coolDownTime;
        Timer time;
        public List<Projectile> projectiles;
        public List<Projectile> projectilesToRemove;

        /// <summary>
        /// Contructs cannon object and initialises max ammo, and cool down times
        /// </summary>
        /// <param name="mSceneMgr">Mogre scene manager</param>
        public Cannon(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            this.mSceneMgr = mSceneMgr;
            maxAmmo = 15;
            ammo = new Stat();
            ammo.InitValue(maxAmmo);
            LoadModel();
            coolDown = 0;
            coolDownTime = 100;
            time = new Timer();
        }

        /// <summary>
        /// loads the cannon model and attacthes to a physics objectt
        /// </summary>
        protected override void LoadModel()
        {
            base.LoadModel();
            Model = new ModelElement(mSceneMgr, "CannonGun.mesh");

            Model.GameEntity.SetMaterialName("red");
            gameNode = Model.GameNode;
            projectiles = new List<Projectile>();
            projectilesToRemove = new List<Projectile>();
            gameNode.Scale(1.2f, 1.2f, 1.2f);
            
        }

        /// <summary>
        /// fires canon, creates a cannonball object and sets velocity in correct direction
        /// </summary>
        public override void Fire()
        {
            if (ammo.Value > 0&&(coolDown<time.Milliseconds))
            {
                coolDown =time.Milliseconds+ coolDownTime;
        

                CannonBall cannonBall = new CannonBall(mSceneMgr);
                Console.Out.WriteLine("Gun Direction:" + GunDirection());
                cannonBall.SetPosition(GunPosition() + 40 * GunDirection());
                cannonBall.physObj.Velocity = (GunDirection() * 100);
                projectiles.Add(cannonBall);
                ammo.Decrease(1);
                
                //GunPosition() + 5 * GunDirection());
            }
        }

        /// <summary>
        /// Chceck and removes necessary projectiles
        /// </summary>
        /// <param name="evt"></param>
        public override void Update(Mogre.FrameEvent evt)
        {
            base.Update(evt);
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
        /// reset ammo to max ammo.
        /// </summary>
        public override void ReloadAmmo()
        {
            if (ammo.Value < maxAmmo)
            {
               // ammo.Increase(5);
            }
            base.ReloadAmmo();
        }
    }
}
