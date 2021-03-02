using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Szczesniak {
    
    /// <summary>
    /// This Class if for all of the colliding and some of the physics that happen in our game world 
    /// </summary>
    public class AABB : MonoBehaviour {

        /// <summary>
        /// Size of our object that the user sets in the inspector
        /// </summary>
        public Vector3 boxSize;

        /// <summary>
        /// To be able to find the min and max of our AABB to check for collision
        /// </summary>
        public Vector3 min;
        public Vector3 max;

        /// <summary>
        /// Starts once
        /// </summary>
        void Start()
        {
            RecalcAABB();
        }


        /// <summary>
        /// This function should be called whenever the position or size of the collider changes.
        /// </summary>
        public void RecalcAABB() {
            //min.x = transform.position.x - boxSize.x / 2;
            //min.y = transform.position.y - boxSize.y / 2;
            //min.z = transform.position.z - boxSize.z / 2;

            min = transform.position - boxSize / 2;
            max = transform.position + boxSize / 2;
        }

        /// <summary>
        /// Checks to see if there are any collisions between anything that has AABB attached to it
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool OverlapCheck(AABB other) {

            if (other.min.x > this.max.x) return false; // gap to right - NO COLLISION
            if (other.max.x < this.min.x) return false; // gap to left - NO COLLISION

            if (other.min.y > this.max.y) return false; // gap to above - NO COLLISION
            if (other.max.y < this.min.y) return false; // gap to below - NO COLLISION

            if (other.min.z > this.max.z) return false; // gap "forward" - NO COLLISION
            if (other.max.z < this.min.z) return false; // gap "behind" - NO COLLISION

            return true;
        }

        /// <summary>
        /// This function does the colliding effect.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Vector3 FindFix(AABB other)
        {
            float moveRight = other.max.x - this.min.x; // positive number
            float moveLeft = other.min.x - this.max.x; // negative number
            float moveUp = other.max.y - this.min.y; // positive number
            float moveDown = other.min.y - this.max.y; // negative number

            Vector3 fix = Vector3.zero;

            if (Mathf.Abs(moveLeft) < Mathf.Abs(moveRight)) {
                fix.x = moveLeft;
            }
            else {
                fix.x = moveRight;
            }

            if (Mathf.Abs(moveUp) < Mathf.Abs(moveDown)) {
                fix.y = moveUp;
            }
            else {
                fix.y = moveDown;
            }

            if (Mathf.Abs(fix.x) < Mathf.Abs(fix.y)) {
                fix.y = 0; // going with horizontal solution; clearing the vertical...
            }
            else {
                fix.x = 0; // Going with vertical soulution, clearing the horizontal...
            }


            return fix;
        }

        /// <summary>
        /// This draws the outline of the AABB box so we are able to see it
        /// </summary>
        private void OnDrawGizmos() {
            // draws stuff in the scene view...

            Gizmos.DrawWireCube(transform.position, boxSize);

        }
    }
}