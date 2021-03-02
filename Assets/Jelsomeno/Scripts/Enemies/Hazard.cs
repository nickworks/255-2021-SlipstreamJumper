using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelsomeno
{
  
    public class Hazard : OverlapObject
    {
        // how much damage the hazard can do
        public float damageAmount = 25;

        public override void OnOverlap(PlayerMovement pm)
        {
            // hp 
            Health hp = pm.GetComponent<Health>();

            if (hp)
            {
                hp.TakeDamage(damageAmount);
            }

            Vector3 vToPlayer = (pm.transform.position - this.transform.position).normalized.normalized;

            pm.LaunchPlayer(vToPlayer * 7);// when the player its the hazard it will knock it back


        }

    }
}
