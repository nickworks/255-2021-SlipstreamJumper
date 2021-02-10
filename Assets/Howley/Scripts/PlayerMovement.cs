using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Wrap in a namespace to show the class is unique to my project.
namespace Howley 
{
    /// <summary>
    /// This class gets input and moves the player
    /// with the input, and Euler physics integration.
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        /*
        Creating our own physics code.
        Objects have position, rotation, scale (transform)
        Need to access player input, and use velocity to propel an object based on that input.
        */
        /// <summary>
        /// When the player wants to move, this value
        /// is used to scale the player's horizontal acceleration.
        /// </summary>
        [Header("Horizontal Movement")]
        // Values for speed up and slow down
        public float acceleration = 5;

        /// <summary>
        /// This value is used to scale the player's horizontal deceleration.
        /// </summary>
        public float deceleration = 40;

        /// <summary>
        /// This value is used to limit the player's maximum horizontal speed.
        /// Measured in meters/second
        /// </summary>
        public float maxSpeed = 5;

        /// <summary>
        /// This value is used to scale the player's
        /// downward acceleration due to gravity.
        /// </summary>
        [Header("Vertical Movement")]
        public float gravity = 10;

        /// <summary>
        /// The velocity we launch the player when they jump. 
        /// Measured in meters/second.
        /// </summary>
        public float jumpImpulse = 10;

        // We need to store and use velocity.
        /// <summary>
        /// The current velocity of the player
        /// Measured in meters/second
        /// </summary>
        private Vector3 velocity = new Vector3();

        /// <summary>
        /// Whether or not the player is currently holding the spacebar down.
        /// </summary>
        private bool isJumpingUpwards = false;

        private bool isGrounded = false;

        private AABB aabb;

        void Start()
        {
            aabb = GetComponent<AABB>();
        }

        /// <summary>
        /// This function updates the player's horizontal and vertical movement,
        /// and applies Euler physics each tick.
        /// </summary>
        void Update()
        {
            HorizontalMovement();

            VerticalMovement();

            // Apply velocity to player position.
            transform.position += velocity * Time.deltaTime; // Adding velocity to position
            aabb.RecalcAABB();

            isGrounded = false;
        }

        /// <summary>
        /// Calculating Euler physics on the Y axis.
        /// </summary>
        private void VerticalMovement()
        {
            float gravMultiplier = 1;

            // Get players input for spacebar
            bool wantsToJump = Input.GetButtonDown("Jump");

            // True for every frame the button is held down
            bool isHoldingJump = Input.GetButton("Jump");

            if (wantsToJump && isGrounded)
            {
                velocity.y = jumpImpulse;
                isJumpingUpwards = true;
            }
            if (!isHoldingJump || velocity.y < 0) // If you've reached peak jump, or not holding space
            {
                isJumpingUpwards = false;
            }

            if (isJumpingUpwards) gravMultiplier = 0.5f;

            // Subtract from the y by gravity per second "GRAVITY"
            velocity.y -= gravity * Time.deltaTime * gravMultiplier;
           
        }

        /// <summary>
        /// Calculating Euler physics on the X axis.
        /// </summary>
        private void HorizontalMovement()
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

            // Clamp velocity
            //if (velocity.x < -maxSpeed) velocity.x = -maxSpeed;
            //if (velocity.x > maxSpeed) velocity.x = maxSpeed;
            velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        }

        /// <summary>
        /// Zeroes out the velocity if an object is hit.
        /// </summary>     
        public void ApplyFix(Vector3 fix)
        {
            transform.position += fix;

            if (fix.y > 0) isGrounded = true;

            if (fix.y != 0)
            {
                velocity.y = 0;
            }
            if (fix.x != 0)
            {
                velocity.x = 0;
            }

            aabb.RecalcAABB();
        }
    }
}


