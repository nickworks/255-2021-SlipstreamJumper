using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        RecalAABB();
    }

    /// <summary>
    /// This function should be called whenever the position or size of the collider changes.
    /// </summary>
    public void RecalAABB()
    {
        min = transform.position - boxSize / 2;
        max = transform.position + boxSize / 2;
    }
    public bool OverlapCheck(AABB other)
    {
        if (other.min.x > this.max.x) return false; // gap to right - NO COLLISION
        if (other.max.x < this.min.x) return false; // gap to right - NO COLLISION

        if (other.min.y > this.max.y) return false; // gap to above - NO COLLISION
        if (other.max.y < this.min.y) return false; // gap to below - NO COLLISION

        if (other.min.z > this.max.z) return false; // gap to in front - NO COLLISION
        if (other.max.z < this.min.z) return false; // gap to behind - NO COLLISION

        return true;
    }

    public Vector3 FindFix(AABB other)
    {

        float moveRight = other.max.x - this.min.x; //positive
        float moveLeft = other.min.x - this.max.x; //negative
        float moveUp = other.max.y - this.min.y; //positive
        float moveDown = other.min.y - this.max.y; //negative

        Vector3 fix = Vector3.zero;

        if(Mathf.Abs(moveLeft) < Mathf.Abs(moveRight))
        {
            fix.x = moveLeft;
        } else
        {
            fix.x = moveRight;
        }

        if (Mathf.Abs(moveUp) < Mathf.Abs(moveDown))
        {
            fix.x = moveUp;
        }
        else
        {
            fix.x = moveDown;
        }

        if(Mathf.Abs(fix.x) < Mathf.Abs(fix.y))
        {
            fix.y = 0; //going with horizontal solution; clearing the vertical.
        } else
        {
            fix.x = 0; //going with the vertical solution; clearing the horizontal.
        }

        return fix;
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
   
}
