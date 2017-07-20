using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

namespace RaceGame
{
    /// <summary>
    /// player controller class deals with input and calls appropriate character methods
    /// </summary>
    class PlayerController:CharacterController
    {

        public bool reload;
        /// <summary>
        /// constructor sets the speed of character
        /// </summary>
        /// <param name="player">player object to be controlled</param>
        public PlayerController(Character player)
        {
            character = player;
            speed = 300;
        }

        public bool changeGun ;//{ get { return changeGun; } set { changeGun = value; } }

        /// <summary>
        /// update player controls
        /// </summary>
        /// <param name="evt">mogre frame event</param>
        public override void Update(Mogre.FrameEvent evt)
        {
            MovementsControl(evt);
            MouseControls();
            ShootingControls();

            base.Update(evt);
        }

        /// <summary>
        /// deal with left right back and forward key entry, call neccessary method in character
        /// </summary>
        /// <param name="evt">Mogre frame event</param>
        private void MovementsControl(FrameEvent evt){
            Vector3 move = Vector3.ZERO;
            if (forward)
            {
                 move += character.Model.Forward;
              
            }

            if (backward)
            {
                move -= character.Model.Forward;
             
            }

            if (left)
            {
                move += character.Model.Left;
            
            }

            if (right)
            {
                move -= character.Model.Left;
              //  Console.Out.WriteLine(move);
            }
            move = move.NormalisedCopy * speed;

            if (move != Vector3.ZERO)
            {
                character.Move(move);// * evt.timeSinceLastFrame
            }


        }
        /// <summary>
        /// rotate based on mouse input
        /// </summary>
        public void MouseControls(){
            character.Model.GameNode.Yaw(Mogre.Math.AngleUnitsToRadians(angles.y));
        }
        /// <summary>
        /// deal with shoot input, call player shoot method
        /// </summary>
        public void ShootingControls(){
            if (shoot)
            {
                character.Shoot();
                shoot = false;
                
            }
            if (reload)
            {
                if (((Player)character).PlayerArmoury.ActiveGun != null) { 
                ((Player)character).PlayerArmoury.ActiveGun.ReloadAmmo();
                reload = false;
            }
                else
                {
                    reload = false;
                }
               
            }
           
        }



    }

    
}
