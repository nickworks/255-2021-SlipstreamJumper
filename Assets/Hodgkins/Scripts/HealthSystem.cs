using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hodgkins
{
    public class HealthSystem : MonoBehaviour
    {
        // state:
        public float health { get; private set; }
        public float healthMax = 100;

        private float cooldownInvulnerability = 0;

        //behavior
        private void Start() {
            health = healthMax;
        }

        private void Update() {
            if (cooldownInvulnerability > 0) cooldownInvulnerability -= Time.deltaTime;
        }

        /// <summary>
        /// How the player takes damage
        /// </summary>
        /// <param name="amt"></param>
        public void TakeDamage(float amt)
        {

            if (cooldownInvulnerability > 0) return; // cooldown not finished

            cooldownInvulnerability = 0.25f; // cooldown until we can take damage again

            if (amt < 0) amt = 0; // negative numbers are ignored
            health -= amt; // health = health - amt
            if (health <= 0) Die();

        }
        /// <summary>
        /// When the object with health (the player) is out of health
        /// </summary>
        public void Die()
        {
            Destroy(gameObject);
            SoundEffectBoard.PlayDie();
            SlipstreamJumper.Game.GameOver();
        }

    }
}