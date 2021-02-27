using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Howley
{
    public class CameraAutoScroll : MonoBehaviour
    {
        /// <summary>
        /// How fast the camera will automatically move every second.
        /// </summary>
        public Vector3 scrollSpeed = new Vector3();

        public Transform target;

        PlayerMovement player;

        void Start()
        {
            player = GetComponent<PlayerMovement>();
        }

        void Update()
        {
            Vector3 position = transform.position; // Get the player's position
            position.x = target.position.x;
            position.y = target.position.y;
            


            if(position.y > 2.5)
            {
                AnimMath.Slide(transform.position.y, position.y, .001f);
                //transform.position += (position - transform.position) * Time.deltaTime * 10;
            }
            else
            {
                transform.position += scrollSpeed * Time.deltaTime;
            }
            
            
            
            
            




        }
    }
}

