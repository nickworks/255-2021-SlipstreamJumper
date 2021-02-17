using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Miller
{
    /// <summary>
    /// This class gets input and moves the player 
    /// with the input and with Euler physics.
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        /// <summary>
        /// When the player wants to move, this value
        /// is used to accelerate the player.
        /// </summary>
        [Header ("Horizontal Movement")]
        public float scalarAcceleration = 5;

        /// <summary>
        /// This value is used to scale the player's deceleration.
        /// </summary>
        public float scalarDeceleration = 40;

        /// <summary>
        /// This value is used to clamp the player's horizontal velocity. Measured in meters/second
        /// </summary>
        public float maxSpeed = 5;

        /// <summary>
        /// This is used to scale the player's downward acceleration due to gravity.
        /// </summary>
        [Header ("Vertical Movement")]
        public float gravity = 10;

        /// <summary>
        /// The velocity we launch the player when they jump. Measured in meters/second.
        /// </summary>
        public float jumpImpuse = 10;

        /// <summary>
        /// This is the current velocity of the player, in meters/second
        /// </summary>
        private Vector3 velocity = new Vector3();

        /// <summary>
        /// Whether or not the player is currently jumoing upwards
        /// </summary>
        private bool isJumpingUpwards = false;
        private bool isGrounded = false;
        private AABB aabb;

        private void Start()
        {
            aabb = GetComponent<AABB>();
        }

        /// <summary>
        /// Do Euler physics each tick
        /// </summary>
        void Update()
        {
            CalcHorizontalMovement();

            CalcVerticalMovement();

            //applying our velocity to our position
            transform.position += velocity * Time.deltaTime;
            aabb.RecalcAABB();
            isGrounded = false;
        }
        /// <summary>
        /// Calculating Euler physics on Y axis
        /// </summary>
        private void CalcVerticalMovement()
        {
            float gravMultiplier = 1;

            bool wantstoJump = Input.GetButtonDown("Jump");
            bool isHoldingJump = Input.GetButton("Jump");

            if (wantstoJump && isGrounded)
            {
                velocity.y = jumpImpuse;
                isJumpingUpwards = true;
            }
            if(!isHoldingJump || velocity.y < 0)
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

            if (h != 0) // user is pressing left or right or (both?)
            {
                // applying acceleration to our velocity:
                velocity.x += h * Time.deltaTime * scalarAcceleration;
            }
            else // user is NOT pressing left our right
            {
                if (velocity.x > 0) // player is moving right..
                {
                    velocity.x -= scalarDeceleration * Time.deltaTime;
                    if (velocity.x < 0) velocity.x = 0;
                }
                if (velocity.x < 0) // player is moving left...
                {
                    velocity.x += scalarDeceleration * Time.deltaTime;
                    if (velocity.x > 0) velocity.x = 0;
                }


            }

            //if (velocity.x < -maxSpeed) velocity.x = -maxSpeed;
            //if (velocity.x < -maxSpeed) velocity.x = maxSpeed;


            //Unity Clamp
            velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        }


        public void ApplyFix(Vector3 fix)
        {
            transform.position += fix;

            if (fix.y > 0) isGrounded = true;

            if(fix.y != 0)
            {
                velocity.y = 0;
            }
            if(fix.x != 0)
            {
                velocity.x = 0;
            }

            aabb.RecalcAABB();
        }
    }
}
