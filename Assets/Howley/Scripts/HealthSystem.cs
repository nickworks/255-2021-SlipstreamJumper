using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Howley
{
    public class HealthSystem : MonoBehaviour
    {
        // C# property:
        public float health { get; private set; }
        public float healthMax = 100;

        private float cooldownInvulnerable = 0;

        private void Start()
        {
            health = healthMax;
        }

        private void Update()
        {
            if (cooldownInvulnerable > 0) cooldownInvulnerable -= Time.deltaTime;
        }

        public void TakeDamage(float amt)
        {
            if (amt < 0) amt = 0;
            health -= amt;
            if (health <= 0) Die();
        }

        public void Die()
        {
            Destroy(gameObject);
        }
    }

}
