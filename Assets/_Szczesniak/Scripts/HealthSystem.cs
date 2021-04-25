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

        public float repawnTime = 1;
        private float repawnTimeSet;

        public int lives = 3;

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

        public Text livesNotifier;

        private MeshRenderer[] playerRespawnEffect;
        private PlayerMovement stopMovementWhenRespawning;
        private ParticleSystem exhaustPipe;
        private bool readyToRespawn = false;



        // behavior

        private void Start() {
            health = healthMax; // Assigning the health 
            healthNotifier.text = "Health: " + health; // Sets up the current health when the game starts
            exhaustPipe = GetComponentInChildren<ParticleSystem>();
            playerRespawnEffect = GetComponentsInChildren<MeshRenderer>();
            stopMovementWhenRespawning = GetComponent<PlayerMovement>();
            repawnTimeSet = repawnTime;
        }

        private void Update() {
            if (coolDownInvulnerability > 0) coolDownInvulnerability -= Time.deltaTime; // invulnerability for the player

            if (repawnTime > 0) repawnTime -= Time.deltaTime;
            if (repawnTime <= 0 && readyToRespawn) {

                transform.position = transform.position + new Vector3(6f, 0, 0);

                foreach (MeshRenderer meshPiece in playerRespawnEffect) {
                    meshPiece.enabled = true;
                }
                exhaustPipe.Play();
                stopMovementWhenRespawning.enabled = true;

                coolDownInvulnerability = 1;
                readyToRespawn = false;
            }
        }

        /// <summary>
        /// When the player takes damage from a object
        /// </summary>
        /// <param name="amt"></param>
        public void TakeDamage(float amt) {
            if (!readyToRespawn) {
                if (coolDownInvulnerability > 0) return; // cooldown not finished...

                coolDownInvulnerability = 0.25f; // cooldown until we can take damage again
                repawnTime = repawnTimeSet;

                if (amt < 0) amt = 0; // negative numbers are ignored
                health -= amt; // health = health - amt;

                SoundEffectBoard.PlayerDamaged();

                if (health <= 0) {
                    lives--;
                    readyToRespawn = true;

                    foreach (MeshRenderer meshPiece in playerRespawnEffect) {
                        meshPiece.enabled = false;
                    }
                    exhaustPipe.Stop();
                    stopMovementWhenRespawning.enabled = false;

                    if (lives > 0) health = healthMax;
                }
                healthNotifier.text = "Health: " + health; // updates health when player takes damage
                livesNotifier.text = "Lives: " + lives; // Tells player the lives they have left.

                if (lives <= 0) {
                    Die(); // when the player has no more health
                }
            }
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