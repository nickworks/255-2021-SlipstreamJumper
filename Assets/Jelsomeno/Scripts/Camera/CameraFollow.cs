using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jelsomeno
{
    /// <summary>
    /// this class sets the up for the camera to follow the player
    /// </summary>
    public class CameraFollow : MonoBehaviour
    {
        /// <summary>
        /// What the camera is following, the player
        /// </summary>
        public Transform target;

        void Start()
        {

        }

        // Update is called once per frame
        void LateUpdate()
        {
            
            Vector3 pos = transform.position;

            pos.x = target.position.x + 5;// follows the player on the x-axis 
            pos.y = target.position.y;// follows the player on the y-axis

            //transform.position = pos;

            //easing
            transform.position += (pos - transform.position) * Time.deltaTime * 10;

        }
    }
}
