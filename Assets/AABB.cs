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

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
   
}
