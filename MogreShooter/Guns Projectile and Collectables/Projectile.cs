using System;
using System.Collections.Generic;
using Mogre;

namespace RaceGame
{
    /// <summary>
    /// parent of all projectiles in game
    /// provides functionality (overridable methods)for damage, speed, loading, disposal and updating the projectile
    /// </summary>
    abstract class Projectile:MovableElement
    {
        protected Timer time;
        protected int maxTime = 1000;
        protected Vector3 initialVelocity;
        protected float speed;
        protected Vector3 initialDirection;
        public Vector3 InitialDirection
        {
            set { initialDirection = value; }
        }
        protected static float healthDamage;
        public static float HealthDamage
        {
            get { return healthDamage; }
        }

        protected static float shieldDamage;
        public static float ShieldDamage
        {
            get { return shieldDamage; }
        }

        virtual protected void Load() { }

        protected Projectile()
        {
            time = new Timer();
        }

        public override void Dispose()
        {
            base.Dispose();
            this.remove = true;
        }

        virtual public void Update(FrameEvent evt) 
        {
            // Projectile collision detection goes here
            // (ignore until week 8) ...

            if (!remove && time.Milliseconds > maxTime)
            {
                //Dispose();
                //remove = true;
            }
        }
    }
}
