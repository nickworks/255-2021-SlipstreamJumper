using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JSmith
{
    /// <summary>
    /// This class gets input and moves the player
    /// with the input and with Euler physics.
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        /// <summary>
        /// When the player wants to move, this value
        /// is used to scale the player's horizontal acceleration.
        /// </summary>
        [Header("Horizontal Movement")]
        public float scalerAcceleration = 50;

        /// <summary>
        /// This value is used to scale the player's horizontal deceleration.
        /// </summary>
        public float scalerDeceleration = 40;

        /// <summary>
        /// This value is used to clamp the player's horizontal velocity. Measured in meters/second.
        /// </summary>
        public float maxSpeed = 5;

        /// <summary>
        /// This is used to scale the player's downward acceleration due to gravity.
        /// </summary>
        [Header("Vertical Movement")]
        public float gravity = 10;

        /// <summary>
        /// The velocity we lauch the player when they jump. Measured in meters/second.
        /// </summary>
        public float jumpImpulse = 15;


        /// <summary>
        /// The maximum fall speed for the player.
        /// </summary>
        public float terminalVelocity = 10;

        /// <summary>
        /// The current velocity of the player in meters/second.
        /// </summary>
        private Vector3 velocity = new Vector3();

        /// <summary>
        /// Whether or not the player is currently jumping upards.
        /// </summary>
        private bool isJumpingUpwards = false;


        private bool isGrounded = false;

        private AABB aabb;

        private void Start()
        {
            aabb = GetComponent<AABB>();
        }


        /// <summary>
        /// Do euler physics each tick.
        /// </summary>
        void Update()
        {

            if(Time.deltaTime > 0.25f)
            {
                return; // lag spike? quit early, do nothing
            }

            CalcHorizontalMovement();

            CalcVerticalMovement();


            //applying our velocity to our position
            transform.position += velocity * Time.deltaTime;

        }

        /// <summary>
        /// Calculating euler physics on Y axis.
        /// </summary>
        private void CalcVerticalMovement()
        {
            float gravMultiplier = 1;

            //detect if on ground:
            bool isGrounded = false;
            if(transform.position.y <= 0) //on the ground
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

            if (isJumpingUpwards) gravMultiplier = 0.5f;

            // applying force of gravity to our velocity:
            velocity.y -= gravity * Time.deltaTime * gravMultiplier;

            //clamp vertical speed to create terminal velocity
            if (velocity.y < -terminalVelocity) velocity.y = -terminalVelocity;

        }

        /// <summary>
        /// Calc euler physics on the X axis.
        /// </summary>
        private void CalcHorizontalMovement()
        {
            float h = Input.GetAxisRaw("Horizontal");
            
            //h = 1; // player wants to move right

            //Euler physics intergration:

            if (h != 0) // user is pressing left or right(or both?)
            {

                float accel = scalerAcceleration;

                if (!isGrounded) // Less acceleration while in the air
                {
                    accel = scalerAcceleration / 4;
                }

                //applying acceleration to our velocity
                velocity.x += h * Time.deltaTime * accel;
            }

            else
            { // user is not pushing left or right

                float decel = scalerDeceleration;

                if (!isGrounded) // less deceleration while in the air
                {
                    decel = scalerDeceleration / 4;
                }

                if (velocity.x > 0) // player is moving right...
                {
                    velocity.x -= decel * Time.deltaTime;
                    if (velocity.x < 0) velocity.x = 0;
                }
                if (velocity.x < 0) // player is moving left
                {
                    velocity.x += decel * Time.deltaTime;
                    if (velocity.x > 0) velocity.x = 0;
                }
            }

            velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        }

        /// <summary>
        /// This moves the player by adding a vector to its position.
        /// The vector represents a "fix" that should move the player
        ///out of another object. From the fix, we can deduce which
        ///direction the player was moved.
        /// </summary>
        public void ApplyFix(Vector3 fix)
        {
            transform.position += fix;

            if (fix.y > 0) isGrounded = true;

            if(fix.y != 0)
            {
                velocity.y = 0;
            }
            if(fix.x != 0)
            {
                velocity.x = 0;
            }

            aabb.RecalcAABB();
        }
    }

    
}
