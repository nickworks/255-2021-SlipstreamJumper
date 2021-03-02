using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Szczesniak {
    public class HealthSystem : MonoBehaviour {
        
        // C# field
        //public float hp;

        // C# property
        // state:
        public float health  { get; private set; }
        public float healthMax = 100;

        private float coolDownInvulnerability = 0;

        public ParticleSystem blowUp;
        public GameObject gameOverText;

        // behavior

        private void Start() {
            health = healthMax;
        }

        private void Update() {
            if (coolDownInvulnerability > 0) coolDownInvulnerability -= Time.deltaTime;
        }

        public void TakeDamage(float amt) {

            if (coolDownInvulnerability > 0) return; // cooldown not finished...

            coolDownInvulnerability = 0.25f; // cooldown until we can take damage again

            if (amt < 0) amt = 0; // negative numbers are ignored
            health -= amt; // health = health - amt;

            if (health <= 0) Die();
        }

        public void Die(){
            Instantiate(blowUp, transform.position, transform.rotation);
            gameOverText.SetActive(true);
            Destroy(gameObject);
        }
    }
}