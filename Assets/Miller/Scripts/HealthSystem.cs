using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{


    // C# property
    // state:
    public float health { get; private set; }
    public float healthMax = 100;


    private float cooldowninvulnerability = 0;

    // behavior

    private void Start()
    {
        health = healthMax;
    }

    private void Update()
    {
        if (cooldowninvulnerability > 0)
            cooldowninvulnerability -= Time.deltaTime;
    }

    public void TakeDamage(float amt)
    {

        if (cooldowninvulnerability > 0) return; // cooldown not finished...

        cooldowninvulnerability = 0.25f; // cooldwon until we can take damage again

        if (amt < 0) amt = 0; // negative numbers are ignored
        health -= amt; // health =  health - amt;
        if (health <= 0) Die(); // dead
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }



}
