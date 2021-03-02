using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelsomeno
{
    [RequireComponent(typeof(AABB))]

    public class Platform : MonoBehaviour
    {

        AABB aabb;

        // Start is called before the first frame update
        void Start()
        {
            aabb = GetComponent<AABB>();

            //registar this platform
            Zone.main.AddPlatform(aabb);
        }


        /// <summary>
        /// remove the platform from the list 
        /// </summary>
        private void OnDestroy()
        {
            Zone.main.RemovePlatform(aabb);
        }

    }
}
