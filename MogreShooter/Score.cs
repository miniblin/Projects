using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaceGame
{
    /// <summary>
    /// score stat class
    /// </summary>    
    class Score :Stat
    {
       public override void Increase(int val){
           value+=val;
       }
    }
}
