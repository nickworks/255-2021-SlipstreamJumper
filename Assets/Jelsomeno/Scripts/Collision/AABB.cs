using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelsomeno
{

    public class AABB : MonoBehaviour
    {
        /// <summary>
        /// designates which platforms that allow the player to be pushed up
        /// </summary>
        public bool isOneWay = false;

        public Vector3 boxSize;


        public Vector3 min;
        public Vector3 max;

        private Vector3 velocityCache;
        private Vector3 previousPosition;

        // Start is called before the first frame update
        void Start()
        {
            RecalcAABB();

        }

        private void Update()
        {
            velocityCache = previousPosition = transform.position;
            previousPosition = transform.position;
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
            if (other.isOneWay){
                if (this.velocityCache.y < 0) return false;// this AABB is moving up and cant collided 
                if (this.min.y < other.transform.position.y) return false;
            }

            if (other.min.x > this.max.x) return false; //gap to the right - NO COLLISION
            if (other.max.x < this.min.x) return false; //gap to the left - NO COLLISION

            if (other.min.y > this.max.y) return false; //gap to the above - NO COLLISION
            if (other.max.y < this.min.y) return false; //gap to the below - NO COLLISION

            if (other.min.z > this.max.z) return false; //gap to the "forward" - NO COLLISION
            if (other.max.z < this.min.z) return false; //gap to the "behind" - NO COLLISION

            return true;
        }

        public Vector3 FindFix(AABB other)
        {

            float moveRight = other.max.x - this.min.x; // pos number
            float moveLeft = other.min.x - this.max.x; // neg number

            float moveUp = other.max.y - this.min.y; // pos number
            float moveDown = other.min.y - this.max.y; // neg number

            Vector3 fix = Vector3.zero;

            if (other.isOneWay)
            {
                return new Vector3(0, moveUp, 0);
            }

            if(Mathf.Abs(moveLeft) < Mathf.Abs(moveRight))
            {
                fix.x = moveLeft;
            }
            else
            {
                fix.x = moveRight;
            }

            if(Mathf.Abs(moveUp) < Mathf.Abs(moveDown))
            {
                fix.y = moveUp;
            }
            else
            {
                fix.y = moveDown;
            }

            if (Mathf.Abs(fix.x) < Mathf.Abs(fix.y))
            {
                fix.y = 0;
            }
            else
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
