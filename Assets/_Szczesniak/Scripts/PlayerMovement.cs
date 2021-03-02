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
        /// These variables are for the horizontal movement of the player:
        /// </summary>
        [Header("Horizontal Movement")]

        /// <summary>
        /// This value is used to scale the player's deceleration.
        /// </summary>
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
        /// This integer is for when the player collects the powerups to boast their speed 
        /// </summary>
        public int powerUpSpeed = 0;

        /// <summary>
        /// These varaiables are for the vertical movement of the player:
        /// </summary>
        [Header("Vertical Movement")]
       
        /// <summary>
        /// This is used to scale the player's downward accleration due to gravity.
        /// </summary>
        public float gravity = 50;
        
        /// <summary>
        /// The velocity we launch the player when they jump
        /// </summary>
        public float jumpImpulse = 15;

        /// <summary>
        /// When the player jumps this will turn true to be able to double jump
        /// </summary>
        private bool committedJump = false;

        /// <summary>
        /// This maximum fall speed for the player. (helps with discrete collision detection)
        /// </summary>
        public float terminalVelocity = 10;

        /// <summary>
        /// The current velocity to apply to the player, in meters/second.
        /// </summary>
        private Vector3 velocity = new Vector3();

        /// <summary>
        /// This is used to check the player's Y axis velocity
        /// </summary>
        public float checkYVelocity = 0;

        /// <summary>
        /// Whether or not the player is currently jumping upwards.
        /// </summary>
        private bool isJumpingUpwards = false;

        /// <summary>
        /// This bool checks if the player is on the ground or not to know when the player can jump
        /// </summary>
        private bool isGrounded = false;

        /// <summary>
        /// A reference to an "Animation Controller", which is an animation state machine.
        /// </summary>
        //private Animator anim;

        /// <summary>
        /// This AudioSource plays the jump sound 
        /// </summary>
        //private AudioSource soundPlayer;

        /// <summary>
        /// Bring in the AABB class to calculate the platforms the player is on
        /// </summary>
        private AABB aabb;

        /// <summary>
        /// This transform checks to see if the player is going backwards
        /// </summary>
        public Transform markerPoint;

        private void Start()
        {
            aabb = GetComponent<AABB>();
            //anim = GetComponent<Animator>();
            //soundPlayer = GetComponentInChildren<AudioSource>();
        }

        /// <summary>
        /// Do Euler physics each tick.
        /// </summary>
        void Update() {

            if (Time.deltaTime > 0.25f) return; // lag spike? quit early, do nothing 

            // communicates w/ anim controller, which decides when to do the jump animation
            //anim.SetBool("isGrounded", isGrounded);

            // Calling these functions to see if we are going backwards, moving horizontally, and moving vertically.
            BackwardsCheck();

            MovementHorizontal();

            CalcVerticalMovement();

            

            // applying our velocity to our position:
            transform.position += velocity * Time.deltaTime;

            aabb.RecalcAABB(); // incase the position or size of the collider changes

            //isGrounded = false;
        }

        /// <summary>
        /// Checks to make sure that the player is moving back, if so it will make the player not move past a certain point
        /// </summary>
        private void BackwardsCheck()
        {
            // if the player moves past the markerpoint
            if (transform.position.x < markerPoint.position.x)
            {
                transform.position = new Vector3(markerPoint.position.x, transform.position.y, transform.position.z);
            }

        }

        /// <summary>
        /// Calculating Euler physics on Y axis
        /// </summary>
        private void CalcVerticalMovement() {

            float gravMultiplier = 1; // gravity multiplier


            bool wantsToJump = Input.GetButtonDown("Jump"); // When the player press the space bar

            bool isHoldingJump = Input.GetButton("Jump"); // When the player holds the space bar

            // if the player is off the ground, jumped already and presses the space bar (This is Double Jump)
            if (!isGrounded && committedJump && wantsToJump)
            {
                velocity.y = jumpImpulse; // adds impulse to the y axis of the player
                committedJump = false; 
                SoundEffectBoard.PlayJump2(); // plays sound effect for jump
            }
            
            // if the player presses the space bar and is on the ground (This is Jump)
            if (wantsToJump && isGrounded) {
                velocity.y = jumpImpulse;
                isJumpingUpwards = true;
                isGrounded = false;
                //soundPlayer.Play();
                committedJump = true;

                SoundEffectBoard.PlayJump2();
            }

            // if the player is not holding space bar and their y velocity is less than 0
            if (!isHoldingJump || velocity.y < 0) {
                isJumpingUpwards = false;
            }
           
            // if the player is holding space bar the gravity multiplier lessens
            if (isJumpingUpwards) gravMultiplier = 0.5f; 

            checkYVelocity = velocity.y;

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

                // store scalerAcceleration into float accel
                float accel = scalerAcceleration;

                /*if (!isGrounded) // less acceleration while in air
                {
                    accel = scalerAcceleration / 2;
                }*/

                // applying acceleration to our velocity
                velocity.x += h * Time.deltaTime * accel;
            }
            else { // user is NOT pushing left or right

                // store scalerDeceleration into float decel
                float decel = scalerDeceleration;

                if (!isGrounded) {
                    decel = scalerDeceleration / 2; // less deceleration in air
                }

                if (velocity.x > 0) { // player is moving right and is not pressing anything 
                    velocity.x -= decel * Time.deltaTime; // slows the player until stoped
                    if (velocity.x < 0) velocity.x = 0; // if velocity is below 0 it keeps it assigned 0 to prevent unneccessary movement
                }
                if (velocity.x < 0) { // player is mocing left and is not pressing anything
                    velocity.x += decel * Time.deltaTime;
                    if (velocity.x > 0) velocity.x = 0;
                }
            }

            //if (velocity.x < -maxSpeed) velocity.x = -maxSpeed;
            //if (velocity.x > maxSpeed) velocity.x = maxSpeed;

            // Clams the player's reverse and forward speed, player can go faster when aquirering powerups
            velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed + powerUpSpeed);
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

        /// <summary>
        /// This launches hte player when they overlap with a Spring Block
        /// </summary>
        /// <param name="vel"></param>
        public void LaunchPlayer(Vector3 vel) {
            vel.z = 0;
            this.velocity = vel;
        }

        /*
        private void PlayerRotating() {
            float rotationAccel = scalerAcceleration;

            if (Input.GetKey("left") && !isGrounded) {
                transform.rotation = Quaternion.Euler(0, 0, velocity.z += rotationAccel * Time.deltaTime);
            }

            if (Input.GetKey("right") && !isGrounded) {
                
            }
        }*/
    }
}
