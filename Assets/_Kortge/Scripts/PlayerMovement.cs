using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Write documentation before every class and class member (variables).
namespace Kortge
{
    /// <summary>
    /// This class gets input and moves the player with input and euler physics. It also handles death and respawning.
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {

        /// <summary>
        /// The current velocity of player.
        /// </summary>
        private Vector3 velocity = new Vector3();

        public int bandages = 0; // Gives the player an extra life after five of these are collected.
        public int lives = 1; // How many times the player can respawn after dying.
        public Vector3 checkpoint; // The position that player would be sent to upon death with a life.

        public Transform cam;
        /// <summary>
        /// When the player wants to move, this value is used to scale the player's deceleration.
        /// </summary>

        [Header("Horizontal Movement")]

        /// <summary>
        /// This value is used to accelerate the player.
        /// </summary>

        public float scalarAcceleration;

        /// <summary>
        /// This value is used to decelerate the player.
        /// </summary>

        public float scalarDeceleration;

        /// <summary>
        /// This value is used to clamp the player's horizontal velocity.
        /// </summary>

        public float maxSpeed = 5;

        public ParticleSystem blood; // Blood from Meat Boy's body is kicked up every time he runs or jumps.

        [Header("Vertical Movement")]

        /// <summary>
        /// This is used to scale the player's downward acceleration due to gravity.
        /// </summary>
        public float gravity;
        /// <summary>
        /// The velocity we launch the player when they jump. Measured in meters/second.
        /// </summary>
        public float jumpImpulse = 10;

        public float terminalVelocity = 15; // The maximum speed the player can fall downwards.

        /// <summary>
        /// Whether or not the player is currently jumping upwards.
        /// </summary>
        private bool isJumpingUpwards = false;
        private bool isGrounded = false; // Checks when the player is on the ground so that the player can jump.
        /// <summary>
        /// Checks if the player is moving into a wall so that wall jumps can be performed.
        /// </summary>
        private bool leftWallHug = false;
        private bool rightWallHug = false;

        /// <summary>
        /// Do euler physics each tick.
        /// </summary>

        public AABB aabb; // The collision checking class.

        void Start() // Get AABB.
        {
            aabb = GetComponent<AABB>();
            blood = GetComponentInChildren<ParticleSystem>();
        }

        // Update is called once per frame
        void Update() // Updates the position of the player.
        {
            if (transform.position.x < cam.position.x-11.3f || transform.position.x > cam.position.x+11.3f || transform.position.y < cam.position.y - 6.6) KillPlayer(); // Kills the player if straying too far to the left, right, or under the camera.
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

        private void CalcVerticalMovement() // Handles how jumping works.
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

        private void CalcHorizontaMovement() // Handles how running works.
        {
            float h = Input.GetAxisRaw("Horizontal");

            if (h != 0)
            { // user is pressing left or right (or both?)
              // applying acceleration to our velocity:
                velocity.x += h * Time.deltaTime * scalarAcceleration;
                if (isGrounded || leftWallHug || rightWallHug) blood.Play();
                else blood.Stop();
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

                blood.Stop();
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

        public void LaunchPlayer(Vector3 vel) // Shoots the player into the air much higher than a jump would.
        {
            vel.z = 0;
            this.velocity = vel;
        }

        public void KillPlayer() // Decides whether to respawn the player or end the game on death.
        {
            if (lives > 0) 
            {
                transform.position = checkpoint + transform.up;
                lives--;
            }
            else Destroy(gameObject);
        }

        private void OnDestroy() // Restarts the scene when death occurs.
        {
            SlipstreamJumper.Game.GameOver();
        }

        public void AddBandage() // Adds a bandage or a life depending on how many bandages the player has.
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