using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hodgkins
{
    /// <summary>
    /// This class gets input and moves the player
    /// with the input and Euler physics.
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        /// <summary>
        /// when the player wants to move, this value 
        /// is used to scale the players acceleration.
        /// </summary>
        [Header("Horizontal Movement")]      
        public float scalarAcceleration = 50;

        /// <summary>
        /// This value is used to scale the player's  horizontal deceleration.
        /// </summary>
        public float scalarDeceleration = 40;

        /// <summary>
        /// This value is used to clamp teh players horizontal velocity.
        /// </summary>
        public float maxSpeed = 5;

        /// <summary>
        /// This is used to scale the player's downward acceleration due to gravity.
        /// </summary>
        [Header("Vertical Movement")]
        public float gravity = 10;

        /// <summary>
        /// The velocity we launch the player when we jump. Measured in meters/second.
        /// </summary>
        public float jumpInpulse = 10;

        /// <summary>
        /// The current velocity of the player, in meters/second.
        /// </summary>
        private Vector3 velocity = new Vector3();

        /// <summary>
        /// Whether or not the player is jumping upwards.
        /// </summary>
        private bool isJumpingUpwards = false;
        
        void Start()
        {

        }

        /// <summary>
        /// Do Euler physics each tick.
        /// </summary>
        void Update()
        {
            MovementHorizontal();
            MovementVertical();


            //applying velocity to our position
            transform.position += velocity * Time.deltaTime;

        }

        /// <summary>
        /// Calculating Euler physics on Y axis.
        /// </summary>
        private void MovementVertical()
        {
            float gravMultiplier = 1;
            //detect if on ground
            bool isGrounded = false;
            if(transform.position.y < 0) // on ground
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
                velocity.y = jumpInpulse;
                isJumpingUpwards = true;
            }
            if (!isHoldingJump) {
                isJumpingUpwards = false;
            }
            if (isJumpingUpwards) gravMultiplier = 0.5f;
            
            //apply force of gravity to velocity
            velocity.y -= gravity * Time.deltaTime * gravMultiplier;




        }

        /// <summary>
        /// Calculating Euler physics on X axis.
        /// </summary>
        private void MovementHorizontal()
        {
            float h = Input.GetAxisRaw("Horizontal");

            //Euler physics integration

            if (h != 0) //user is pressing left or right (or both)
            {
                //applying acceleration to velocity
                velocity.x += h * Time.deltaTime * scalarAcceleration;
            }
            else
            { // user is not pushing left or right

                if (velocity.x > 0) //player is moving right
                {
                    velocity.x -= scalarDeceleration * Time.deltaTime;
                    if (velocity.x < 0) velocity.x = 0;
                }
                if (velocity.x < 0)
                {
                    velocity.x += scalarDeceleration * Time.deltaTime;
                    if (velocity.x > 0) velocity.x = 0;
                }
            }

            //if (velocity.x < maxSpeed) velocity.x = -maxSpeed;
            //if (velocity.x > maxSpeed) velocity.x =  maxSpeed;

            //unity clamp
            velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        }
    }
}
