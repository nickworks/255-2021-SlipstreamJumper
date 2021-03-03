using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geib // This namespace needs to be in all of my scripts!
{

    /// <summary>
    /// This clas gets input and moves the player
    /// with the input and with Euler physics.
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        /// <summary>
        /// When the player wants to move, this value
        /// is used to scale the player's horizontal acceleration.
        /// </summary>
        [Header("Horizontal Movement")]

        public float scalarAcceleration = 50;

        /// <summary>
        /// This value is used to scale the player's horizontal deceleration.
        /// </summary>
        public float scalarDeceleration = 40;

        /// <summary>
        /// This value is used to clamp the player's horizontal velocity.
        /// measured in meters/second.
        /// </summary>
        public float maxSpeed = 5;

        /// <summary>
        /// This value is used to scale the player's downward acceleration due do gravity.
        /// </summary>
        [Header("Vertical Movement")]
        public float gravity = 50;
        /// <summary>
        /// The velocity we launch the player when we jump.
        /// Measured in meters/second.
        /// </summary>
        public float jumpImpulse = 10;

        /// <summary>
        /// The maximum speed at which the player may fall. (Should be no more than half the height of the player to ensure they do not clip through the ground.)
        /// </summary>
        public float terminalVelocity = 15;
        
        /// <summary>
        /// Whether or not the player is currently jumping upwards. 
        /// </summary>
        private bool isJumpingUpwards = false;
        /// <summary>
        /// The current velocity of the player in meters/second.
        /// </summary>
        private Vector3 velocity = new Vector3();

        /// <summary>
        /// A boolean variable to determine whether or not the player is on the ground.
        /// </summary>
        private bool isGrounded = false;

        /// <summary>
        /// This is a reference to the AABB collision system
        /// </summary>
        private AABB aabb;

        /// <summary>
        /// A referecne to an "Animator Controller", which is an animation state machine
        /// </summary>
        private Animator anim;

        private AudioSource soundPlayer;

        void Start()
        {
            aabb = GetComponent<AABB>();
            anim = GetComponent<Animator>();
            soundPlayer = GetComponentInChildren<AudioSource>();
        }

        /// <summary>
        /// Do euler physics each tick.
        /// </summary>
        void Update()
        {
            if (Time.deltaTime > 0.25) return; // Lag spike? quit early, do nothing
            //print(isGrounded);

            // Communicates w/ animation controller to determine what animation needs to be running. 
            anim.SetBool("isGrounded", isGrounded);
            
            CalcMovementHorizontal();

            CalcVerticalMovement();


            // Applying our velocity to our position:
            transform.position += velocity * Time.deltaTime;

            aabb.RecalcAABB();
            isGrounded = false;
        }

        /// <summary>
        /// Calculating Euler physics on y-axis.
        /// </summary>
        private void CalcVerticalMovement()
        {

            float gravMultiplier = 1;


            bool wantsToJump = Input.GetButtonDown("Jump");

            bool isHoldingJump = Input.GetButton("Jump");

            // Jump!
            if(wantsToJump && isGrounded)
            {
                velocity.y = jumpImpulse;
                isJumpingUpwards = true;
                isGrounded = false;

                SoundEffectBoard.PlayJump2();


            }
            if(!isHoldingJump || velocity.y < 0)
            {
                isJumpingUpwards = false;
            }

            if (isJumpingUpwards) gravMultiplier = 0.5f;

            // apply force of gravity to our velocity
            velocity.y -= gravity * Time.deltaTime * gravMultiplier;

            //clamp vertical terminal velocity
            if (velocity.y < -terminalVelocity) velocity.y = -terminalVelocity;

        }



        /// <summary>
        /// Calculating Euler physics on the x-axis
        /// </summary>
        private void CalcMovementHorizontal()
        {
            float h = Input.GetAxisRaw("Horizontal");
            
            //h = 1;

            // --== Euler physic integration ==--

            // Alternate Solution:
            //transform.position += Vector3.right * h * Time.deltaTime;

            if (h != 0) // user is pushing left or right (or both?
            {
                // applying acceleration to our velocity:
                velocity.x += h * Time.deltaTime * scalarAcceleration;
            }
            else // user is NOT pushing left or right
            {
                if (velocity.x > 0) // Player is moving right
                {
                    velocity.x -= scalarDeceleration * Time.deltaTime;
                    if (velocity.x < 0) velocity.x = 0; // Don't cange directions.
                }
                if (velocity.x < 0) // Player is moving left
                {
                    velocity.x += scalarDeceleration * Time.deltaTime;
                    if (velocity.x > 0) velocity.x = 0; // Don't change directions.
                }
            }

            // Manual Clamp
            // if (velocity.x < -maxSpeed) velocity.x = -maxSpeed;
            //if (velocity.x > maxSpeed) velocity.x = maxSpeed;

            // Unity Clamp function
            velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        }

        /// <summary>
        /// This moves the player by adding a vector ro its position.
        /// The vector repreents a "fix" that should move the player 
        /// out of another object. From the fix, we can deduce which
        /// direction the player has moved
        /// </summary>
        /// <param name="fix"></param>How far to move the player</param>
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
        public void LaunchPlayer(Vector3 vel)
        {
            vel.z = 0;
            this.velocity = vel;
        }
    }
}
