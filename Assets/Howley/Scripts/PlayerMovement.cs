using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Wrap in a namespace to show the class is unique to my project.
namespace Howley 
{
    public class PlayerMovement : MonoBehaviour
    {
        /*
        Creating our own physics code.
        Objects have position, rotation, scale (transform)
        Need to access player input, and use velocity to propel an object based on that input.
        */

        // Values for speed up and slow down
        public float acceleration = 5;
        public float deceleration = 40;
        // We need to store and use velocity.
        private Vector3 velocity = new Vector3();

        void Update()
        {
            // Get the horizontal axis and store this in a variable. 
            float h = Input.GetAxisRaw("Horizontal");

            // === Euler physics integration. ===
            if (h != 0) // User is pressing A or D.
            {
                // Add the player's input to the velocity x deltaTime.
                velocity.x += h * Time.deltaTime * acceleration;
            }
            else // User is not pressing A or D
            {
                if (velocity.x > 0) // Player is moving right
                {
                    velocity.x -= deceleration * Time.deltaTime; // Accelerate left
                    if (velocity.x < 0) velocity.x = 0; 
                }
                if (velocity.x < 0) // Player is moving left
                {
                    velocity.x += deceleration * Time.deltaTime; // Accelerate right
                    if (velocity.x > 0) velocity.x = 0;
                }
            }
            // Apply velocity to player position.
            transform.position += velocity * Time.deltaTime; // Adding velocity to position
        }
    }
}


