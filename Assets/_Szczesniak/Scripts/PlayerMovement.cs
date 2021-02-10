using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Szczesniak {
    public class PlayerMovement : MonoBehaviour
    {
        private Vector3 velocity = new Vector3();
        public float deceleration = 40;
        public float acceleration = 5;

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

            float h = Input.GetAxis("Horizontal");

            //transform.position += Vector3.right * h * Time.deltaTime;
            
            // ======== Euler physics intergration ========

            if (h != 0) { // user is pressing left or right (or both)
                // applying acceleration to our velocity
                velocity.x += h * Time.deltaTime * acceleration;
            }
            else { // user is NOT pushing left or right

                if(velocity.x > 0) { // player is moving right
                    velocity.x -= deceleration * Time.deltaTime;
                    if (velocity.x < 0) velocity.x = 0;
                }
                if(velocity.x < 0) { // player is mocing left
                    velocity.x += deceleration * Time.deltaTime;
                    if (velocity.x > 0) velocity.x = 0;
                }
            }

            // applying our velocity to our position:
            transform.position += velocity * Time.deltaTime;
        }
    }
}
