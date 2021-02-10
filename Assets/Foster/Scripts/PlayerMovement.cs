using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Foster
{
   /// <summary>
   /// This class gets imput and moves the player with the input and euler physics
   /// </summary>
    public class PlayerMovement : MonoBehaviour
    {

        /// <summary>
        /// when the player wants to move , this value
        /// is used to accelerate the player
        /// </summary>
        [Header("Horizontal movement")]
        public float speed = 5;
        public float despeed = 40;
        public float maxSpeed = 5;
        /// <summary>
        /// this values help scale the players jump
        /// </summary>
        [Header("Vertical Movement")]
        public float jumpImpulse = 10;
        public float gravity = 10;

        /// <summary>
        /// whether or not he player is moving upwards
        /// </summary>
        private bool isJumpingUpWards = false;
        /// <summary>
        /// The current velocity for the player in meters per seconds
        /// </summary>
        private Vector3 velocity = new Vector3();

        
    /// <summary>
    /// does euler physics each tick
    /// </summary>
        void Update()
        {
            MovementHorizontal();

            VerticalMovement();

            transform.position += velocity * Time.deltaTime;

        }
        /// <summary>
        /// Calcutaing Euler physics on y axis
        /// </summary>
        private void VerticalMovement()
        {
            bool isGrounded = false;

            float gravMultiplier = 1;

            //detech ground
            if(transform.position.y <0) //on the ground
            {

                Vector3 pos = transform.position;
                pos.y = 0;
                transform.position = pos;
                velocity.y = 0;
                isGrounded = true;
            }

            bool wantsToJump = Input.GetButtonDown("Jump");
            bool isHoldingJump = Input.GetButton("Jump");

            if(wantsToJump && isGrounded)
            {
                velocity.y = jumpImpulse;
                isJumpingUpWards = true;
            }

            if(!isHoldingJump || velocity.y < 0)
            {
                isJumpingUpWards = false;
            }
            if (isJumpingUpWards) gravMultiplier = .5f;

            //applying force of gravity to velocity
            velocity.y -= gravity * Time.deltaTime * gravMultiplier;

        }
        /// <summary>
        /// Calculating euler physcics on the x axis
        /// </summary>
        private void MovementHorizontal()
        {
            float h = Input.GetAxisRaw("Horizontal");


            //Euler Physics intergation
            if (h != 0)
            {

                velocity.x += h * Time.deltaTime * speed;
            }
            else
            {

                if (velocity.x > 0)//if player moves right
                {
                    velocity.x -= despeed * Time.deltaTime;
                    if (velocity.x < 0) velocity.x = 0;
                }
                if (velocity.x < 0)//if player moves left
                {
                    velocity.x += despeed * Time.deltaTime;
                    if (velocity.x > 0) velocity.x = 0;
                }
            }

            velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        }
    }
}
