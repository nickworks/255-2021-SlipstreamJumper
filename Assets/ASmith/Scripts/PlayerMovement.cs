using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASmith
{
    public class PlayerMovement : MonoBehaviour
    {
        public float acceleration = 20;
        public float deceleration = 40;
        private Vector3 velocity = new Vector3();

        void Start()
        {
        
        }

        void Update()
        {
            float h = Input.GetAxisRaw("Horizontal"); // Use raw to avoid the built in acceleration and deceleration

            // Eueler Physics Integration
            if (h != 0) // User is pressing left/right
            {
                // accelerate
                velocity.x += h * Time.deltaTime * acceleration;
            } else // User not pressing left/right
            {
              if (velocity.x > 0) // Player moving right
              {
                    velocity.x -= deceleration * Time.deltaTime;
              }
              if (velocity.x < 0) // Player moving left
                {
                    velocity.x += deceleration * Time.deltaTime;
                    if (velocity.x > 0) velocity.x = 0;
                }
            }

            velocity.x += h * Time.deltaTime;

            // Applies velocity to position
            transform.position += velocity * Time.deltaTime;
        }
    }
}

