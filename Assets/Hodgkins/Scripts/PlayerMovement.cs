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
        /// The maximum fall speed for the player.
        /// </summary>
        public float terminalVelocity = 10;

        /// <summary>
        /// The current velocity of the player, in meters/second.
        /// </summary>
        private Vector3 velocity = new Vector3();

        /// <summary>
        /// Whether or not the player is jumping upwards.
        /// </summary>
        private bool isJumpingUpwards = false;
        private bool isGrounded = false;
        
        /// <summary>
        /// Whether or not the player can double jump, 
        /// had to be made public so DobleJump can access it
        /// </summary>
        public bool canDoubleJump = false;

        private AABB aabb;
        
        private void Start()
        {
            aabb = GetComponent<AABB>();
            //soundPlayer = GetComponentInChildren<AudioSource>();
        }

        /// <summary>
        /// Do Euler physics each tick.
        /// </summary>
        void Update()
        {
            if (Time.deltaTime > 0.025f) return; // lag spike? quit early, do nothing
            
            MovementHorizontal();
            MovementVertical();

            //applying velocity to our position
            transform.position += velocity * Time.deltaTime;
            aabb.RecalcAABB();
            isGrounded = false;

        }

        /// <summary>
        /// Calculating Euler physics on Y axis.
        /// </summary>
        private void MovementVertical()
        {
            float gravMultiplier = 1;
            
            bool wantsToJump = Input.GetButtonDown("Jump"); // spacebar or 'A' button
            bool isHoldingJump = Input.GetButton("Jump");
            

            if (wantsToJump && isGrounded) // regular jump
            {
                velocity.y = jumpInpulse;
                isJumpingUpwards = true;
                isGrounded = false;

                SoundEffectBoard.PlayJump();                
            } else if (canDoubleJump && !isGrounded && wantsToJump) // double jump
            {
                velocity.y = jumpInpulse;
                isJumpingUpwards = true;
                SoundEffectBoard.PlayJump();
                canDoubleJump = false;
            }
            if (!isHoldingJump) { // if jump button is let go, immeadiately start to fall
                isJumpingUpwards = false;
            }
            if (isJumpingUpwards) gravMultiplier = 0.5f; // jump button held, longer floatier jump
            
            //apply force of gravity to velocity
            velocity.y -= gravity * Time.deltaTime * gravMultiplier;

            //clamp vertical speed to create terminal velocity
            if (velocity.y < -terminalVelocity) velocity.y = -terminalVelocity;

        }

        /// <summary>
        /// Calculating Euler physics on X axis.
        /// </summary>
        private void MovementHorizontal()
        {
            float h = Input.GetAxisRaw("Horizontal"); // a & d, left & right arrows and left stick

            //h = 1; // always moving right


            //Euler physics integration
            if (h != 0) //user is pressing left or right (or both)
            {

                float accel = scalarAcceleration;
                
                if(!isGrounded) // less acceleration while in air
                {
                    accel = scalarAcceleration / 5;
                }

                //applying acceleration to velocity
                velocity.x += h * accel;
            }
            else
            { // user is not pushing left or right
                float decel = scalarDeceleration;

                if(!isGrounded)
                {
                    decel = scalarDeceleration / 5;
                }

                if (velocity.x > 0) //player is moving right
                {
                    velocity.x -= decel + Time.deltaTime;
                    if (velocity.x < 0) velocity.x = 0;
                }
                if (velocity.x < 0)
                {
                    velocity.x += decel + Time.deltaTime;
                    if (velocity.x > 0) velocity.x = 0;
                }
            }

            //if (velocity.x < maxSpeed) velocity.x = -maxSpeed;
            //if (velocity.x > maxSpeed) velocity.x =  maxSpeed;

            //unity clamp
            velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        }

        /// <summary>
        /// This moves the player by adding a vector to its position.
        /// The vector represents a 'fix' that should move the player
        /// out of another object. From the fix, we can tell which
        /// direction the player was moved.
        /// </summary>
        /// <param name="fix"></param>
        public void ApplyFix(Vector3 fix)
        {
            transform.position += fix;
            
            if (fix.y > 0) isGrounded = true;

            if(fix.y != 0) // move player up or down
            {
                velocity.y = 0;
            }
            if(fix.x != 0)
            {
                velocity.x = 0;
            }

            aabb.RecalcAABB();
        }
        /// <summary>
        /// launch player away when hitting a hazard or spring, 
        /// the value here is neutral, the items can change it in their script
        /// </summary>
        /// <param name="vel"></param>
        public void LaunchPlayer(Vector3 vel)
        {
            vel.z = 0;
            this.velocity = vel;        
        }
    }
}
