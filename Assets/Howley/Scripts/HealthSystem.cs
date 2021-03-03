using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Howley
{
    public class HealthSystem : MonoBehaviour
    {
        // C# property:
        public float health { get; private set; }

        /// <summary>
        /// The maximum amount of health the object with a health system has.
        /// </summary>
        public float healthMax = 100;

        /// <summary>
        /// This variable is a cooldown for the player being able to take damage.
        /// </summary>
        private float cooldownInvulnerable = 0;

        private void Start()
        {
            health = healthMax;
        }

        private void Update()
        {
            if (cooldownInvulnerable > 0) cooldownInvulnerable -= Time.deltaTime;
        }

        /// <summary>
        /// This function gives a damage amount, and checks for 0 health.
        /// </summary>
        /// <param name="amt"></param>
        public void TakeDamage(float amt)
        {
            if (amt < 0) amt = 0;
            health -= amt;
            if (health <= 0) Die();
        }

        /// <summary>
        /// This function destroys a game object if the health is 0.
        /// </summary>
        public void Die()
        {
            Destroy(gameObject);
        }
    }

}
