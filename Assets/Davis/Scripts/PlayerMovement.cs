using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Davis { 
    /// <summary>
    /// this class gets input and moves the player
    /// with the input and with Euler physics.
    /// </summary>
    public class PlayerMovement : MonoBehaviour {

        /// <summary>
        /// when the player wants to move, this value
        /// is used to scale the player's acceleration.
        /// </summary>
        [Header("Horizontal Movement")] //making these headers organizes them in Unity Editor

        public float acceleration = 5; //making these public variables lets you edit them in Unity Editor
        /// <summary>
        /// this value is used to scale the player's deceleration
        /// </summary>
        public float deceleration = 40;

        /// <summary>
        /// this value is used to clamp the player's horizontal velocity, measured in meters per second.
        /// </summary>
        public float maxSpeed = 5;


        /// <summary>
        /// this value is used to scale the player's downward acceleration due to gravity.
        /// </summary>
        [Header("Vertical Movement")]

        public float gravity = 10;

        /// <summary>
        /// the velocity we launch the player when they jump, measured in meters per second.
        /// </summary>
        public float jumpImpulse = 15;



        /// <summary>
        /// the current velocity of the player, in meters per second.
        /// </summary>
        private Vector3 velocity = new Vector3();
        /// <summary>
        /// whether or not the player is currently jumping upwards.
        /// </summary>
        private bool isJumpingUpwards = false;
        private bool isGrounded = false;
        private AABB aabb;



        // Start is called before the first frame update
        private void Start() {

            aabb = GetComponent<AABB>();
        
        }

        // Update is calling euler physics once per frame
        void Update()
        {

            if (Time.deltaTime > 0.25f) return; // lag spike? quit early do nothing
            
            
            

            CalcHorizontalMovement();
            CalcVerticalMovement();


            //applying our velocity to our position:
            transform.position += velocity * Time.deltaTime;


            isGrounded = false;
            aabb.RecalcAABB();
        }

        /// <summary>
        /// this calculates the vertical movement (up and down) and ground detection.
        /// </summary>
        private void CalcVerticalMovement()
        {

            float gravMultiplier = 1;

            //detect if on ground;
            bool isGrounded = false;
            /*
              if (transform.position.y < 0) { //if on ground
                Vector3 pos = transform.position;
                pos.y = 0;
                transform.position = pos;
                velocity.y = 0;
                isGrounded = true;
            } 
            */

            bool wantsToJump = Input.GetButtonDown("Jump"); //true when pressing button
            bool isHoldingJump = Input.GetButton("Jump"); //true when holding down button

            if(wantsToJump && isGrounded)
            {
                velocity.y = jumpImpulse;
                isJumpingUpwards = true;
            }
            if(!isHoldingJump || velocity.y < 0) 
            {
                isJumpingUpwards = false;
            }

            if (isJumpingUpwards) gravMultiplier = 0.5f;

            // apply force of gravity to our velocity;
            velocity.y -= gravity * Time.deltaTime * gravMultiplier;

            
        }

        /// <summary>
        /// this calculates the horizontal movement left and right
        private void CalcHorizontalMovement()
        {
            float h = Input.GetAxisRaw("Horizontal");

            // ======= Euler physics integration ========


            if (h != 0)
            {
                //applying acceleration to our velocity:
                velocity.x += h * Time.deltaTime * acceleration;

                //transform.position += Vector3.right * h * Time.deltaTime;
            }
            else
            { //user is NOT pushing left or right:

                if (velocity.x > 0)
                { //player is moving right...
                    velocity.x -= deceleration * Time.deltaTime;
                    if (velocity.x < 0) velocity.x = 0;
                }
                if (velocity.x < 0)
                { //player is moving left...
                    velocity.x += deceleration * Time.deltaTime;
                    if (velocity.x > 0) velocity.x = 0;
                }

            }

            //clamp;
            if (velocity.x < -maxSpeed) velocity.x = -maxSpeed;
            if (velocity.x > maxSpeed) velocity.x = maxSpeed;


            //unity clamp:
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