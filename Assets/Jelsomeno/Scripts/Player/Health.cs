using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jelsomeno
{
    public class Health : MonoBehaviour
    {
        /// <summary>
        /// this is a health class for the player so they can take damage and die
        /// </summary>

        // state:
        public float currentHealth = 100;
        public float healthMax = 100;

        /// <summary>
        /// this is so we can place the empty game object and be the location for the respawn
        /// </summary>
        public Transform spawnPoint;
       //public HealthBarBehavior HealthBar;

        private float cooldownInvulnerability = 0;

        private void Start()
        {
            currentHealth = healthMax;
            //HealthBarBehavior.SetHealth(health, healthMax);
        }


        private void Update()
        {
            if(cooldownInvulnerability > 0) cooldownInvulnerability -= Time.deltaTime;
            
        }
        public void TakeDamage(float amt)
        {
            if (cooldownInvulnerability > 0) return; // cooldown not finished

            cooldownInvulnerability = 0.25f; // cooldown until we take damage again


            if (amt < 0) amt = 0;// negative numbers are ignored
            currentHealth -= amt;
            if (currentHealth <= 0) Die();// die
        }

        public void Die()
        {
            Respawn();
            
            currentHealth = healthMax; // resets the players health once they respawn

        }

        /// <summary>
        /// after player loses their health, respawn them at the respawn point
        /// </summary>
        public void Respawn()
        {
            this.transform.position = spawnPoint.position;
        }
    }
}
