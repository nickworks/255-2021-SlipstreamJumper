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

        /// <summary>
        /// Hold reference to the camera.
        /// </summary>
        private Camera cam;

        /// <summary>
        /// Hold reference to a transform.
        /// </summary>
        public Transform target;

        /// <summary>
        /// Hold reference to the playermovement class.
        /// </summary>
        public PlayerMovement player;

        /// <summary>
        /// The start function is called once before the first update.
        /// </summary>
        void Start()
        {
            player = GetComponent<PlayerMovement>();
            cam = GetComponentInChildren<Camera>();
        }

        /// <summary>
        /// The update function is called every game tick.
        /// </summary>
        void Update()
        {
            if (target)
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
}

