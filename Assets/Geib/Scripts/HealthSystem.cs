using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geib
{
    public class HealthSystem : MonoBehaviour
    {
        
        //state:
        public float health { get; private set; }
        public float healthMax = 100;

        private float cooldownInvulnerability = 0;

        //behavior

        private void Start()
        {
            health = healthMax;
        }

        private void Update()
        {
            if (cooldownInvulnerability > 0)
            {
                cooldownInvulnerability -= Time.deltaTime;
            }
        }

        public void TakeDamage(float amt)
        {

            if (cooldownInvulnerability > 0) return; // Not done cooling down

            cooldownInvulnerability = 0.25f; // cooldown untl we can take damage again

            if (amt < 0) amt = 0; // negative numbers are ignored
            health -= amt; // health = health - amount
            if (health <= 0) Die(); // die...
        }

        public void Die()
        {
            Destroy(gameObject);
        }

    }
}
