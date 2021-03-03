using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Foster
{
    public class NoDamage : OverlapObjects
    {
        private float healing = 50;

        public override void OnOverlap(PlayerMovement pm)
        {
            HealthSystem health = pm.GetComponent<HealthSystem>();
            if (health)
            {
                health.Heal(healing);
            }
            Destroy(gameObject);
        }
    }
}