using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Erickson
{
    public class NewBehaviourScript : MonoBehaviour
    {
        /// <summary>
        /// This value is used to scale the player's horizontal acceleration.
        /// </summary>
        [Header("Horizontal Movement")]
        public float acceleration = 5;
        /// <summary>
        /// This value is used to scale the player's horizontal deceleration.
        /// </summary>
        public float deceleration = 40;
        /// <summary>
        /// This value is used to clamp the player's velocity.
        /// </summary>
        public float maxSpeed = 5;
        /// <summary>
        /// This value is used to scale the player's downward acceleration due to gravity.
        /// </summary>
        [Header("Vertical Movement")]
        public float gravity = 20;
        /// <summary>
        /// The velocity we launch the player when they jump. Measured in meter/second.
        /// </summary>
        public float jumpImpulse = 15;
        /// <summary>
        /// The current velocity of the player in meters/second.
        /// </summary>
        private Vector3 velocity = new Vector3();
        /// <summary>
        /// Whether or not the player is jumping.
        /// </summary>
        private bool isJumpingUpwards = false;

        /// <summary>
        /// Do Euler physics each tick.
        /// </summary>
        void Update()
        {
            float h = Input.GetAxisRaw("Horizontal");

            if (h != 0)
            {
                velocity.x += h * Time.deltaTime;
            }
            else { 
            
                if(velocity.x > 0)
                {
                    velocity.x += deceleration * Time.deltaTime;
                    if (velocity.x < 0) velocity.x = 0;
                }
                if (velocity.x < 0)
                {
                    velocity.x += deceleration * Time.deltaTime;
                    if (velocity.x > 0) velocity.x = 0;
                }

            }

            // clamp:
            //if (velocity.x < -maxSpeed) velocity.x = -maxSpeed;
            //if (velocity.x > maxSpeed) velocity.x = maxSpeed;

            velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        }
        /// <summary>
        /// Calculating Euler physics on Y axis.
        /// </summary>
        private void CalcVerticalMovement() {

            float gravMultiplier = 1;

            bool isGrounded = false;
            if (transform.position.y < 0) {
                Vector3 pos = transform.position;
                pos.y = 0;
                transform.position = pos;
                velocity.y = 0;
                isGrounded = true;
            }

            bool wantsToJump = Input.GetButtonDown("Jump");
            bool isHoldingJump =  Input.GetButton("Jump");

            if(wantsToJump && isGrounded)
            {
                velocity.y = jumpImpulse;
                isJumpingUpwards = true;
            }
            if (!isHoldingJump || velocity.y < 0)
            {
                isJumpingUpwards = false;
            }

            velocity.y -= gravity * Time.deltaTime * gravMultiplier;


        }
    }
}
