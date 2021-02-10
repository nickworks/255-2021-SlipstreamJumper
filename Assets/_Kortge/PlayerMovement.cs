using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Write documentation before every class and class member (variables).
namespace Kortge
{
    /// <summary>
    /// This class gets input and moves the player with input and euler physics.
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        /// <summary>
        /// When the player wants to move, this value is used to scale the player's deceleration.
        /// </summary>

        [Header("Horizontal Movement")]

        public float scalarAcceleration;

        /// <summary>
        /// This value is used to decelerate the player.
        /// </summary>

        public float scalarDeceleration;

        /// <summary>
        /// This value is used to clamp the player's horizontal velocity.
        /// </summary>

        public float maxSpeed = 5;

        /// <summary>
        /// This is used to scale the player's downward acceleration due to gravity.
        /// </summary>

        [Header("Horizontal Movement")]

        public float gravity;
        /// <summary>
        /// The velocity we launch the player when they jump. Measured in meters/second.
        /// </summary>
        public float jumpImpulse = 10;
        /// <summary>
        /// The current velocity of player.
        /// </summary>
        private Vector3 velocity = new Vector3();
        /// <summary>
        /// Whether or not the player is currently jumping upwards.
        /// </summary>
        private bool isJumpingUpwards = false;

        /// <summary>
        /// Do euler physics each tick.
        /// </summary>
        // Start is called before the first frame update

        public AABB aabb;
        private bool isGrounded = false;
        void Start()
        {
            aabb = GetComponent<AABB>();
        }

        // Update is called once per frame
        void Update()
        {
            CalcHorizontaMovement();
            CalcVerticalMovement();

            // applying velocity to our position:
            transform.position += velocity * Time.deltaTime;

            isGrounded = false;

            aabb.RecalcAABB();
        }
        /// <summary>
        /// Calculating the Euler physics on Y axis.
        /// </summary>
        /// 

        private void CalcVerticalMovement()
        {
            float gravMultiplier = 1;

            bool wantsToJump = Input.GetButtonDown("Jump");
            bool isHoldingJump = Input.GetButton("Jump");

            if (wantsToJump && isGrounded)
            {
                velocity.y = 10;
                isJumpingUpwards = true;
            }

            if (!isHoldingJump || velocity.y < 0)
            {
                isJumpingUpwards = false;
            }

            if (isJumpingUpwards) gravMultiplier = 0.5f;

            // apply gravity to velocity
            velocity.y -= gravity * gravMultiplier * Time.deltaTime;

        }

        private void CalcHorizontaMovement()
        {
            float h = Input.GetAxisRaw("Horizontal");

            if (h != 0)
            { // user is pressing left or right (or both?)
              // applying acceleration to our velocity:
                velocity.x += h * Time.deltaTime * scalarAcceleration;
            }

            else // user is NOT pushing left or right:
            {
                if (velocity.x > 0) // player is moving right...
                {
                    velocity.x -= scalarDeceleration * Time.deltaTime;
                    if (velocity.x < 0) { velocity.x = 0; }
                }

                if (velocity.x < 0)
                {
                    velocity.x += scalarDeceleration * Time.deltaTime;
                    if (velocity.x > 0) { velocity.x = 0; }
                }
            }

            //if (velocity.x < -maxSpeed) velocity.x = -maxSpeed;
            //if (velocity.x > maxSpeed) velocity.x = -maxSpeed;

            // unity claim
            velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        }

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
    }
}