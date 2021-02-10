using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelsomeno {
    /// <summary>
    /// This class gets input and moves the player
    /// with the input and Euler physics.
    /// </summary>

    public class PlayerMovement : MonoBehaviour
    {
        /// <summary>
        /// When the player wants to move, this value is used to scale the players acceleration.
        /// </summary>
        [Header("Horizontal Movement")]
        
        public float scalerAcceleration = 5;

        /// <summary>
        /// This value is used to scale the players deceleration.
        /// </summary>

        public float scalerDeceleration = 40;

        /// <summary>
        /// This value is used to clamp the players horizontal velocity, measured in meters/second
        /// </summary>

        public float maxSpeed = 5;

        /// <summary>
        /// This is used to scale the players downward acceleration due to gravity.
        /// </summary>

        [Header("Vertical Movement")]

        public float gravity = 10;

        /// <summary>
        /// The velocity we launch the player when they jump, measuered in meters/second
        /// </summary>

        public float jumpImpulse = 10;

        /// <summary>
        /// current velocity of the player in meters.second
        /// </summary>

        private Vector3 velocity = new Vector3();

        /// <summary>
        /// Whether or not the player is currently jumping upwards.
        /// </summary>

        private bool isJumpingUpwards = false;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            CalcHorizontalMovement();

            CalcVerticalMovement();

            //applying velocity to position 
            transform.position += velocity * Time.deltaTime;

        }


        /// <summary>
        /// Calculating Euler physics on Y axis
        /// </summary>

        private void CalcVerticalMovement()
        {


            float gravMultiplier = 1;

            // detect if on the ground:
            bool isGrounded = false;
            if (transform.position.y <= 0)
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


            // apply force of gravity to our velocity 
            velocity.y -= gravity * Time.deltaTime * gravMultiplier;


        }

        /// <summary>
        /// Calculating Euler physics on X axis
        /// </summary>

        private void CalcHorizontalMovement()
        {
            float h = Input.GetAxisRaw("Horizontal");

            // Euler physics integration:

            if (h != 0)
            {
                // applying acceleration to velocity
                velocity.x += h * Time.deltaTime * scalerAcceleration;

            }
            else
            {

                if (velocity.x > 0) // player is moving right
                {
                    velocity.x -= scalerDeceleration * Time.deltaTime;
                    if (velocity.x < 0) velocity.x = 0;

                }
                if (velocity.x < 0)
                {
                    velocity.x += scalerDeceleration * Time.deltaTime;
                    if (velocity.x > 0) velocity.x = 0;

                }

            }

            //if (velocity.x < -maxSpeed) velocity.x = -maxSpeed;
            //if (velocity.x >  maxSpeed) velocity.x = maxSpeed;

            velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        }

    }
}
