using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelsomeno
{

    public class Parallax : MonoBehaviour
    {
        /// <summary>
        /// reference to the length of parallax and when to start it
        /// </summary>
        private float length, startpos;
        /// <summary>
        /// reference to camera
        /// </summary>
        public GameObject cam;
        /// <summary>
        /// how much effect we want the parallax to do
        /// </summary>
        public float parallaxEffect;

        void Start()
        {
            startpos = transform.position.x;
            length = GetComponent<SpriteRenderer>().bounds.size.x; // gives us the length of the sprite
        }

        void FixedUpdate()
        {
            float dist = (cam.transform.position.x * parallaxEffect); // how much we have moved in the world space

            transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
        }
    }
}
