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

            //transform.position = pos;

            // asymptotic slide:
            // exponential slide:
            transform.position = AnimMath.Slide(transform.position, target.position, 0.0005f);//(pos - transform.position) * Time.deltaTime * 5;
        }
    }
}