using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jelsomeno
{
    public class Health : MonoBehaviour
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
            if(cooldownInvulnerability > 0) cooldownInvulnerability -= Time.deltaTime;
            
        }
        public void TakeDamage(float amt)
        {
            if (cooldownInvulnerability > 0) return; // cooldown not finished

            cooldownInvulnerability = 0.25f; // cooldown until we take damage again


            if (amt < 0) amt = 0;// negative numbers are ignored
            health -= amt;
            if (health <= 0) Die();// die
        }

        public void Die()
        {
            Destroy(gameObject);
        }
    }
}
