using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Szczesniak {
    public class CameraFollow : MonoBehaviour {
        /// <summary>
        /// The thing that the camera is following.
        /// </summary>
        public Transform target;

        private Vector3 previousPosition;

        void Start() {
            previousPosition.x = transform.position.x - 1;
        }


        void LateUpdate() {

            //transform.position = pos;
            Vector3 pos = transform.position;
            

            // asymptotic slide:
            // exponential slide:
            if (!target) return; // if no player, then no error from camera follow

            pos.y = target.position.y;

            if ((target.position - previousPosition).x >= 0) {
                //transform.position = AnimMath.Slide(transform.position, target.position, 0.0005f);//(pos - transform.position) * Time.deltaTime * 5;
                previousPosition = transform.position;
                pos.x = target.position.x;
            }

            transform.position += (pos - transform.position) * Time.deltaTime * 10;

        }
    }
}