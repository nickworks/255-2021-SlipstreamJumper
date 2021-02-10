using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Szczesniak {
    /// <summary>
    /// This class get input and moves the player 
    /// with the input and with Euler physics
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        /// <summary>
        /// This value is used to scale the player's deceleration.
        /// </summary>
        [Header("Horizontal Movement")]
       
        public float scalerDeceleration = 40;

        /// <summary>
        /// When the player wants to move, this value is used to accelerate the player.
        /// </summary>
        public float scalerAcceleration = 5;

        /// <summary>
        /// This calue is used to clamp the player's horizontal velocity, measured in meters/second.
        /// </summary>
        public float maxSpeed = 5;

        /// <summary>
        /// This is used to scale the player's downward accleration due to gravity.
        /// </summary>
        [Header("Vertical Movement")]

        public float gravity = 50;
        
        /// <summary>
        /// The velocity we launch the player when they jump
        /// </summary>
        public float jumpImpulse = 15;

        /// <summary>
        /// The current velocity to apply to the player, in meters/second.
        /// </summary>
        private Vector3 velocity = new Vector3();

        /// <summary>
        /// Whether or not the player is currently jumping upwards.
        /// </summary>
        private bool isJumpingUpwards = false;

        
        /// <summary>
        /// Do Euler physics each tick.
        /// </summary>
        void Update() {
            MovementHorizontal();
            
            CalcVerticalMovement();


            // applying our velocity to our position:
            transform.position += velocity * Time.deltaTime;
        }

        /// <summary>
        /// Calculating Euler physics on Y axis
        /// </summary>
        private void CalcVerticalMovement() {

            float gravMultiplier = 1;

            // detect if on ground
            bool isGrounded = false;
            if (transform.position.y <= 0) {

                Vector3 pos = transform.position;
                pos.y = 0;
                transform.position = pos;
                velocity.y = 0;
                isGrounded = true;
            }

            bool wantsToJump = Input.GetButtonDown("Jump");

            bool isHoldingJump = Input.GetButton("Jump");

            if (wantsToJump && isGrounded) {
                velocity.y = jumpImpulse;
                isJumpingUpwards = true;
            }
            if (!isHoldingJump || velocity.y < 0) {
                isJumpingUpwards = false;
            }

            if (isJumpingUpwards) gravMultiplier = 0.5f;

            // apply force of gravtiy to our velocity:
            velocity.y -= gravity * Time.deltaTime * gravMultiplier;

        }

        /// <summary>
        /// Calculating Euler physics on X axis
        /// </summary>
        private void MovementHorizontal() {
            float h = Input.GetAxis("Horizontal");

            //transform.position += Vector3.right * h * Time.deltaTime;

            // ======== Euler physics intergration ========

            if (h != 0)
            { // user is pressing left or right (or both)
                // applying acceleration to our velocity
                velocity.x += h * Time.deltaTime * scalerAcceleration;
            }
            else
            { // user is NOT pushing left or right

                if (velocity.x > 0)
                { // player is moving right
                    velocity.x -= scalerDeceleration * Time.deltaTime;
                    if (velocity.x < 0) velocity.x = 0;
                }
                if (velocity.x < 0)
                { // player is mocing left
                    velocity.x += scalerDeceleration * Time.deltaTime;
                    if (velocity.x > 0) velocity.x = 0;
                }
            }

            //if (velocity.x < -maxSpeed) velocity.x = -maxSpeed;
            //if (velocity.x > maxSpeed) velocity.x = maxSpeed;

            velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        }
    }
}
