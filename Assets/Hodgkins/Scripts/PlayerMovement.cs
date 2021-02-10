using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hodgkins
{
    public class PlayerMovement : MonoBehaviour
    {
        public float acceleration = 5;
        public float deceleration = 40;
        private Vector3 velocity = new Vector3();
        
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            float h = Input.GetAxisRaw("Horizontal");

            //Euler physics integration

            if (h != 0) //user is pressing left or right (or both)
            {
                //applying acceleration to velocity
                velocity.x += h * Time.deltaTime * acceleration;
            } else { // user is not pushing left or right

                if(velocity.x > 0) //player is moving right
                {
                    velocity.x -= deceleration * Time.deltaTime;
                    if (velocity.x < 0) velocity.x = 0;
                }
                if(velocity.x < 0)
                {
                    velocity.x += deceleration * Time.deltaTime;
                    if (velocity.x > 0) velocity.x = 0;
                }

            }

            //applying velocity to our position
            transform.position += velocity * Time.deltaTime;

        }
    }
}
