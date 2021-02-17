using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Szczesniak {
    public class CameraFollow : MonoBehaviour {
        /// <summary>
        /// The thing that the camera is following.
        /// </summary>
        public Transform target;

        void Start() {

        }


        void LateUpdate() {

            Vector3 pos = transform.position;

            pos.x = target.position.x + 5;
            pos.y = target.position.y + 1;

            //transform.position = pos;

            // asymptotic slide:
            // exponential slide:
            transform.position += (pos - transform.position) * Time.deltaTime * 5;
        }
    }
}