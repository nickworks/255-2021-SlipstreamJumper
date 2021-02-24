using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Howley
{
    public class Hazard : OverlapObject
    {
        public float damageAmount = 25;

        public override void OnOverlap(PlayerMovement pm)
        {
            HealthSystem health = pm.GetComponent<HealthSystem>();

            if (health)
            {
                health.TakeDamage(damageAmount);
            }

            Vector3 vToPlayer = (pm.transform.position - this.transform.position).normalized;
            
            pm.LauchPlayer(vToPlayer * 15);
        }
    }
}

