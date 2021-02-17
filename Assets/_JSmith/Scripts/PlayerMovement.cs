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
        /// The current velocity of the player in meters/second.
        /// </summary>
        private Vector3 velocity = new Vector3();

        /// <summary>
        /// Whether or not the player is currently jumping upards.
        /// </summary>
        private bool isJumpingUpwards = false;

        /// <summary>
        /// Do euler physics each tick.
        /// </summary>
        void Update()
        {
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

        }

        /// <summary>
        /// Calc euler physics on the X axis.
        /// </summary>
        private void CalcHorizontalMovement()
        {
            float h = Input.GetAxisRaw("Horizontal");

            //Euler physics intergration:

            if (h != 0) // user is pressing left or right(or both?)
            {
                //applying acceleration to our velocity
                velocity.x += h * Time.deltaTime * scalerAcceleration;
            }

            else
            { // user is not pushing left or right

                if (velocity.x > 0) // player is moving right...
                {
                    velocity.x -= scalerDeceleration * Time.deltaTime;
                    if (velocity.x < 0) velocity.x = 0;
                }
                if (velocity.x < 0) // player is moving left
                {
                    velocity.x += scalerDeceleration * Time.deltaTime;
                    if (velocity.x > 0) velocity.x = 0;
                }
            }

            velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        }
    }
}
