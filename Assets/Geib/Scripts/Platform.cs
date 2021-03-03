using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geib
{
    [RequireComponent(typeof(AABB))] // Every platform component MUST have an AABB component
    public class Platform : MonoBehaviour
    {
        AABB aabb;

        void Start()
        {
            aabb = GetComponent<AABB>();

            // register this platform!
            Zone.main.AddPlatform(aabb);

        }

        private void OnDestroy()
        {
            if (Zone.main != null)
            {
                // Remove this platform from the list:
                Zone.main.RemovePlatform(aabb);
            }
        }

    }
}
