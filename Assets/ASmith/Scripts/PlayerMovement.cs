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
        /// The velocity we launch the player when they dash. Measuered in meters per sec
        /// </summary>
        public float dashImpulse = 500;
        /// <summary>
        /// Maximum fall speed for player
        /// </summary>
        public float terminalVelocity = 10;

        /// <summary>
        /// Current velocity applied to player. Measured in meters per sec
        /// </summary>
        public Vector3 velocity = new Vector3();

        /// <summary>
        /// Whether or not player is currently jumping upwards
        /// </summary>
        private bool isJumpingUpwards = false;
        /// <summary>
        /// Whether or not the player can double jump
        /// </summary>
        private bool canDoubleJump = false;
        /// <summary>
        /// Whether or not the payer is on the ground
        /// </summary>
        private bool isGrounded = false;
        /// <summary>
        /// Whether or not the player is standing still
        /// </summary>
        private bool isIdle = true;
        /// <summary>
        /// Whether or not player is currently dashing
        /// </summary>
        private bool isDashing = false;

        /// <summary>
        /// Gets a reference to the player's current location
        /// </summary>
        public Transform playerLocation;
        /// <summary>
        /// Gets a reference to the camera used in game
        /// </summary>
        public Transform cam;

        /// <summary>
        /// Gets a reference to the AABB class to be used for movement and collision detection
        /// </summary>
        private AABB aabb;

        /// <summary>
        /// A reference to an "Animator Controller" which is an animation state machine
        /// </summary>
        private Animator anim;

        //private AudioSource soundPlayer;

        private void Start()
        {
            aabb = GetComponent<AABB>();
            anim = GetComponentInChildren<Animator>();
            //soundPlayer = GetComponentInChildren<AudioSource>();
            //cam = GetComponent<Camera>();
        }

        /// <summary>
        /// Do Euler physics each tick
        /// </summary>
        void Update()
        {
            if (Time.deltaTime > 0.25f) return; // quit early, do nothing, if lag spike
            
            anim.SetBool("isGrounded", isGrounded); // communicates to animation controller whether or not the payer isGrounded            
            anim.SetBool("isIdle", isIdle); // communicates to animation controller whether or not the payer isIdle

            CalcHorizontalMovement(); // Runs HorizontalMovement method
            CalcVerticalMovement(); // Runs VerticalMovement method

            // Applies velocity to position
            transform.position += velocity * Time.deltaTime;

            aabb.RecalcAABB();
            isGrounded = false; // sets isGrounded to false every frame

            if (playerLocation.position.x < cam.position.x - 14) // If the player is off the left edge of the screen they die
            {
                // Had trouble calling this function from HealthSystem so recreated it here
                Destroy(gameObject); // Kills the player if they end up off the left edge of the screen
                SoundEffectBoard.PlayDie(); // plays death audio
            }else  if (playerLocation.position.x > cam.position.x + 12.5f) // If the player is touching the right edge of the screen restrict their movement
            {
                velocity.x = 0; // Player can no longer move right if touching the right edge of the screen
            }

        }

        /// <summary>
        /// Calculates Euler physics on y axis
        /// </summary>
        private void CalcVerticalMovement()
        {
            float gravMultiplier = 1; // Sets the gravity Multiplier to 1

            bool wantsToJump = Input.GetButtonDown("Jump"); // If pressing spacebar, wantsToJump = true
            bool isHoldingJump = Input.GetButton("Jump"); // If pressing spacebar, isHoldingJump = true
            bool wantsToDoubleJump = Input.GetButton("Jump"); // If pressing spacebar, wantsToDoubleJump = true

            if (wantsToJump && isGrounded) // Jump
            {
                print("jump");
                velocity.y = 0; // sets velocity on y-axis to 0
                velocity.y = jumpImpulse; // sets velocity on y-axis to the jumpImpulse value
                isJumpingUpwards = true; // sets isJumpingUpwards to true
                isGrounded = false; // sets isGrounded to false
                canDoubleJump = true; // sets canDoubleJump to true

                //SoundEffectBoard.PlayJump(transform.position); // plays jump audio on jump at the player position (still considered a 3D sound so DOES NOT WORK)
                SoundEffectBoard.PlayJump2(); // plays jump audio on jump
            }
            if (!isHoldingJump || velocity.y < 0) 
            {
                isJumpingUpwards = false; // If not holding jump, jumpingUpwards = false
            }
            if (wantsToDoubleJump && canDoubleJump && !isJumpingUpwards) // Double Jump
            {
                print("Ju-Jump");
                canDoubleJump = false; // sets canDoubleJump to false
                velocity.y = 0; // sets velocity on y-axis to 0
                velocity.y = jumpImpulse; // sets velocity on y-axis to jumpImpulse value
                SoundEffectBoard.PlayDoubleJump(); // plays double jump audio
            }
            if (isJumpingUpwards == true) gravMultiplier = 0.5f; // if you are jumping half the gravity multiplier to allow more air time

            // Apply force of gravity to velocity
            velocity.y -= gravity * Time.deltaTime * gravMultiplier;

            // clamp vertical speed to create terminal velocity
            if (velocity.y < -terminalVelocity) velocity.y = -terminalVelocity;

        }

        /// <summary>
        /// Calculates Euler physics on x axis
        /// </summary>
        private void CalcHorizontalMovement()
        {
            bool wantsToDash = Input.GetKeyDown("left shift");
            float h = Input.GetAxisRaw("Horizontal"); // Use raw to avoid the built in acceleration and deceleration
            // h = 1; // player always moves right

            // Eueler Physics Integration
            if (h != 0) // User is pressing left/right
            {
                float accel = scalarAcceleration;

                if (!isGrounded) // more acceleration while in air for air-control
                {
                    accel *= 5;
                }
                // accelerate
                velocity.x += h * Time.deltaTime * accel;
                isIdle = false;

                if (wantsToDash && !isDashing)
                {
                    // TODO: Make dash work
                    print("DASH");
                    isDashing = true;
                    velocity.x = h * dashImpulse * accel * Time.deltaTime;
                    // TODO: Create dash sound

                    isDashing = false;
                }
            }
            else // User not pressing left/right
            {
                float decel = scalarDeceleration; // less deceleration while in air

                if (!isGrounded)
                {
                    decel /= 5;
                }
                if (velocity.x > 0) // Player moving right
                {
                    velocity.x -= decel * Time.deltaTime;
                }
                if (velocity.x < 0) // Player moving left
                {
                    velocity.x += decel * Time.deltaTime;
                    if (velocity.x > 0) velocity.x = 0;
                }
                isIdle = true;
            }

            velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        }

        /// <summary>
        /// Moves player by adding a vector to its position
        /// The vector represents a "fix" that moves the player
        /// out of other objects. From the fix we can see which 
        /// direction the player was moved
        /// </summary>
        /// <param name="fix">How far to move the player</param>
        public void ApplyFix(Vector3 fix)
        {
            transform.position += fix;

            if (fix.y != 0) // move player up/down
            {
                velocity.y = 0;
                if (fix.y > 0) isGrounded = true;
            }

            if (fix.x != 0) // move player left/right
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

