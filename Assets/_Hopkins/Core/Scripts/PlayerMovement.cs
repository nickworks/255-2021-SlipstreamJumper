using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hopkins
{
    
    public class PlayerMovement : MonoBehaviour
    {
        public float acceleration = 20;
        public Vector3 velocity;
        public float deceleration = 40;
        void Start()
        {

        }

        void Update()
        {
            float h = Input.GetAxis("Horizontal");

            if (h != 0) //user pressing left or right
            {
                //applying acceleration to velocity
                velocity.x += h * Time.deltaTime * acceleration;
            }
            else //user NOT pressing left or right
            {
                if (velocity.x > 0) 
                {
                    velocity.x -= deceleration * Time.deltaTime;
                    if (velocity.x < 0)
                    {
                        velocity.x = 0;
                    }
                }
                if (velocity.x < 0)
                {
                    velocity.x += deceleration * Time.deltaTime;
                    if (velocity.x > 0)
                    {
                        velocity.x = 0;
                    }
                }
            }
            //Euler physics stuff
            //applying velocity to position
            transform.position += velocity * Time.deltaTime;
        }
    }
}
