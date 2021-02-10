using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Velting
{


    public class AABB : MonoBehaviour
    {

        public Vector3 boxSize;

        public Vector3 min;
        public Vector3 max;


        void Start()
        {
            RecalcAABB();
        
        }


        void Update()
        {
        }

        /// <summary>
        /// This function should be called whenever the position or
        /// size of the collider changes.
        /// </summary>
        public void RecalcAABB()
        {
        
            //min.x = transform.position.x - boxSize.x / 2;
            //min.y = transform.position.y - boxSize.y / 2;
            //min.z = transform.position.y - boxSize.z / 2;
        
            min = transform.position - boxSize / 2;
            max = transform.position + boxSize / 2;
        }

        public bool OverlapCheck(AABB other)
        {

            if (other.min.x > this.max.x) return false; // gap to right - No Collision
            if (other.max.x < this.min.x) return false; //gap to left - No Collision

            if (other.min.y > this.max.y) return false; //gap above - No Collision
            if (other.max.y < this.min.y) return false; //gap below - No Collision

            if (other.min.z > this.max.z) return false; //gap behind - No Collision
            if (other.max.z < this.min.z) return false; //gap before - No Collision

            return true;
        
        }

        public Vector3 FindFix(AABB other)
        {
            float moveRight = other.max.x - this.min.x;//positive
            float moveLeft = other.min.x - this.max.x;//negative

            float moveUp = other.max.y - this.min.y;//positive
            float moveDown = other.min.y - this.max.y;//negative

            Vector3 fix = Vector3.zero;

            if (Mathf.Abs(moveLeft) < Mathf.Abs(moveRight))
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

            if(Mathf.Abs(fix.x) < Mathf.Abs(fix.y))
            {
                fix.y = 0; //going horizontal, no vertical solution
            }
            else
            {
                fix.x = 0; //going verticle, no horizontal
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