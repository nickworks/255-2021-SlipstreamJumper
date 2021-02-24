using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Szczesniak {
    [RequireComponent(typeof(AABB))]
    public class OverlapObject : MonoBehaviour {
        // Start is called before the first frame update
        AABB aabb;

        void Start() {
            aabb = GetComponent<AABB>();
            Zone.main.powerups.Add(aabb);
            // add block to list of hazards?
        }

        // Update is called once per frame
        private void OnDestroy() {
            if (Zone.main == null) return; // do nothing.
            Zone.main.powerups.Remove(aabb);
        }

        /// <summary>
        /// This function should be overridden by child classes (hence 'virtual')
        /// </summary>
        /// <param name="pm"></param>
        virtual public void OnOverlap(PlayerMovement pm) {

        }
    }
}