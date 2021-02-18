using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Szczesniak
{
    [RequireComponent(typeof(AABB))] // Every platform component MUST have a AABB component
    public class Platform : MonoBehaviour
    {
        AABB aabb;

        void Start()
        {
            aabb = GetComponent<AABB>();

            // register this platform!
            Zone.main.AddPlatform(aabb);
        }

        private void OnDestroy() {
            // remove this platform from the list:
            Zone.main.RemovePlatform(aabb);
        }
    }
}