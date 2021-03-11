using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Objects that the player cannot pass through, added to the zone index.
/// </summary>
namespace Kortge
{
    [RequireComponent(typeof(AABB))] // Every Platform component MUST have an AABB component.
    public class Platform : MonoBehaviour
    {
        AABB aabb; // The object's collider.

        // Start is called before the first frame update
        void Start()
        {
            aabb = GetComponent<AABB>();

            // register this platform!
            Zone.main.AddPlatform(aabb);
        }
    }
}
