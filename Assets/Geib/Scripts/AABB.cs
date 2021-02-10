using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geib
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
        public void RecalcAABB()
        {
            min = transform.position - boxSize / 2;
            max = transform.position + boxSize / 2;
        }

        public bool OverlapCheck(AABB other)
        {
            if (other.min.x > this.max.x) return false; // gap to right - NO COLLiSION
            if (other.max.x < this.min.x) return false; // gap to left - NO COLLiSION

            if (other.min.y > this.max.y) return false; // gap to above - NO COLLiSION
            if (other.max.y < this.min.y) return false; // gap to below - NO COLLiSION

            if (other.min.z > this.max.z) return false; // gap to in front of the player - NO COLLiSION
            if (other.max.z < this.min.z) return false; // gap to behind the player - NO COLLiSION

            return true;
        }

        private void OnDrawGizmos()
        {
            // draws stuff in the scene view...

            Gizmos.DrawWireCube(transform.position, boxSize);
        }
    }
}
