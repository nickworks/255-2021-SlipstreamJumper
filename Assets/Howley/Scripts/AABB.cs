using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Howley
{
    /// <summary>
    /// This class creates an AABB collision cube around a game object.
    /// </summary>
    public class AABB : MonoBehaviour
    {
        /// <summary>
        /// Used to designate a platform as a one way platform.
        /// </summary>
        public bool isOneWay = false;
         
        /// <summary>
        /// This value gets the width, height, and depth of the game object.
        /// </summary>
        public Vector3 boxSize;

        /// <summary>
        /// This value stores the minimum for x, y, and z
        /// </summary>
        public Vector3 min;

        /// <summary>
        /// This value stores the maximum for x, y, and z
        /// </summary>
        public Vector3 max;

        /// <summary>
        /// Calculate and store velocity.
        /// </summary>
        private Vector3 velocityCache;
        private Vector3 prevPos;

        /// <summary>
        /// The start function is called once before the first update.
        /// </summary>
        void Start()
        {
            RecalcAABB();
        }

        /// <summary>
        /// The update function is called every game tick.
        /// </summary>
        void Update()
        {
            // Already has deltaTime   
            velocityCache = prevPos - transform.position;
            prevPos = transform.position;
        }

        /// <summary>
        /// This function should be called whenever the position or size
        /// of the collider changes.
        /// </summary>
        public void RecalcAABB()
        {
            //min.x = transform.position.x - boxSize.x / 2;
            //min.y = transform.position.y - boxSize.y / 2;
            //min.z = transform.position.z - boxSize.z / 2;

            // Does the same as the above 3
            min = transform.position - boxSize / 2;
            max = transform.position + boxSize / 2;
        }

        /// <summary>
        /// This function returns true or false based on
        /// if the boxes are overlapping.
        /// </summary>
        public bool OverlapCheck(AABB other)
        {
            if (other.isOneWay)
            {
                if (this.velocityCache.x < 0) return false; // The AABB is moving to the right.
                if (this.min.x < other.transform.position.x) return false;
            }

            if (other.min.x > this.max.x) return false; // gap to the right - NO COLLISION
            if (other.max.x < this.min.x) return false; // gap to the left - NO COLLISION

            if (other.min.y > this.max.y) return false; // gap above - NO COLLISION
            if (other.max.y < this.min.y) return false; // gap below - NO COLLISION

            if (other.min.z > this.max.z) return false; // gap "forward" or "in front of" - NO COLLISION
            if (other.max.z < this.min.z) return false; // gap "behind" - NO COLLISION

            return true;
        }

        /// <summary>
        /// Find and return the direction to move by comparing the 4 possible
        /// solutions, comparing them to each other, and returning the
        /// shortest possible solution.
        /// </summary>
        public Vector3 FindFix(AABB other)
        {
            float moveRight = other.max.x - this.min.x; // Positive num
            float moveLeft = this.max.x - other.min.x; // Negative num

            float moveUp = other.max.y - this.min.y; // Positive num
            float moveDown = other.min.y - this.max.y; // Negative num

            Vector3 fix = Vector3.zero;

            if (other.isOneWay)
            {
                return new Vector3(moveRight, 0, 0);
            }

            if (Mathf.Abs(moveLeft) < Mathf.Abs(moveRight))
            {
                fix.x = moveLeft;
            }
            else
            {
                fix.x = moveRight;
            }

            if (Mathf.Abs(moveUp) < Mathf.Abs(moveDown))
            {
                fix.y = moveUp;
            }
            else
            {
                fix.y = moveDown;
            }

            if (Mathf.Abs(fix.x) < Mathf.Abs(fix.y))
            {
                fix.y = 0; // Going with horizontal solution, clear vertical
            }
            else
            {
                fix.x = 0; // Going with vertical solution, clear horizontal
            }

            return fix;
        }

        /// <summary>
        /// This function draws the wire box of collision.
        /// </summary>
        private void OnDrawGizmos()
        {
            // Draws in the scene view...

            Gizmos.DrawWireCube(transform.position, boxSize);
        }
    }
}



