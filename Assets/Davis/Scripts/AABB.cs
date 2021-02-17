using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Davis { 
public class AABB : MonoBehaviour
{
    public Vector3 boxSize;

    public Vector3 min;
    public Vector3 max;

    // Start is called before the first frame update
    void Start()
    {
        RecalcAABB();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// this function should be called whenever the position or
    /// size of the collider changes.
    /// </summary>
    public void RecalcAABB()
    {
        //min.x = transform.position.x - boxSize.x / 2;
        //min.y = transform.position.y - boxSize.y / 2;
        //min.z = transform.position.x - boxSize.x / 2;

        min = transform.position - boxSize / 2;
        max = transform.position + boxSize / 2;
    }

    public bool OverlapCheck(AABB other)
    {
        if (other.min.x > this.max.x) return false; // gap to right
        if (other.max.x < this.min.x) return false; // gap to left

        if (other.min.y > this.max.y) return false; // gap to above
        if (other.max.y < this.min.y) return false; // gap to below

        if (other.min.z > this.max.z) return false; // gap "forward"
        if (other.max.z < this.min.z) return false; // gap "behind"

        return true;
    }



    public Vector3 FindFix(AABB other)
    {
            float moveRight = other.max.x - this.min.x; //positive no.
            float moveLeft = other.min.x - this.max.x; //negative no.

            float moveUp = other.max.y - this.min.y; //positive no.
            float moveDown = other.min.y - this.max.y; //negative no.

            Vector3 fix = Vector3.zero;

            if(Mathf.Abs(moveLeft) < Mathf.Abs(moveRight))
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
                fix.y = 0; //going w/ horizontal solution + clearing the vertical
            }
            else
            {
                fix.x = 0; //going w/ vertical solution + clearing the horizontal
            }

            return fix;
    }
    private void OnDrawGizmos()
    {
        //draws stuff in the scene view...

        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}
}