using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geib // This namespace needs to be in all of my scripts!
{

    public class PlayerMovement : MonoBehaviour
    {
        public float acceleration = 20;
        public float deceleration = 40;
        private Vector3 velocity = new Vector3();

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            float h = Input.GetAxisRaw("Horizontal");

            // --== Euler physic integration ==--

            // Alternate Solution:
            //transform.position += Vector3.right * h * Time.deltaTime;

            if (h != 0) // user is pushing left or right (or both?
            {
                // applying acceleration to our velocity:
                velocity.x += h * Time.deltaTime * acceleration;
            } else // user is NOT pushing left or right
            {
                if(velocity.x > 0) // Player is moving right
                {
                    velocity.x -= deceleration * Time.deltaTime;
                    if (velocity.x < 0) velocity.x = 0; // Don't cange directions.
                }
                if (velocity.x < 0) // Player is moving left
                {
                    velocity.x += deceleration * Time.deltaTime;
                    if (velocity.x > 0) velocity.x = 0; // Don't change directions.
                }


            }

            

            // Applying our velocity to our position:
            transform.position += velocity * Time.deltaTime;



        }
    }
}
