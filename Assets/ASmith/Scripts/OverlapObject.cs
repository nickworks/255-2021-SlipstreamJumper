using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASmith
{
    [RequireComponent(typeof(AABB))]
    public class OverlapObject : MonoBehaviour // Parent class for all Hazards and Powerups
    {
        AABB aabb;
        void Start()
        {
            aabb = GetComponent<AABB>();
            Zone.main.powerups.Add(aabb);
        }

        void Update()
        {

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

