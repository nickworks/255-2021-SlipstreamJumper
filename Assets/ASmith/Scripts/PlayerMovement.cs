using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASmith
{
    /// <summary>
    /// Takes input and moves player
    /// based off Input and Euler Physics
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        /// <summary>
        /// When player wants to move
        /// this value is used to scale
        /// the player acceleration
        /// </summary>
        [Header("Horizontal Movement")]
        public float scalarAcceleration = 5;
        /// <summary>
        /// This value is used to scale the player decelerattion
        /// </summary>
        public float scalarDeceleration = 40;
        /// <summary>
        /// This value is used to clamp the player's velocity. Measured in meters per sec
        /// </summary>
        public float maxSpeed = 5;

        /// <summary>
        /// This value is used to scale the player's downward acceleration due to gravity
        /// </summary>
        [Header("Vertical Movement")]
        public float gravity = 10;
        /// <summary>
        /// The velocity we launch the player when they jump. Measured in meters per sec
        /// </summary>
        public float jumpImpulse = 10;

        /// <summary>
        /// Current velocity applied to player. Measured in meters per sec
        /// </summary>
        public Vector3 velocity = new Vector3();
        /// <summary>
        /// Whether or not player is currently jumping upwards
        /// </summary>
        private bool isJumpingUpwards = false;

        /// <summary>
        /// Do Euler physics each tick
        /// </summary>
        void Update()
        {
            CalcHorizontalMovement();
            CalcVerticalMovement();

            // Applies velocity to position
            transform.position += velocity * Time.deltaTime;
        }

        /// <summary>
        /// Calculates Euler physics on y axis
        /// </summary>
        private void CalcVerticalMovement()
        {
            float gravMultiplier = 1;
            bool isGrounded = false;
            // Detect if on ground
            if (transform.position.y < 0) // On ground
            {
                Vector3 pos = transform.position;
                pos.y = 0;
                transform.position = pos;
                velocity.y = 0;
                isGrounded = true;
            }

            bool wantsToJump = Input.GetButtonDown("Jump");
            bool isHoldingJump = Input.GetButton("Jump");

            if (wantsToJump && isGrounded)
            {
                velocity.y = jumpImpulse;
                isJumpingUpwards = true;
            }
            if (!isHoldingJump || velocity.y < 0)
            {
                isJumpingUpwards = false;
            }
            if (isJumpingUpwards == true) gravMultiplier = 0.5f;

            // Apply force of gravity to velocity
            velocity.y -= gravity * Time.deltaTime * gravMultiplier;

        }

        /// <summary>
        /// Calculates Euler physics on x axis
        /// </summary>
        private void CalcHorizontalMovement()
        {
            float h = Input.GetAxisRaw("Horizontal"); // Use raw to avoid the built in acceleration and deceleration

            // Eueler Physics Integration
            if (h != 0) // User is pressing left/right
            {
                // accelerate
                velocity.x += h * Time.deltaTime * scalarAcceleration;
            }
            else // User not pressing left/right
            {
                if (velocity.x > 0) // Player moving right
                {
                    velocity.x -= scalarDeceleration * Time.deltaTime;
                }
                if (velocity.x < 0) // Player moving left
                {
                    velocity.x += scalarDeceleration * Time.deltaTime;
                    if (velocity.x > 0) velocity.x = 0;
                }
            }

            velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        }
    }
}

