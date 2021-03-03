using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Geib
{
    public class HealthSystem : MonoBehaviour
    {
        
        //state:
        /// <summary>
        /// Health is the player's health, or the amount of damage 
        /// the player can withstand before dying.
        /// </summary>
        public float health { get; private set; }
        /// <summary>
        /// This is the maximum health the player can have.
        /// </summary>
        public float healthMax = 100;

        /// <summary>
        /// This is how log the player is immune from taking damage after taking damage.
        /// </summary>
        private float cooldownInvulnerability = 3;

        /// <summary>
        /// This is the scene for ZoneGeib
        /// </summary>
        private Scene scene;

        //behavior

        private void Start()
        {
            health = healthMax;
            scene = SceneManager.GetActiveScene();
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
            SoundEffectBoard.PlayDeath();
            new WaitForSeconds(100);
            Application.LoadLevel(scene.name);
            
           
            //Destroy(gameObject);
        }

    }
}
