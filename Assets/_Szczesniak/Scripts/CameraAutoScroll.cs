using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Szczesniak {
    /// <summary>
    /// This class is used to scroll the camera like a mario game (NOT USED)
    /// </summary>
    public class CameraAutoScroll : MonoBehaviour {

        /// <summary>
        /// How much to add to position every second. (m/s)
        /// </summary>

        public Vector3 scrollSpeed = new Vector3();

        void Update() {
            // Moves the camera to the right at a specific speed
            transform.position += scrollSpeed * Time.deltaTime;
        }
    }
}