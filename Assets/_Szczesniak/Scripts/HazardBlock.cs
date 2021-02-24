using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Szczesniak {
    public class HazardBlock : OverlapObject {

        public float damageAmount = 25;
        public override void OnOverlap(PlayerMovement pm) {

            HealthSystem health = pm.GetComponent<HealthSystem>();

            if (health) {
                health.TakeDamage(damageAmount);
            }

            // add knock-back

            Vector3 vToPlayer = (pm.transform.position - this.transform.position).normalized;


            pm.LaunchPlayer(vToPlayer * 15);

            // player overlaps!

        }
    }
}