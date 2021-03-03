using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Howley
{
    [RequireComponent(typeof(AABB))]
    public class OverlapObject : MonoBehaviour
    {
        /// <summary>
        /// Give all subclasses a reference to the AABB script.
        /// </summary>
        public AABB aabb;

        void Start()
        {
            aabb = GetComponent<AABB>();
            Zone.main.powerups.Add(aabb);
        }

        /// <summary>
        /// This function removes powerups from the array when they are destroyed.
        /// </summary>
        private void OnDestroy()
        {
            if (Zone.main == null) return;
            Zone.main.powerups.Remove(aabb);
        }

        /// <summary>
        /// This will be overidden by child classes.
        /// </summary>
        /// <param name="pm"></param>
        virtual public void OnOverlap(PlayerMovement pm)
        {
            
        }
    }
}

