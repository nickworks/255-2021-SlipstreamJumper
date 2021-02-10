using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelsomeno {

    public class PlayerMovement : MonoBehaviour
    {
        public float acceleration = 5;
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

            // Euler physics integration:

            if (h != 0)
            {
                // applying acceleration to velocity
                velocity.x += h * Time.deltaTime * acceleration;

            } else {

                if(velocity.x > 0) // player is moving right
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

            //applying velocity to position 
            transform.position += velocity * Time.deltaTime;

        }
    }
}
