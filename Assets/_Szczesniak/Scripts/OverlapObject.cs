using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Szczesniak {
    [RequireComponent(typeof(AABB))] // Requires AABB script

    /// <summary>
    /// This Class is for when the player overlaps on other objects with AABB
    /// </summary>
    public class OverlapObject : MonoBehaviour {
        // Start is called before the first frame update
        AABB aabb;

        void Start() {
            aabb = GetComponent<AABB>();
            Zone.main.powerups.Add(aabb); // added to list
            // add block to list of hazards?
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        private void OnDestroy() {
            if (Zone.main == null) return; // do nothing.
            Zone.main.powerups.Remove(aabb); // remove from list
        }

        /// <summary>
        /// This function should be overridden by child classes (hence 'virtual')
        /// </summary>
        /// <param name="pm"></param>
        virtual public void OnOverlap(PlayerMovement pm) {

        }
    }
}