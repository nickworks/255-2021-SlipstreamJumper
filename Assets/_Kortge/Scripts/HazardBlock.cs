using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kortge
{
    [RequireComponent(typeof(AABB))]
    public class HazardBlock : OverlapObject
    {
        public override void OnOverlap(PlayerMovement pm)
        {
            HealthSystem health = pm.GetComponent<HealthSystem>();

            if (health)
            {
                health.TakeDamage(25);
            }

            Vector3 vToPlayer = (pm.transform.position - this.transform.position).normalized;
            vToPlayer = vToPlayer.normalized;

            pm.LaunchPlayer(vToPlayer * 15);
        }
    }
}