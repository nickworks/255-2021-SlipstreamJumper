using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geib
{
    public class ImpendingDoom : OverlapObject
    {

        public float damageAmount = 100;

        public Vector3 velocity;

        private void Start()
        {
            velocity.x = 0.01f;
        }
        private void Update()
        {
            
            while (velocity.x <= 0.1f)
            {
                velocity.x += 0.0000001f;
            }
            transform.position += velocity;
        }
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
