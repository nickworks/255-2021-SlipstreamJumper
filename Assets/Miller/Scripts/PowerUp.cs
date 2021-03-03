using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Miller
{
    public class PowerUp : MonoBehaviour
    {
        public float multiplier = 1.4f;

        public GameObject pickupEffect;

        AABB aabb;

        void Start()
        {
            aabb = GetComponent<AABB>();
            Zone.main.powerups.Add(aabb);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Pickup(other);
            }
        }

        void Pickup(Collider2D player)
        {
            Instantiate(pickupEffect, transform.position, transform.rotation);

            player.transform.localScale *= multiplier;

            Destroy(gameObject);
        }
    }
}
