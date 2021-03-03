using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASmith
{
    [RequireComponent(typeof(AABB))]
    public class OverlapObject : MonoBehaviour // Parent class for all Hazards and Powerups
    {
        /// <summary>
        /// Gets a reference to the AABB class
        /// </summary>
        AABB aabb;
        void Start()
        {
            aabb = GetComponent<AABB>(); // Gets a reference to the AABB class for access to the aabb collision detector
            Zone.main.powerups.Add(aabb);
        }

        private void OnDestroy()
        {
            if (Zone.main == null) return; // do nothing
            Zone.main.powerups.Remove(aabb);
        }

        /// <summary>
        /// This function is overwritten by child classes (because of "virtual")
        /// </summary>
        /// <param name="pm"></param>
        virtual public void OnOverlap(PlayerMovement pm)
        {

        }
    }
}

