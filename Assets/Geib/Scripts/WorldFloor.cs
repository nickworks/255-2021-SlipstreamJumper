using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geib
{
    public class WorldFloor : OverlapObject
    {

        public float damageAmount = 100;
        public override void OnOverlap(PlayerMovement pm)
        {

            HealthSystem health = pm.GetComponent<HealthSystem>();

            if (health)
            {
                health.TakeDamage(damageAmount);
            }

            //TODO add knock-back

            Vector3 vToPlayer = (pm.transform.position - this.transform.position).normalized;
            SoundEffectBoard.PlayDeath();
            Debug.Log("You fell.");


        }
    }
}
