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
        private bool isGrounded = false;

        private AABB aabb;

        /// <summary>
        /// A reference to an "Animator Controller" which is an animation state machine
        /// </summary>
        private Animator anim;

        private AudioSource soundPlayer;

        private void Start()
        {
            aabb = GetComponent<AABB>();
            anim = GetComponentInChildren<Animator>();
            soundPlayer = GetComponentInChildren<AudioSource>();
        }

        /// <summary>
        /// Do Euler physics each tick
        /// </summary>
        void Update()
        {
            if (Time.deltaTime > 0.25f) return; // quit early, do nothing, if lag spike

            // communicates to anim controller when to switch animations
            anim.SetBool("isGrounded", isGrounded);

            CalcHorizontalMovement();
            CalcVerticalMovement();

            // Applies velocity to position
            transform.position += velocity * Time.deltaTime;

            aabb.RecalcAABB();
            isGrounded = false;
        }

        /// <summary>
        /// Calculates Euler physics on y axis
        /// </summary>
        private void CalcVerticalMovement()
        {
            float gravMultiplier = 1;

            bool wantsToJump = Input.GetButtonDown("Jump");
            bool isHoldingJump = Input.GetButton("Jump");

            if (wantsToJump && isGrounded) // start jumping
            {
                velocity.y = jumpImpulse;
                isJumpingUpwards = true;
                isGrounded = false;

                //SoundEffectBoard.PlayJump(transform.position); // plays jump audio on jump at the player position (still considered a 3D sound so doesn't work)
                SoundEffectBoard.PlayJump2(); // plays jump audio on jump
            }
            if (!isHoldingJump || velocity.y < 0)
            {
                isJumpingUpwards = false;
            }
            if (isJumpingUpwards == true) gravMultiplier = 0.5f;

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
            float h = Input.GetAxisRaw("Horizontal"); // Use raw to avoid the built in acceleration and deceleration
            // h = 1; // player always moves right

            // Eueler Physics Integration
            if (h != 0) // User is pressing left/right
            {
                float accel = scalarAcceleration;

                if (!isGrounded) // less acceleration while in air
                {
                    accel *= 5;
                }
                // accelerate
                velocity.x += h * Time.deltaTime * accel;
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

