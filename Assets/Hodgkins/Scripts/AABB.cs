using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hodgkins
{

    public class AABB : MonoBehaviour
    {
        public Vector3 boxSize;

        public Vector3 min;
        public Vector3 max;


        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            RecalcAABB();
        }

        /// <summary>
        /// This function should be called whenever the position 
        /// or size of the collider changes
        /// </summary>
        private void RecalcAABB()
        {
            //min.x = transform.position.x - boxSize.x / 2;
            //min.y = transform.position.y - boxSize.y / 2;
            //min.z = transform.position.z - boxSize.z / 2;

            min = transform.position - boxSize / 2;

        }

        public bool OverlapCheck(AABB other)
        {
            if (other.min.x > this.max.x) return false; //gap to right == no collision
            if (other.min.x < this.max.x) return false; //gap to left == no collision

            if (other.min.y > this.max.y) return false; //gap above == no collision
            if (other.min.y < this.max.y) return false; //gap below == no collision

            if (other.min.z > this.max.z) return false; //gap 'forward' == no collision
            if (other.min.z < this.max.z) return false; //gap 'behind' == no collision


            return true;
        }

        private void OnDrawGizmos()
        {
            // draws stuff in scene view.

            Gizmos.DrawWireCube(transform.position, boxSize);
        }
    }
}