using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Velting
{
    public class HealthSystem : MonoBehaviour
    {
        //state:
        //C# field:
        //public float hp;

        // C# property:
        public float health { get; private set; }
        public float healthMax = 100;


        private float cooldownInvulnerability = 0;

        //behavior

        private void Start()
        {
            health = healthMax;
        }
        private void Update()
        {
            if (cooldownInvulnerability > 0) cooldownInvulnerability -= Time.deltaTime; //reduces cooldown to zero
        }
        public void TakeDamage(float amt)
        {

            if (cooldownInvulnerability > 0) return; //cooldown not finished, exit function

            cooldownInvulnerability = .25f; //cooldown until we can take damage again


            if (amt < 0) amt = 0; //negative numbers are ignored
            health -= amt; // health = health - amt;
            if (health <= 0) Die();//die
        }

        public void Die()
        {
            Destroy(gameObject); //Destroys game object when health reaches zero
        }


    }
}