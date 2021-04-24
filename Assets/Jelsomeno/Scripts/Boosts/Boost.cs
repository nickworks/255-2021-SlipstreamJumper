using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jelsomeno
{
    /// <summary>
    /// This class is for only one boost, the jump increase boost pick up
    /// </summary>
    public class Boost : OverlapObject
    {
        /// <summary>
        /// this will check to see if the player has overlapped the object and if so pick it up and changed the player jump impulse
        /// </summary>
        /// <param name="pm"></param>
        public override void OnOverlap(PlayerMovement pm)
        {
            // if the  boost is picked up then apply boost to player movement 
            PlayerMovement jumpIncrease = pm.GetComponent<PlayerMovement>();
                if (jumpIncrease)
                {
                    /// once you pick up the item your character now has super jump, doubling your jump impulse 
                    jumpIncrease.jumpImpulse += 7;// increases jump impulse by 7
                    Destroy(gameObject);//destroys the boost item after it is picked up by player
                }
            
        }
    }
}




