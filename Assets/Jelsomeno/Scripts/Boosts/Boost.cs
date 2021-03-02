using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jelsomeno
{
    public class Boost : OverlapObject
    {
        public override void OnOverlap(PlayerMovement pm)
        {
            PlayerMovement jumpIncrease = pm.GetComponent<PlayerMovement>();
                if (jumpIncrease)
                {
                    /// once you pick up the item your character now has super jump, doubling your jump impulse 
                    jumpIncrease.jumpImpulse += 7;
                    Destroy(gameObject);
                }
            
        }
    }
}




