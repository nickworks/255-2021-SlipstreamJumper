using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Velting
{
    public class PlayerMovement : MonoBehaviour
    {

        private Animator anim;

        private AudioSource soundPlayer;
        /// <summary>
        /// When the player wants to move, this value
        /// is used to scale the players acceleration
        /// </summary>
        [Header("Horizontal Movement")]

        public float acceleration = 20;

        /// <summary>
        /// This value is used to scale the players
        /// deceleration
        /// </summary>
        public float deceleration = 40;

        /// <summary>
        /// This value is used to clamp the players
        /// horizontal velocity. Measured in m/s
        /// </summary>
        public float maxSpeed = 5;
        private Vector3 velocity = new Vector3();

        /// <summary>
        /// This is used to scale the player's downward acceleration
        /// due to gravity
        /// </summary>
        [Header ("Vertical Movement")]

               
        public float gravity = 10;
        private bool isGrounded = false;
        private bool isJumpingUpwards = false;

        /// <summary>
        /// This variable clamps the maximum downward velocity of the player
        /// </summary>
        public float terminalVelocity = 3;

        private AABB aabb;
        /// <summary>
        /// The velocity we launch the player when they jump.
        /// Measured in m/s
        /// </summary>
        public float jumpImpulse = 10;
        
        void Start()
        {
            aabb = GetComponent<AABB>();
            anim = GetComponentInChildren<Animator>();
            soundPlayer = GetComponentInChildren<AudioSource>();
        }

       
        void Update()
        {
            //detect if on ground:

            anim.SetBool("isGrounded", isGrounded);
            CalcVerticalMovement();
            CalcMovementHorizontal();

            //applying our velocity to our position
            transform.position += velocity * Time.deltaTime;
            aabb.RecalcAABB();

            isGrounded = false;
        }

        private void CalcMovementHorizontal()
        {
            float h = Input.GetAxis("Horizontal");

           // h = 1; //player wants to move right

            // Euler physics integration:

            if (h != 0) //user is pressing left or right (or both)
            {

                float airAccel = acceleration;

               
                //applying acceleration to our velocity:
                velocity.x += h * Time.deltaTime * airAccel;
            }

            else //user is not pushing left or right
            {

                float airDecel = deceleration;

                if (!isGrounded)
                {
                    airDecel /= 15;
                }

                if (velocity.x > 0) // player is moving right
                {
                    velocity.x += -airDecel * Time.deltaTime;
                    if (velocity.x < 0) velocity.x = 0;
                }

                if (velocity.x < 0) //player is moving left 
                {
                    velocity.x += airDecel * Time.deltaTime;
                    if (velocity.x > 0) velocity.x = 0;
                }
            }

            //clamp:
            //if (velocity.x < -maxSpeed) velocity.x = -maxSpeed;

            //unity clamp:
            velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        }

        private void CalcVerticalMovement()
        {
            float gravMultiplier = 1;
            float doubleJumpCooldown = .5f;
            doubleJumpCooldown -= Time.deltaTime;

            bool canJumpAgain = false;

            if (doubleJumpCooldown == 0) canJumpAgain = true;


            //Jump Mechanic

            bool wantsToJump = Input.GetButtonDown("Jump");
            bool isHoldingJump = Input.GetButton("Jump");

            if (wantsToJump && isGrounded)
            {
                velocity.y = jumpImpulse;
                isJumpingUpwards = true;

                AudioSource.PlayClipAtPoint(SoundEffectBoard.main.soundJump, transform.position);

                
            }

            if (wantsToJump && isGrounded && canJumpAgain)
            {
                velocity.y = jumpImpulse;
                isJumpingUpwards = true;
                doubleJumpCooldown += .5f;

                AudioSource.PlayClipAtPoint(SoundEffectBoard.main.soundJump, transform.position);


            }

            if (!isHoldingJump || velocity.y < 0)
            {
                isJumpingUpwards = false;
            }

            if (isJumpingUpwards) gravMultiplier = .5f;

            //This is the force of gravity:
            velocity.y -= gravity * Time.deltaTime * gravMultiplier;

            // clamp vertical speed to creat terminal velocity:
            if (velocity.y < -terminalVelocity) velocity.y = -terminalVelocity;

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

        public void ApplyOneWay(Vector3 passUp)
        {
            transform.position += passUp;
            if (passUp.y > 0) isGrounded = true;
            if (passUp.y != 0)
            {
                velocity.y = 0;
            }

            if (passUp.x != 0)
            {
                velocity.x = 0;
            }

            aabb.RecalcAABB();
        }

        public void LaunchPlayer(Vector3 velocity)
        {
            velocity.z = 0;
            this.velocity = velocity;
        }
    }

}
