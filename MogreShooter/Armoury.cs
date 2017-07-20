using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
namespace RaceGame
{
    /// <summary>
    /// the armoury class tracks weapons of the player, allows the player to switch between weapons
    /// ans pick up new ones
    /// </summary>
    class Armoury
    {
        private bool gunChanged;
        public bool GunChanged{
            get{return gunChanged;}
            set{gunChanged=value;}
        }

        private Gun activeGun;
       

        public Gun ActiveGun
        {
            get { return activeGun; }
          
        }

        List<Gun> collectedGuns;

        public Armoury(){
            collectedGuns = new List<Gun>();
            
        }
        /// <summary>
        /// dispose of guns
        /// </summary>
        public void Dispose()
        {
            if (activeGun != null)
            {
                activeGun.Dispose();
            }

            foreach (Gun gun in collectedGuns)
            {
                gun.Dispose();
            }
        }

        public void ChangeGun(Gun gun)
        {
            
            activeGun = gun;
            gunChanged = true;
        }
        /// <summary>
        /// allows player to swap to a specific gun via and index
        /// </summary>
        /// <param name="index">index of new weapon</param>
        public void SwapGun(int index)
        {
           
            if (collectedGuns != null && activeGun != null)
            {
               
                ChangeGun(collectedGuns[index%collectedGuns.Count]);
                Console.WriteLine(index+"changing Weapon:" + index % collectedGuns.Count+"out of :"+collectedGuns.Count);
            }
        }

        /// <summary>
        /// allows player add a new gun to their armoury
        /// checks if they already have the gun before dding it. if the player already has it, the weapon is reloaded
        /// </summary>
        /// <param name="gun">the new gun object</param>
        public void AddGun(Gun gun)
        {
            bool add = true;
            
            foreach (Gun g in collectedGuns)
            {
                if (add && g.GetType() == gun.GetType())
                {
                    g.ReloadAmmo();
                    ChangeGun(g);
                    add = false;
                }
            }
            if (add)
            {
                ChangeGun(gun);
                collectedGuns.Add(gun);
            }
            else{
                gun.Dispose();
            }
           
        }
        /// <summary>
        /// updates each weapon
        /// </summary>
        /// <param name="evt">The Mogre Frame Event</param>
        public void Update(Mogre.FrameEvent evt)
        {
            foreach (Gun gun in collectedGuns)
            {
                gun.Update(evt);
            }
        }

    }
}
