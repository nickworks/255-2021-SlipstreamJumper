using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Szczesniak {
    /// <summary>
    /// This Class purpose is to make the camera follow the player
    /// </summary>
    public class CameraFollow : MonoBehaviour {
        /// <summary>
        /// The thing that the camera is following.
        /// </summary>
        public Transform target;

        /// <summary>
        /// The position the Camera was at before
        /// </summary>
        private Vector3 previousPosition;

        void Start() {
            previousPosition.x = transform.position.x - 1; // setting up the camera
        }

        /// <summary>
        /// Updates last compared to other update functions
        /// </summary>
        void LateUpdate() {

            //transform.position = pos;
            Vector3 pos = transform.position;
            

            // asymptotic slide:
            // exponential slide:
            if (!target) return; // if no player, then no error from camera follow

            pos.y = target.position.y;

            // if the camera is not in line with the player, it will follow until it is false
            if ((target.position - previousPosition).x >= 0) {
                //transform.position = AnimMath.Slide(transform.position, target.position, 0.0005f);//(pos - transform.position) * Time.deltaTime * 5;
                previousPosition = transform.position;
                pos.x = target.position.x;
            }

            transform.position += (pos - transform.position) * Time.deltaTime * 10; // Camera follows the player

        }
    }
}