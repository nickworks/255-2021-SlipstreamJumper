using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pattison {
    public class AABB : MonoBehaviour {

        public Vector3 boxSize;

        public Vector3 min;
        public Vector3 max;


        void Start() {

        }


        void Update() {

            RecalcAABB();

        }
        /// <summary>
        /// This function should be called whenever the position
        /// or size of the collider changes.
        /// </summary>
        public void RecalcAABB() {

            //min.x = transform.position.x - boxSize.x / 2;
            //min.y = transform.position.y - boxSize.y / 2;
            //min.z = transform.position.z - boxSize.z / 2;

            min = transform.position - boxSize / 2;
            max = transform.position + boxSize / 2;
        }

        public bool OverlapCheck(AABB other) {

            if (other.min.x > this.max.x) return false; // gap to right - NO COLLISION
            if (other.max.x < this.min.x) return false; // gap to left - NO COLLISION

            if (other.min.y > this.max.y) return false; // gap to above - NO COLLISION
            if (other.max.y < this.min.y) return false; // gap to below - NO COLLISION

            if (other.min.z > this.max.z) return false; // gap "forward" - NO COLLISION
            if (other.max.z < this.min.z) return false; // gap "behind" - NO COLLISION

            return true;
        }

        private void OnDrawGizmos() {
            // draws stuff in the scene view...

            Gizmos.DrawWireCube(transform.position, boxSize);
        }
    }
}