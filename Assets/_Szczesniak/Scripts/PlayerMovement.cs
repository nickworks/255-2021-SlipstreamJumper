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
        /// This maximum fall speed for the player. (helps with discrete collision detection)
        /// </summary>
        public float terminalVelocity = 10;

        /// <summary>
        /// The current velocity to apply to the player, in meters/second.
        /// </summary>
        private Vector3 velocity = new Vector3();

        /// <summary>
        /// Whether or not the player is currently jumping upwards.
        /// </summary>
        private bool isJumpingUpwards = false;
        private bool isGrounded = false;

        private AABB aabb;

        private void Start()
        {
            aabb = GetComponent<AABB>();
        }

        /// <summary>
        /// Do Euler physics each tick.
        /// </summary>
        void Update() {

            if (Time.deltaTime > 0.25f) return; // lag spike? quit early, do nothing 
            MovementHorizontal();
            
            CalcVerticalMovement();

            // applying our velocity to our position:
            transform.position += velocity * Time.deltaTime;

            aabb.RecalcAABB();

            isGrounded = false;
        }

        /// <summary>
        /// Calculating Euler physics on Y axis
        /// </summary>
        private void CalcVerticalMovement() {

            float gravMultiplier = 1;


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


            // Clamp vertical speed to create terminal velocity
            if (velocity.y < -terminalVelocity) velocity.y = -terminalVelocity;

        }

        /// <summary>
        /// Calculating Euler physics on X axis
        /// </summary>
        private void MovementHorizontal() {
            float h = Input.GetAxis("Horizontal");

            //h = 1; // always moves forward

            //transform.position += Vector3.right * h * Time.deltaTime;

            // ======== Euler physics intergration ========

            if (h != 0) { // user is pressing left or right (or both)

                float accel = scalerAcceleration;

                if (!isGrounded) // less acceleration while in air
                {
                    accel = scalerAcceleration / 2;
                }

                // applying acceleration to our velocity
                velocity.x += h * Time.deltaTime * accel;
            }
            else { // user is NOT pushing left or right

                float decel = scalerDeceleration;

                if (!isGrounded) {
                    decel = scalerDeceleration / 2; // less deceleration in air
                }

                if (velocity.x > 0) { // player is moving right
                    velocity.x -= decel * Time.deltaTime;
                    if (velocity.x < 0) velocity.x = 0;
                }
                if (velocity.x < 0) { // player is mocing left
                    velocity.x += decel * Time.deltaTime;
                    if (velocity.x > 0) velocity.x = 0;
                }
            }

            //if (velocity.x < -maxSpeed) velocity.x = -maxSpeed;
            //if (velocity.x > maxSpeed) velocity.x = maxSpeed;

            velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        }

        /// <summary>
        /// This moves the player by adding a vector to its position 
        /// The vector represents a "fix" that should move the player 
        /// out of another object. from the fix, we can deduce which
        /// direction the player was moved.
        /// </summary>
        /// <param name="fix"></param> How far 
        public void ApplyFix(Vector3 fix) {

            transform.position += fix;

            if (fix.y > 0) isGrounded = true;

            if (fix.y != 0) {
                velocity.y = 0;
            }
            if (fix.x != 0) {
                velocity.x = 0;
            }

            aabb.RecalcAABB();

        }

        public void LaunchPlayer(Vector3 vel) {
            vel.z = 0;
            this.velocity = vel;
        }
    }
}
