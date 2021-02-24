using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelsomeno
{
  
    public class Hazard : OverlapObject
    {
        public float damageAmount = 25;

        public override void OnOverlap(PlayerMovement pm)
        {
            Health hp = pm.GetComponent<Health>();

            if (hp)
            {
                hp.TakeDamage(damageAmount);
            }

            Vector3 vToPlayer = (pm.transform.position - this.transform.position).normalized.normalized;




            pm.LaunchPlayer(vToPlayer * 7);


        }

    }
}
