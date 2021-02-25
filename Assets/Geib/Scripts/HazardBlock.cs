using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geib
{
    public class HazardBlock : OverlapObject
    {

        public float damageAmount = 25;
        public override void OnOverlap(PlayerMovement pm)
        {

            HealthSystem health = pm.GetComponent<HealthSystem>();

            if (health)
            {
                health.TakeDamage(damageAmount);
            }

            //TODO add knock-back

            Vector3 vToPlayer = (pm.transform.position - this.transform.position).normalized;

            pm.LaunchPlayer(vToPlayer * 20);

        }
    }
}
