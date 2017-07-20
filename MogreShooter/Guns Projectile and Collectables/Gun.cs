using System;
using Mogre;

namespace RaceGame
{
    /// <summary>
    /// gun object is the parent of all weapons in the game
    /// provide functinality for ammo, reloading and gun position
    /// </summary>
    class Gun:MovableElement
    {
        protected int maxAmmo;
        public Timer Time;
        float ReloadTime = 5000;
        protected Projectile projectile;
        public bool reload;
        public Projectile Projectile
        {
            set { projectile = value; }
            
        }

        public Gun()
        {
            reload = false;
            Time = new Timer();
        }

        protected Stat ammo;
        public Stat Ammo
        {
            get { return ammo; }
        }

        /// <summary>
        /// get ostion of gun
        /// </summary>
        /// <returns>returns gun position</returns>
        public Vector3 GunPosition()
        {
            SceneNode node = gameNode;
            try
            {
                while (node.ParentSceneNode.ParentSceneNode != null)
                {
                    node = node.ParentSceneNode;
                }
            }
            catch (System.AccessViolationException)
            { }

            return node.Position;
        }

        /// <summary>
        /// get direction of gun
        /// </summary>
        /// <returns>returns direction gun is facing</returns>
        public Vector3 GunDirection()
        {
            SceneNode node = gameNode;
            try
            {
                while (node.ParentSceneNode.ParentSceneNode != null)
                {
                    node = node.ParentSceneNode;
                }
            }
            catch (System.AccessViolationException)
            { }

            Vector3 direction = node.LocalAxes * gameNode.LocalAxes.GetColumn(2);

            return direction;
        }
        /// <summary>
        /// update allows player to relaod gun
        /// </summary>
        /// <param name="evt">Mogre Frame Event</param>
        virtual public void Update(Mogre.FrameEvent evt) {

            if (reload)
            {
                if (Time.Milliseconds > ReloadTime)
                {
                    Console.WriteLine("Reloading to: " + ammo.Max);
                    ammo.Reset();
                    reload = false;
                }
            }
        }

        virtual protected void LoadModel() { }

        /// <summary>
        /// set ammo back to max ammo
        /// </summary>
        virtual public void ReloadAmmo() {

            if (ammo.Value <=0)
            {
                Time.Reset();
                reload = true;
                Console.WriteLine("Reload Timer");
            }
           
        
        }
        virtual public void Fire() { }        
    }
}
