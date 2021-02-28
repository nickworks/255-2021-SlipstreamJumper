using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Write documentation before every class and class member (variables).
namespace Kortge
{
    /// <summary>
    /// This class gets input and moves the player with input and euler physics.
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        private int bandages = 0;
        private int lives = 0;

        public Transform cam;
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

        public float terminalVelocity = 15;

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

        public AABB aabb;
        private bool isGrounded = false;
        private bool leftWallHug = false;
        private bool rightWallHug = false;

        void Start()
        {
            aabb = GetComponent<AABB>();
        }

        // Update is called once per frame
        void Update()
        {
            if (transform.position.x < cam.position.x-11.3f || transform.position.x > cam.position.x+11.3f || transform.position.y < cam.position.y - 6.6) KillPlayer();
            if (Time.deltaTime > 0.25f) return; // quit early, do nothing

            CalcHorizontaMovement();
            CalcVerticalMovement();

            // applying velocity to our position:
            transform.position += velocity * Time.deltaTime;

            isGrounded = false;
            leftWallHug = false;
            rightWallHug = false;

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
                velocity.y = 7.5f;
                isJumpingUpwards = true;
            }

            else if (wantsToJump && leftWallHug)
            {
                velocity.y = 7.5f;
                velocity.x = 2.5f;
            }

            else if (wantsToJump && rightWallHug)
            {
                velocity.y = 7.5f;
                velocity.x = -2.5f;
            }

            if (!isHoldingJump || velocity.y < 0)
            {
                isJumpingUpwards = false;
            }

            if (isJumpingUpwards) gravMultiplier = 0.5f;

            // apply gravity to velocity
            velocity.y -= gravity * gravMultiplier * Time.deltaTime;

            // clamp vertivcal speed to create terminal velocity;
            if (velocity.y < -terminalVelocity) velocity.y = -terminalVelocity;

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
                float decel = scalarDeceleration;
                if (!isGrounded)
                {
                    decel = scalarDeceleration = 2;
                }

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

        /// <summary>
        /// Moves the player by adding a vector to its position.
        /// The vector represents a "fix" that should move the player out of another object.
        /// From the fix, we can deduce which direction the player was moved in.
        /// </summary>
        /// <param name="fix"></param>
        public void ApplyFix(Vector3 fix)
        {
            transform.position += fix;

            if (fix.y > 0) isGrounded = true;

            if (fix.x < 0)
            {
                rightWallHug = true;
                print("right" + fix.x);
            }

            if (fix.x > 0)
            {
                leftWallHug = true;
                print("left" + fix.x);
            }

            if (fix.y != 0) // Move player up or down.
            {
                velocity.y = 0; // If move player up
            }

            if (fix.x != 0)
            {
                velocity.x = 0;
            }

            aabb.RecalcAABB();
        }

        public void LaunchPlayer(Vector3 vel)
        {
            vel.z = 0;
            this.velocity = vel;
        }

        public void KillPlayer()
        {
            if (lives > 0) 
            {
                transform.position = cam.position + (transform.up * 4);
                lives--;
            }
            else Destroy(gameObject);
        }

        private void OnDestroy() // Restarts the scene when death occurs.
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        public void AddBandage()
        {
            bandages++;
            if (bandages >= 5)
            {
                bandages -= 5;
                lives++;
            }
            print("Lives: " + lives + " Bandages: " + bandages);
        }
    }
}