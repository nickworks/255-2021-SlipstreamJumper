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
        /// This is so you scale the speed of the Dash speed
        /// </summary>
        public float DashForce;

        /// <summary>
        /// 
        /// </summary>
        public float StartDashTime;

        /// <summary>
        /// 
        /// </summary>
        float CurrentDashTime;


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

        public float terminalVelocity = 10;
        /// <summary>
        ///  The maximum fall speed for the player. (helps with discrete collision detection)
        /// </summary>


        private Vector3 velocity = new Vector3();

        /// <summary>
        /// Whether or not the player is currently jumping upwards.
        /// </summary>

        bool isDashing;

        private bool isJumpingUpwards = false;
        private bool isGrounded = false;
        private AABB aabb;

        // reference to the sprite animation 
        private Animator anim;

        private AudioSource soundPlayer;

        private void Start()
        {
            aabb = GetComponent<AABB>();
            //anim = GetComponent<Animator>();
            soundPlayer = GetComponentInChildren<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {

            if (Time.deltaTime > 0.25f) return; // lag spike/ quit early do nothing

            //communicates with anim controller
            //anim.SetBool("isGrounded", isGrounded);

            CalcHorizontalMovement();

            Dash();

            CalcVerticalMovement();

            //applying velocity to position 
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


            bool wantsToJump = Input.GetButtonDown("Jump");

            bool isHoldingJump = Input.GetButton("Jump");


            if (wantsToJump && isGrounded)
            {
                velocity.y = jumpImpulse;
                isJumpingUpwards = true;
                isGrounded = false;
                soundPlayer.Play();
                //SoundEffectsBoard.PlayJump();




            }
            if (!isHoldingJump || velocity.y < 0)
            {
                isJumpingUpwards = false;
            }

            if (isJumpingUpwards) gravMultiplier = 0.5f;


            // apply force of gravity to our velocity 
            velocity.y -= gravity * Time.deltaTime * gravMultiplier;

            // clamp vertical speed to create terminal velocity:
            if (velocity.y < -terminalVelocity) velocity.y = -terminalVelocity;

        }

        /// <summary>
        /// Calculating Euler physics on X axis
        /// </summary>

        private void CalcHorizontalMovement()
        {
            float h = Input.GetAxisRaw("Horizontal");

            //h = 1;// player wants to move right

            // Euler physics integration:

            if (h != 0)
            {

                float accel = scalerAcceleration;

                if (!isGrounded)
                {
                    accel = scalerAcceleration / 10;
                }

                // applying acceleration to velocity
                velocity.x += h * Time.deltaTime * accel;

            }
            else
            {

                float decel = scalerDeceleration;

                if (!isGrounded)
                {
                    decel = scalerDeceleration / 10;
                }

                if (velocity.x > 0) // player is moving right
                {
                    velocity.x -= decel * Time.deltaTime;
                    if (velocity.x < 0) velocity.x = 0;

                }
                if (velocity.x < 0)
                {
                    velocity.x += decel * Time.deltaTime;
                    if (velocity.x > 0) velocity.x = 0;

                }

            }

            //if (velocity.x < -maxSpeed) velocity.x = -maxSpeed;
            //if (velocity.x >  maxSpeed) velocity.x = maxSpeed;

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

        public void LaunchPlayer(Vector3 vel)
        {
            vel.z = 0;
            velocity = vel;
        }


        /// <summary>
        /// when the player presses the left shift bar they can dash forward
        /// </summary>
        private void Dash()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isDashing = true;
                CurrentDashTime = StartDashTime;
                velocity = Vector3.zero;

            }

            if (isDashing)
            {
                velocity = transform.right * DashForce;

                CurrentDashTime -= Time.deltaTime;

                if(CurrentDashTime <= 0)
                {
                    isDashing = false;
                }
            }
        }

    }
}
