using System;

namespace RaceGame
{
    /// <summary>
    /// This method represents a statistic for a character in the game.
    /// </summary>
    class Stat
    {
        protected int max; // This field contatins the maximum value for this stat
        /// <summary>
        /// Read only. This property gives back the maximum value of this stat
        /// </summary>
        public int Max
        {
            get { return max; }
        }

        protected int value; // This field contains the current value of this stat
        /// <summary>
        /// Read only. This property gives back the current value of this stat
        /// </summary>
        public int Value
        {
            get { return value; }
        }
        
        /// <summary>
        /// This method initializes the current and max amount of this stat to a value set by the user
        /// </summary>
        /// <param name="val">An initial value set by the user</param>
        public void InitValue(int val) 
        { 
            max = value = val; 
        }
        
        /// <summary>
        /// This virtual method increases the current value of the stat by a value set by the user, 
        /// if the final increased current value is bigger than the maximum value for this stat the 
        /// current value is set to the maximum
        /// </summary>
        /// <param name="val">A value by wich increase the current value of this stat</param>
        virtual public void Increase(int val)
        {
            if((value+val)<max){
                value+=val;
            }
            else{
                value=max;
            }
            
            // YOUR CODE FOR INCREASING THE value FIELD BY val UNTIL IT REACHES MAX GOES HERE.
            // value MUST ALWAYS BE LESSER THAN OR EQUAL TO MAX
        }

        /// <summary>
        /// This method decreases the current value of the stat by a value set by the user, 
        /// if the final decreased current value is smaller than zero the current value is set to zero
        /// </summary>
        /// <param name="val">A value by wich decrease the current value of this stat</param>
        public void Decrease(int val)
        {
            if ((value - val) > 0)
            {
                value -= val;
            }
            else
            {
                value = 0;
            }
            // YOUR CODE FOR DECREASE value BY val UNTILL IT REACHES 0 GOES HERE.
            // value MUST ALWAYS BE BIGGER THAN OR EQUAL TO 0.
        }

        /// <summary>
        /// This method sets the current value of this stat to the maximum value of this stat
        /// </summary>
        public void Reset()
        {
            value = max;
        }
    }
}
