using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kortge
{
    public class AABB : MonoBehaviour
    {
        public Vector3 boxSize;

        public Vector3 min;
        public Vector3 max;

        public bool oneWay;

        // Start is called before the first frame update
        void Start()
        {
            RecalcAABB();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void RecalcAABB()
        {
            //min.x = transform.position.x - boxSize.x / 2;
            //min.y = transform.position.y - boxSize.y / 2;
            //min.z = transform.position.z - boxSize.z / 2;

            min = transform.position - boxSize / 2;
            max = transform.position + boxSize / 2;
        }

        public bool OverlapCheck(AABB other)
        {
            if (other.min.x > this.max.x) return false; // Gap to right.
            if (other.max.x < this.min.x) return false; // Gat to left.

            if (other.min.y > this.max.y) return false; // Gap up.
            if (other.max.y < this.min.y) return false; // Gap down.

            if (other.min.z > this.max.z) return false; // Gap forward.
            if (other.max.z < this.min.z) return false; // Gap backward.

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