using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Howley
{
    [RequireComponent(typeof(AABB))] // Every platform component must have AABB 
    public class Platform : MonoBehaviour
    {
        AABB aabb;
        


        void Start()
        {
            aabb = GetComponent<AABB>();

            // register this platform.
            Zone.main.AddPlatform(aabb);
        }

        /// <summary>
        /// Remove this platform from the list.
        /// </summary>
        private void OnDestroy()
        {
            Zone.main.RemovePlatform(aabb);
        }
    }
}

