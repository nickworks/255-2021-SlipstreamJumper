using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hodgkins
{
    public class HazardBlock : OverlapObject
    {
        public float damageAmount = 20; // the amount of damage the hazard deals to the player
        
        /// <summary>
        /// When the player touches the hazard
        /// </summary>
        /// <param name="pm"></param>
        public override void OnOverlap(PlayerMovement pm)
        {
            HealthSystem health = pm.GetComponent<HealthSystem>(); // makes sure the object the hazard 
                                                                  //  touches has a health system
            
            if(health)
            {
                health.TakeDamage(damageAmount);
            }

            Vector3 vToPlayer = (pm.transform.position - this.transform.position).normalized;
            
            pm.LaunchPlayer(vToPlayer * 15); // launches player away when hit

            SoundEffectBoard.PlayHit();
        }
    }
}