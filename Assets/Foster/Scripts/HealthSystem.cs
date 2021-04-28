using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Foster { 
    public class HealthSystem : MonoBehaviour
    {

        //c# property;
        public static float health { get; private set; }
        public float healthMax = 100;

        private float coolDownInv = 0;
        private float coolDownHealth = 0;

        private void Start()
        {
            health = healthMax;
        }

        private void Update()
        {
            if(coolDownInv >0) coolDownInv -= Time.deltaTime;
            
        }
        public void Heal(float heal)
        {
            if (coolDownHealth > 0) return;
            coolDownHealth = .25f;
            if (heal < 0) heal = 0;
            health += heal;
            if (health >= 100) health = 100;

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
            SlipstreamJumper.Game.GameOver();

        }
    }
}