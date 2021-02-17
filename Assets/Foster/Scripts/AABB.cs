using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Foster
{
    public class AABB : MonoBehaviour
    {
       public Vector3 boxSize;

        public Vector3 min;
        public Vector3 max;



        // Start is called before the first frame update
        void Start()
        {
            RecalAABB();
        }

        // Update is called once per frame
        void Update()
        {



        }

        /// <summary>
        /// This function should be called whenever the position
        /// or size of collider changes
        /// </summary>
        public void RecalAABB()
        {
          
            min = transform.position - boxSize / 2;
            max = transform.position + boxSize / 2;
        }

        public bool OverlapCheck(AABB other)
        {

            if (other.min.x > this.max.x) return false;//gap to right
            if (other.max.x < this.min.x) return false;//gap to left

            if (other.min.y > this.max.y) return false;//gap to above
            if (other.max.y < this.min.y) return false;//gap to below

            if (other.min.z > this.max.z) return false;//gap to forward
            if (other.max.z < this.min.z) return false;//gap to behind
            return true;


        }

        public Vector3 FindFix(AABB other)
        {
            float moveRight = other.max.x - this.min.x;
            float moveLeft = other.min.x - this.max.x;

            float moveUp = other.max.y - this.min.y;
            float moveDown = other.min.y - this.max.y;

            Vector3 fix = Vector3.zero;

            if(Mathf.Abs(moveLeft) < Mathf.Abs(moveRight))
            {
                fix.x = moveLeft;
            }
            else
            {
                fix.x = moveRight;
            }

            if(Mathf.Abs(moveUp)< Mathf.Abs(moveDown))
            {
                fix.y = moveUp;
            }
            else
            {
                fix.y = moveDown;
            }

            if(Mathf.Abs(fix.x) < Mathf.Abs(fix.y))
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
            //draws stuff in the scene view

            Gizmos.DrawWireCube(transform.position, boxSize);
        }
    }
}