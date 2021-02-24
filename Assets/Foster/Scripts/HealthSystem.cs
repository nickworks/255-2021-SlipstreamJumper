using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Foster { 
    public class HealthSystem : MonoBehaviour
    {

        //c# property;
        public float health { get; private set; }
        public float healthMax = 100;

        private float coolDownInv = 0;

        private void Start()
        {
            health = healthMax;
        }

        private void Update()
        {
            if(coolDownInv >0) coolDownInv -= Time.deltaTime;
            
        }

        public void TakeDamage(float amt)
        {
            if (coolDownInv > 0) return;

            coolDownInv = .25f;

            if (amt < 0) amt = 0;
            health -= amt;

            if (health <= 0) Die();
        }
        public void Die()
        {
            Destroy(this.gameObject);
        }
    }
}