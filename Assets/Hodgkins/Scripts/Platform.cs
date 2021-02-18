using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hodgkins
{
    [RequireComponent(typeof(AABB))] // every platform component must have an AABB component
    public class Platform : MonoBehaviour
    {

        AABB aabb;

        void Start()
        {
            aabb = GetComponent<AABB>();

            //register this platform
            Zone.main.AddPlatform(aabb);
        }
        private void OnDestroy()
        {
            Zone.main.RemovePlatform(aabb);   
        }
    }
}
