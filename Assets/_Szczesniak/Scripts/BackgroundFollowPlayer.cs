using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Szczesniak {
    /// <summary>
    /// The background follows the camera to keep the player from seeing out of bounds
    /// </summary>
    public class BackgroundFollowPlayer : MonoBehaviour {

        /// <summary>
        /// Gets camera transform
        /// </summary>
        public Transform Cam;

        void Update() {
            Vector3 backgroundPos = new Vector3(Cam.position.x, transform.position.y, transform.position.z); // gets information to move background to cam
            transform.position = backgroundPos; // moves background to cam
            
        }
    }
}