using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pattison {
    [RequireComponent(typeof(AABB))] // Every Platform component MUST have an AABB component
    public class Platform : MonoBehaviour {

        AABB aabb;

        void Start() {
            aabb = GetComponent<AABB>();

            // register this platform!
            Zone.main.AddPlatform(aabb);
        }
        private void OnDestroy() {
            if (Zone.main == null) return;
            // remove this platform from the list:
            Zone.main.RemovePlatform(aabb);
        }

    }
}