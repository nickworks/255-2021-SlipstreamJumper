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
        public float maxSpeed = 20;

        /// <summary>
        /// This is used to scale the player's downward acceleration due to gravity.
        /// </summary>
        [Header ("Vertical Movement")]
        public float gravity = 20;

        /// <summary>
        /// The velocity we launch the player when they jump. Measured in meters/second.
        /// </summary>
        public float jumpImpulse = 10;

        /// <summary>
        /// The maximum fall speed for the player. (helps with discrete collision detection)
        /// </summary>
        public float terminalVelocity = 10;

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
            if (Time.deltaTime > 0.25f) return; // lag spike? quit early, do nothing


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


            // clamp vertical speed to create terminal velocity
            if (velocity.y < -terminalVelocity) velocity.y = -terminalVelocity;

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
                float accel = scalarAcceleration;


                // applying acceleration to our velocity:
                velocity.x += h * Time.deltaTime * accel;
            }
            else // user is NOT pressing left our right
            {


                float decel = scalarDeceleration;

                if (!isGrounded) // less deceleration while in the air
                {
                    decel = scalarDeceleration / 3;
                }


                if (velocity.x > 0) // player is moving right..
                {
                    velocity.x -= decel * Time.deltaTime;
                    if (velocity.x < 0) velocity.x = 0;
                }
                if (velocity.x < 0) // player is moving left...
                {
                    velocity.x += decel * Time.deltaTime;
                    if (velocity.x > 0) velocity.x = 0;
                }


            }

            //if (velocity.x < -maxSpeed) velocity.x = -maxSpeed;
            //if (velocity.x < -maxSpeed) velocity.x = maxSpeed;


            //Unity Clamp
            velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        }

        /// <summary>
        /// Moves the player by adding a vector to its position
        /// The vector represents a "fix" that should move the player out of another object
        /// From the fix, we can deduce direction the player was moved in
        /// </summary>
        /// <param name="fix"></param>
        public void ApplyFix(Vector3 fix)
        {
            transform.position += fix;


            if(fix.y != 0) // move player up or down
            {
            velocity.y = 0;
            if (fix.y > 0) isGrounded = true; // if move player up, player is standing on ground
            }
            if(fix.x != 0) // move player left or right
            {
                velocity.x = 0;
            }

            aabb.RecalcAABB();
        }

        public void LaunchPlayer(Vector3 vel)
        {
            velocity.z = 0;
            velocity = vel;
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Pick Up"))
            {
                other.gameObject.SetActive(false);
            }
        }
    }
}
