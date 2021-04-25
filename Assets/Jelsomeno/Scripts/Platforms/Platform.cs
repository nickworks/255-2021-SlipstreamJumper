using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelsomeno
{
    /// <summary>
    /// needs componenets from the AABB script
    /// </summary>
    [RequireComponent(typeof(AABB))]

    public class Platform : MonoBehaviour
    {
        /// <summary>
        /// reference to AABB script
        /// </summary>
        AABB aabb;

        /// <summary>
        /// isOneWay platform true or false
        /// </summary>
        public bool isOneWay = false;

        // Start is called before the first frame update
        void Start()
        {
            aabb = GetComponent<AABB>();

            aabb.isOneWay = isOneWay; // checks to see if a platform is a oneWay platform

            //registar this platform
            Zone.main.AddPlatform(aabb);
        }


        /// <summary>
        /// remove the platform from the list 
        /// </summary>
        private void OnDestroy()
        {
            if (Zone.main == null) return;
            // remove this platform from the list:
            Zone.main.RemovePlatform(aabb);
        }

    }
}
