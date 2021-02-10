using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hopkins
{
 
    /// <summary>
    /// Class gets input and moves the player based on the input obtained, and Euler physics.
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
       /// <summary>
       /// This value is used to scale the player's acceleration.
       /// </summary>
        [Header ("Horizontal Movement")]
        public float acceleration = 20;
        /// <summary>
        /// This value is used to scale the player's deceleration.
        /// </summary>
        public float deceleration = 50;
        /// <summary>
        /// This value is used to clamp the player's horizontal velocity.
        /// </summary>
        public float maxSpeed = 5;
        /// <summary>
        /// This is used to scale the player's downward acceleration due to gravity.
        /// </summary>
        [Header("Vertical Movement")]
        public float gravity = 10;
        /// <summary>
        /// The velocity we launch the player when they jump. Measured in m/s.
        /// </summary>
        public float jumpImpulse = 10;

        /// <summary>
        /// Whether or not the player is currently jumping upwards.
        /// </summary>
        private bool isJumpingUpwards = false;
        /// <summary>
        /// The current velocity of the player in m/s.
        /// </summary>
        private Vector3 velocity = new Vector3();

        void Start()
        {

        }

        void Update()
        {
            HorizontalMovement();
            VerticalMovement();
            //Euler physics stuff
            //applying velocity to position
            transform.position += velocity * Time.deltaTime;
        }
        /// <summary>
        /// Calculating Euler physics on X axis.
        /// </summary>
        private void HorizontalMovement()
        {
            float h = Input.GetAxis("Horizontal");

            if (h != 0) //user pressing left or right
            {
                //applying acceleration to velocity
                velocity.x += h * Time.deltaTime * acceleration;
            }
            else //user NOT pressing left or right
            {
                if (velocity.x > 0)
                {
                    velocity.x -= deceleration * Time.deltaTime;
                    if (velocity.x < 0)
                    {
                        velocity.x = 0;
                    }
                }
                if (velocity.x < 0)
                {
                    velocity.x += deceleration * Time.deltaTime;
                    if (velocity.x > 0)
                    {
                        velocity.x = 0;
                    }
                }
            }
            velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);

        }
        /// <summary>
        /// Calculating Euler physics on Y axis.
        /// </summary>
        private void VerticalMovement()
        {
            float gravMultiplier = 1;
            bool wantsToJump = Input.GetButtonDown("Jump");
            bool isHoldingJump = Input.GetButton("Jump");
            bool isGrounded = true;

            if (transform.position.y <- 0)
            {
                Vector3 pos = transform.position;
                pos.y = 0;
                transform.position = pos;
                velocity.y = 0;
                isGrounded = true;
            }
            if (wantsToJump && isGrounded)
            {
                velocity.y = 10*jumpImpulse;
                isJumpingUpwards = true;
            }
            if (!isHoldingJump)
            {
                isJumpingUpwards = false;
            }
            if (isJumpingUpwards)
            {
                gravMultiplier = 0.5f;
            }

            velocity.y -= gravity * Time.deltaTime * gravMultiplier;
        }
    }
}
