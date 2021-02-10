using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Foster
{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed = 5;
        public float despeed = 40;
        private Vector3 velocity = new Vector3();

        void Start()
        {

        }

    
        void Update()
        {
            float h = Input.GetAxisRaw("Horizontal");


            //Euler Physics intergation
            if(h != 0)
            {
               
            velocity.x += h * Time.deltaTime * speed;
            }
            else
            {

                if(velocity.x > 0)//if player moves right
                {
                    velocity.x -= despeed * Time.deltaTime;
                    if (velocity.x < 0) velocity.x = 0;
                }
                if(velocity.x < 0)//if player moves left
                {
                    velocity.x += despeed * Time.deltaTime;
                    if (velocity.x > 0) velocity.x = 0;
                }


            }



            transform.position += velocity * Time.deltaTime;

        }
    }
}
