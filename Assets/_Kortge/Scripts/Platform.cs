using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kortge
{
    [RequireComponent(typeof(AABB))] // Every Platform component MUST have an AABB component.
    public class Platform : MonoBehaviour
    {
        AABB aabb;

        // Start is called before the first frame update
        void Start()
        {
            aabb = GetComponent<AABB>();

            // register this platform!
            Zone.main.AddPlatform(aabb);
        }
        /*private void OnDestroy()
        {
            Zone.main.AddPlatform(aabb);
        }*/
        // Update is called once per frame
    }
}
