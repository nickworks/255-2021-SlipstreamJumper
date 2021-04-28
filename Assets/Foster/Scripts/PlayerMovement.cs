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
            public float maxSpeed = 250;
            public float dash = 5;
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

            private bool isGrounded = false;
            /// <summary>
            /// The current velocity for the player in meters per seconds
            /// </summary>
            private Vector3 velocity = new Vector3();

            float coolDownDash = 0;

            private AABB aabb;

            private Animator anim;
            private AudioSource soundPlayer;


            private void Start()
            {

                aabb = GetComponent<AABB>();
                anim = GetComponent<Animator>();

            soundPlayer = GetComponentInChildren<AudioSource>();
            }

            /// <summary>
            /// does euler physics each tick
            /// </summary>
            void Update()
            {

            if (Time.deltaTime > .25f) return; //lag spike quit early
          

                coolDownDash -= Time.deltaTime;
            if (coolDownDash <= 0) coolDownDash = 0;
                MovementHorizontal();

                VerticalMovement();

                transform.position += velocity * Time.deltaTime;

                aabb.RecalAABB();
                isGrounded = false;
            }
            /// <summary>
            /// Calcutaing Euler physics on y axis
            /// </summary>
            private void VerticalMovement()
            {
                float gravMultiplier = 1;

                //detech ground
                bool wantsToJump = Input.GetButtonDown("Jump");
                bool isHoldingJump = Input.GetButton("Jump");

                if (wantsToJump && isGrounded)
                {
                    velocity.y = jumpImpulse;
                    isJumpingUpWards = true;
                    isGrounded = false;



                SoundEffectBoard.PlayJump(transform.position);
                }

                if (!isHoldingJump || velocity.y < 0)
                {
                    isJumpingUpWards = false;
                }
                if (isJumpingUpWards) gravMultiplier = .5f;

                //applying force of gravity to velocity
                velocity.y -= gravity * Time.deltaTime * gravMultiplier;


                //clamp vertical speed
                //if (velocity.y < -1) velocity.y = -1;



            }

            /// <summary>
            /// Calculating euler physcics on the x axis
            /// </summary>
            private void MovementHorizontal()
            {
                float h = Input.GetAxisRaw("Horizontal");
                bool dashIsOn = Input.GetButtonDown("Fire1");

                //Euler Physics intergation
                if (h != 0)
                {
                float accel = speed;

                if (!isGrounded)
                {
                    accel = speed / 2;
                }

                float regSpeed = h * Time.deltaTime * accel;

                if (coolDownDash <= 0 && dashIsOn)
                {
                    coolDownDash = .5f;
                }
                if (coolDownDash > 0)
                {

                    float dasher = speed * dash;

                    if (!isGrounded)
                    {
                        accel = dasher / 2;
                    }

                    float dashSpeed = h * Time.deltaTime * dasher;
                    velocity.x += regSpeed + dashSpeed * 2;
                }
                else
                {

                    velocity.x += regSpeed;
                   // print("regular speed");
                }

                //print(regSpeed);
                //print(velocity.x);
                //print(coolDownDash);
                
                }
                else
                {

                float decel = despeed;

                if (!isGrounded)
                {
                    decel = despeed / 2;
                }

                    if (velocity.x > 0)//if player moves right
                    {
                        velocity.x -= decel * Time.deltaTime;
                        if (velocity.x < 0) velocity.x = 0;
                    }
                    if (velocity.x < 0)//if player moves left
                    {
                        velocity.x += decel * Time.deltaTime;
                        if (velocity.x > 0) velocity.x = 0;
                    }
                }
                if (coolDownDash <= 0 && dashIsOn) velocity.x = Mathf.Clamp(velocity.x, -50, 50);
                else velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
            }


            public void ApplyFix(Vector3 fix)
            {
                transform.position += fix;

                if (fix.y > 0) isGrounded = true;

                if (fix.y != 0)
                {
                    velocity.y = 0;
                }

             //   if (fix.x != 0)
              //  {
              //      velocity.x = 0;
              //  }

                aabb.RecalAABB();
            }

            public void LaunchPlayer(Vector3 vel)
            {

            vel.z = 0;

            velocity = vel;

            }

        public void OnTriggerEnter(Collider other)
        {
            if(this.tag == ("Player") & other.tag == ("FallGuard"))
            {
                SlipstreamJumper.Game.GameOver();
            }
        }

    }
}
