using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the parameters how other AABB objects can collide with this object.
/// </summary>
namespace Kortge
{
    public class AABB : MonoBehaviour
    {
        /// <summary>
        /// The space the collision box is covering.
        /// </summary>
        public Vector3 boxSize;

        /// <summary>
        /// The least amount the player should be moved upon overlapping with an object.
        /// </summary>
        public Vector3 min;
        /// <summary>
        /// The most amount the player should be moved upon overlapping with an object.
        /// </summary>
        public Vector3 max;

        /// <summary>
        /// Decides if a platform can be passed through from the bottom.
        /// </summary>
        public bool oneWay;

        // Start is called before the first frame update
        void Start()
        {
            RecalcAABB();
        }

        public void RecalcAABB() // Moves an object when colliding with another object.
        {
            min = transform.position - boxSize / 2;
            max = transform.position + boxSize / 2;
        }

        public bool OverlapCheck(AABB other) // Checks for overlap with other objects.
        {
            if (other.min.x > this.max.x) return false; // Gap to right.
            if (other.max.x < this.min.x) return false; // Gat to left.

            if (other.min.y > this.max.y) return false; // Gap up.
            if (other.max.y < this.min.y) return false; // Gap down.

            if (other.min.z > this.max.z) return false; // Gap forward.
            if (other.max.z < this.min.z) return false; // Gap backward.

            return true;
        }

        public Vector3 FindFix(AABB other) // Finds a new position for an object when colliding with another object.
        {
            float moveRight = other.max.x - this.min.x;
            float moveLeft = other.min.x - this.max.x;

            float moveUp = other.max.y - this.min.y;
            float moveDown = other.min.y - this.max.y;

            Vector3 fix = Vector3.zero;

            if(Mathf.Abs(moveLeft) < Mathf.Abs(moveRight))
            {
                fix.x = moveLeft;
            } else
            {
                fix.x = moveRight;
            }

            if(Math.Abs(moveUp) < Mathf.Abs(moveDown)) // Important for one-way platforms.
            {
                fix.y = moveUp;
            } else if (!other.oneWay)
            {
                fix.y = moveDown;
            }

            if(Mathf.Abs(fix.x) < Mathf.Abs(fix.y))
            {
                fix.y = 0;
            } else
            {
                fix.x = 0;
            }

            return fix;
        }

        private void OnDrawGizmos()
        {
            // draws stuff in the scene view...

            Gizmos.DrawWireCube(transform.position, boxSize);
        }
    }
}