using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pattison {
    [RequireComponent(typeof(AABB))]
    public class OverlapObject : MonoBehaviour {
        AABB aabb;

        void Start() {
            aabb = GetComponent<AABB>();
            Zone.main.powerups.Add(aabb);
        }

        private void OnDestroy() {
            if (Zone.main == null) return; // do nothing
            Zone.main.powerups.Remove(aabb);
        }

        /// <summary>
        /// This function should be overidden by child classes (hence `virtual`)
        /// </summary>
        /// <param name="pm"></param>
        virtual public void OnOverlap(PlayerMovement pm) {

        }

    }
}