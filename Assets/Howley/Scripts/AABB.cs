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

        void Start()
        {

        }


        void Update()
        {
            RecalcAABB();
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
            if (other.min.x > this.max.x) return false; // gap to the right - NO COLLISION
            if (other.max.x < this.min.x) return false; // gap to the left - NO COLLISION

            if (other.min.y > this.max.y) return false; // gap above - NO COLLISION
            if (other.max.y < this.min.y) return false; // gap below - NO COLLISION

            if (other.min.z > this.max.z) return false; // gap "forward" or "in front of" - NO COLLISION
            if (other.max.z < this.min.z) return false; // gap "behind" - NO COLLISION

            return true;
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



