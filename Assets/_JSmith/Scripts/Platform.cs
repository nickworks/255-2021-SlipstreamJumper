using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JSmith
{
    [RequireComponent(typeof(AABB))] // Every platform component must have an AABB component.

    public class Platform : MonoBehaviour
    {

        AABB aabb;

        void Start()
        {
            aabb = GetComponent<AABB>();

            //register this platform!
            Zone.main.AddPlatform(aabb);
        }

        private void OnDestroy()
        {
            if (Zone.main == null) return;
            //remove this platform from the list
            Zone.main.RemovePlatform(aabb);
        }


    }
}
