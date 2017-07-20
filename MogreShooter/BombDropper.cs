using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
namespace RaceGame
{
    class BombDropper : Gun
    {
        public List<Projectile> projectiles;
        public List<Projectile> projectilesToRemove;
       
        ModelElement Model;
        /// <summary>
        /// Contructs and initilises the bombDroopper object
        /// </summary>
        /// <param name="mSceneMgr">Mogre sceneManager</param>
        public BombDropper(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            maxAmmo = 5;
            ammo = new Stat();
            ammo.InitValue(maxAmmo);
            LoadModel();

        }
        /// <summary>
        /// Loads model and niitialises projectiles list
        /// </summary>
        protected override void LoadModel()
        {
            base.LoadModel();
            Model = new ModelElement(mSceneMgr, "BombDropperGun.mesh");

            Model.GameEntity.SetMaterialName("blue");
            gameNode = Model.GameNode;
            projectiles = new List<Projectile>();
            projectilesToRemove = new List<Projectile>();
           
        }

        /// <summary>
        /// Fire projectiles
        /// </summary>
        public override void Fire()
        {
            if (ammo.Value > 0)
            {
                Bomb bomb = new Bomb(mSceneMgr);
               
                bomb.SetPosition(GunPosition() + 20 * (-GunDirection()));
                ammo.Decrease(1);
                projectiles.Add(bomb);
                //GunPosition() + 5 * GunDirection());
            }
        }

       /// <summary>
       /// reload ammo of weapon
       /// </summary>
        public override void ReloadAmmo()
        {
            if (ammo.Value < maxAmmo)
            {
                ammo.Increase(5);
            }
            base.ReloadAmmo();
        }

        /// <summary>
        /// update projectiles, check if need to be removed and remove
        /// </summary>
        /// <param name="evt">Mogre frame event</param>
        public override void Update(Mogre.FrameEvent evt)
        {
            base.Update(evt);
            foreach (Bomb cBall in projectiles)
            {

                cBall.Update(evt);
                if (cBall.toRemove)
                {
                    projectilesToRemove.Add(cBall);
                }
            }

            foreach (Bomb ball in projectilesToRemove)
            {
                projectiles.Remove(ball);
                ball.Dispose();

            }
            projectilesToRemove.Clear();

        }
    }
}
