using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Davis { 

    public class PlayerMovement : MonoBehaviour {

        public float acceleration = 5;
        public float deceleration = 40;
        private Vector3 velocity = new Vector3();

        // Start is called before the first frame update
        void Start() {
            

        
        }

        // Update is called once per frame
        void Update() {

            float h = Input.GetAxisRaw("Horizontal");

            // ======= Euler physics integration ========


            if (h != 0)
            {
                //applying acceleration to our velocity:
                velocity.x += h * Time.deltaTime * acceleration;

                //transform.position += Vector3.right * h * Time.deltaTime;
            } else { //user is NOT pushing left or right:
            
                if(velocity.x > 0) { //player is moving right...
                    velocity.x -= deceleration * Time.deltaTime;
                    if (velocity.x < 0) velocity.x = 0;
                } 
                if (velocity.x < 0) { //player is moving left...
                    velocity.x += deceleration * Time.deltaTime;
                    if (velocity.x > 0) velocity.x = 0;
                }
            
            }

            
            //applying our velocity to our position:
            transform.position += velocity * Time.deltaTime;
        
        }
    }
}