using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foster
{
    [RequireComponent(typeof(AABB))] //every platform compontent must have an AABB component
    public class Platform : MonoBehaviour
    {
        AABB aabb;

        void Start()
        {
            aabb = GetComponent<AABB>();

            //register this platform

            Zone.main.AddPlateform(aabb);
        }

        private void OnDestroy()
        {

            if (Zone.main == null) return;
            Zone.main.RemovingPlatform(aabb);
        }

    }
}