using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Szczesniak {
    /// <summary>
    /// This class is to keep track of the player's health
    /// </summary>
    public class HealthSystem : MonoBehaviour {
        
        // C# field
        //public float hp;

        // C# property
        // state:

        /// <summary>
        /// The variable to hold the player's health in private set
        /// </summary>
        public float health  { get; private set; }

        /// <summary>
        /// The player's max health
        /// </summary>
        public float healthMax = 100;

        /// <summary>
        /// float to make the player invincible for small amount of time to not be killed instantly from the FPS speed
        /// </summary>
        private float coolDownInvulnerability = 0;

        /// <summary>
        /// Particle system for when the player dies, smoke fills the like they blew up
        /// </summary>
        public ParticleSystem blowUp;

        /// <summary>
        /// Game over text when the player dies
        /// </summary>
        public GameObject gameOverText;

        /// <summary>
        /// Health text for the player to know how much health they have currently
        /// </summary>
        public Text healthNotifier;

        // behavior

        private void Start() {
            health = healthMax; // Assigning the health 
            healthNotifier.text = "Health: " + health; // Sets up the current health when the game starts
        }

        private void Update() {
            if (coolDownInvulnerability > 0) coolDownInvulnerability -= Time.deltaTime; // invulnerability for the player
        }

        /// <summary>
        /// When the player takes damage from a object
        /// </summary>
        /// <param name="amt"></param>
        public void TakeDamage(float amt) {

            if (coolDownInvulnerability > 0) return; // cooldown not finished...

            coolDownInvulnerability = 0.25f; // cooldown until we can take damage again

            if (amt < 0) amt = 0; // negative numbers are ignored
            health -= amt; // health = health - amt;

            healthNotifier.text = "Health: " + health; // updates health when player takes damage
            SoundEffectBoard.PlayerDamaged();

            if (health <= 0) Die(); // when the player has no more health
        }

        /// <summary>
        /// When the player has lost all health and dies
        /// </summary>
        public void Die(){
            SoundEffectBoard.PlayDeathSound();
            Instantiate(blowUp, transform.position, transform.rotation); // spawning the blewUp particle effect
            gameOverText.SetActive(true); // showing the Game Over text
            Destroy(gameObject); // destroy player
        }
    }
}