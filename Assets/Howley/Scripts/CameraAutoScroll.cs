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

        private Camera cam;

        public Transform target;

        public PlayerMovement player;

        void Start()
        {
            player = GetComponent<PlayerMovement>();
            cam = GetComponentInChildren<Camera>();
        }

        void Update()
        {
            float posY = transform.position.y;
            Vector2 pos = new Vector2(transform.position.x, transform.position.y);
            Vector2 targetPos = new Vector2(target.position.x, target.position.y);

            if (target.transform.position.y >= 3.5)
            {
                transform.position = AnimMath.Slide(transform.position, target.position, .008f);
            }
            else
            {
                transform.position += scrollSpeed * Time.deltaTime;                
            }
            
        }
    }
}

