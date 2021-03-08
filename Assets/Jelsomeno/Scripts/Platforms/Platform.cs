using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelsomeno
{
    [RequireComponent(typeof(AABB))]

    public class Platform : MonoBehaviour
    {

        AABB aabb;
        public bool isOneWay = false;

        // Start is called before the first frame update
        void Start()
        {
            aabb = GetComponent<AABB>();

            aabb.isOneWay = isOneWay;

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
