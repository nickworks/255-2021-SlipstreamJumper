using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelsomeno
{


    public class AABB : MonoBehaviour
    {
        public Vector3 boxSize;

        public Vector3 min;
        public Vector3 max;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            RecalcAABB();


        }

        /// <summary>
        /// This function should be called whenever the postion or size of the collider changes
        /// </summary>
        public void RecalcAABB()
        {

            min = transform.position - boxSize / 2;
            max = transform.position + boxSize / 2;

        }

        public bool OverlapCheck(AABB other)
        {
            if (other.min.x > this.max.x) return false; //gap to the right - NO COLLISION
            if (other.max.x < this.min.x) return false; //gap to the left - NO COLLISION

            if (other.min.y > this.max.y) return false; //gap to the above - NO COLLISION
            if (other.max.y < this.min.y) return false; //gap to the below - NO COLLISION

            if (other.min.z > this.max.z) return false; //gap to the "forward" - NO COLLISION
            if (other.max.z < this.min.z) return false; //gap to the "behind" - NO COLLISION

            return true;
        }

        private void OnDrawGizmos()
        {
            // draws stuff in the scene view...
            Gizmos.DrawWireCube(transform.position, boxSize);
        }
    }
}
