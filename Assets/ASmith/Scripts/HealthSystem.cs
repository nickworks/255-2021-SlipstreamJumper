using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASmith
{
    public class HealthSystem : MonoBehaviour
    {
        // state:
        public float health { get; private set; }
        public float healthMax = 100;

        private float cooldownInvulnerability = 0;

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

        // behavior:
        public void TakeDamage(float amt)
        {
            if (cooldownInvulnerability > 0) return; // still have i-frames, dont take damage
            cooldownInvulnerability = .25f; // cooldown till you can take damage
            if (amt < 0) amt = 0; // Negative numbers ignored
            health -= amt; 
            if (health <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            Destroy(gameObject);
        }
    }
}

