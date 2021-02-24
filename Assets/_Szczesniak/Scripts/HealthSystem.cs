using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Szczesniak {
    public class HealthSystem : MonoBehaviour {
        
        // C# field
        //public float hp;

        // C# property
        // state:
        public float health  { get; private set; }
        public float healthMax = 100;

        private float coolDownInvulnerability = 0;

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
            Destroy(gameObject);
        }
    }
}