using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Wrap in a namespace to show the class is unique to my project.
namespace Howley 
{
    /// <summary>
    /// This class gets input and moves the player
    /// with the input, and Euler physics integration.
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        /*
        Creating our own physics code.
        Objects have position, rotation, scale (transform)
        Need to access player input, and use velocity to propel an object based on that input.
        */
        /// <summary>
        /// When the player wants to move, this value
        /// is used to scale the player's horizontal acceleration.
        /// </summary>
        [Header("Horizontal Movement")]
        // Values for speed up and slow down
        public float acceleration = 5;

        /// <summary>
        /// This value is used to scale the player's horizontal deceleration.
        /// </summary>
        public float deceleration = 40;

        /// <summary>
        /// This value is used to limit the player's maximum horizontal speed.
        /// Measured in meters/second
        /// </summary>
        public float maxSpeed = 5;

        /// <summary>
        /// This value is used to scale the player's
        /// downward acceleration due to gravity.
        /// </summary>
        [Header("Vertical Movement")]
        public float gravity = 10;

        /// <summary>
        /// The velocity we launch the player when they jump. 
        /// Measured in meters/second.
        /// </summary>
        public float jumpImpulse = 10;

        /// <summary>
        /// The terminal velocity of the player measured per tick.
        /// </summary>
        public float terminalVelocity = 10;

        // We need to store and use velocity.
        /// <summary>
        /// The current velocity of the player
        /// Measured in meters/second
        /// </summary>
        private Vector3 velocity = new Vector3();

        /// <summary>
        /// Whether or not the player is currently holding the spacebar down.
        /// </summary>
        public bool isJumpingUpwards = false;

        /// <summary>
        /// This boolean tracks whether the player is on the ground's AABB or not.
        /// </summary>
        public bool isGrounded = false;

        /// <summary>
        /// This boolean tracks if the player has double jumped in the last 5 seconds.
        /// </summary>
        public bool hasDoubleJumped = false;

        /// <summary>
        /// A reference to the AABB script.
        /// </summary>
        public AABB aabb;

        /// <summary>
        /// A reference to an "Animation Controller", or animation state machine.
        /// </summary>
        private Animator anim;

        /// <summary>
        /// Hold reference to the health system script
        /// </summary>
        private HealthSystem health;

        /// <summary>
        /// This is a cooldown for the player to be able to double jump
        /// </summary>
        private float cooldownDJ = 0;

        /// <summary>
        /// A cooldown between the first jump, and the double jump.
        /// </summary>
        private float cooldownBetweenJump = 0;

        /// <summary>
        /// The start function is called once before the first update.
        /// </summary>
        void Start()
        {
            aabb = GetComponent<AABB>();
            anim = GetComponent<Animator>();
            health = GetComponent<HealthSystem>();
        }

        /// <summary>
        /// This function updates the player's horizontal and vertical movement,
        /// and applies Euler physics each tick.
        /// </summary>
        void Update()
        {
            if (Time.deltaTime > 0.25f) return;

            HorizontalMovement();

            VerticalMovement();

            DoubleJumpCooldown();

            // Apply velocity to player position.
            transform.position += velocity * Time.deltaTime; // Adding velocity to position
            aabb.RecalcAABB();

            isGrounded = false;

            // If the player falls below the threshold, run the die function.
            if (transform.position.y <= -5) health.Die();
        }

        /// <summary>
        /// This function runs the cooldowns for double jump, and time between the first jump and the double jump.
        /// </summary>
        private void DoubleJumpCooldown()
        {
            cooldownBetweenJump -= Time.deltaTime;
            cooldownDJ -= Time.deltaTime;
            if (cooldownDJ <= 0) hasDoubleJumped = false;
        }

        /// <summary>
        /// Calculating Euler physics on the Y axis.
        /// </summary>
        private void VerticalMovement()
        {
            float gravMultiplier = 1;

            // Get players input for spacebar
            bool wantsToJump = Input.GetButtonDown("Jump");

            // True for every frame the button is held down
            bool isHoldingJump = Input.GetButton("Jump");

            if (wantsToJump && isGrounded)
            {
                velocity.y = jumpImpulse;
                isJumpingUpwards = true;
                isGrounded = false;
                cooldownBetweenJump = .2f;
            }

            if (wantsToJump && !isGrounded && !hasDoubleJumped && cooldownBetweenJump <= 0)
            {
                velocity.y = jumpImpulse;
                isJumpingUpwards = true;
                hasDoubleJumped = true;
                cooldownDJ = 5;
            }

            if (!isHoldingJump || velocity.y < 0) // If you've reached peak jump, or not holding space
            {
                isJumpingUpwards = false;
            }

            if (isJumpingUpwards) gravMultiplier = 0.5f;

            // Subtract from the y by gravity per second "GRAVITY"
            velocity.y -= gravity * Time.deltaTime * gravMultiplier;

            // Clamp vertical speed to create terminal velocity.
            if (velocity.y < terminalVelocity) velocity.y = -terminalVelocity;
        }

        /// <summary>
        /// Calculating Euler physics on the X axis.
        /// </summary>
        private void HorizontalMovement()
        {
            // Get the horizontal axis and store this in a variable. 
            float h = Input.GetAxisRaw("Horizontal");

            //h = 1;

            // === Euler physics integration. ===
            if (h != 0) // User is pressing A or D.
            {
                float accel = acceleration;

                if (!isGrounded) // Less acceleration in the air.
                {
                    accel = acceleration / 4;
                }

                // Add the player's input to the velocity x deltaTime.
                velocity.x += h * Time.deltaTime * accel;
            }
            else // User is not pressing A or D
            {
                float decel = deceleration;

                if (!isGrounded) // Less deceleration in the air.
                {
                    decel = deceleration / 3;
                }

                if (velocity.x > 0) // Player is moving right
                {
                    velocity.x -= decel * Time.deltaTime; // Accelerate left
                    if (velocity.x < 0) velocity.x = 0;
                }
                if (velocity.x < 0) // Player is moving left
                {
                    velocity.x += decel * Time.deltaTime; // Accelerate right
                    if (velocity.x > 0) velocity.x = 0;
                }
            }

            // Clamp velocity
            //if (velocity.x < -maxSpeed) velocity.x = -maxSpeed;
            //if (velocity.x > maxSpeed) velocity.x = maxSpeed;
            velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        }

        /// <summary>
        /// Zeroes out the velocity if an object is hit.
        /// The vector represents a fix to move the player out of another object.
        /// </summary>     
        public void ApplyFix(Vector3 fix)
        {
            transform.position += fix;

            if (fix.y != 0) // Move player up and down
            {
                velocity.y = 0;
                if (fix.y > 0) isGrounded = true;
            }
            if (fix.x != 0) // Move player left or right
            {
                velocity.x = 0;
            }

            aabb.RecalcAABB();
        }

        /// <summary>
        /// This function launches the player directly upwards.
        /// </summary>
        /// <param name="velocity"></param>
        public void LauchPlayer(Vector3 velocity)
        {
            velocity.z = 0;
            this.velocity = velocity; // Referring to the private property needs this. in front of it.
        }
        

    }
}


