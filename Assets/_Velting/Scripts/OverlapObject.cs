using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Velting
{
    [RequireComponent(typeof(AABB))]
    public class OverlapObject : MonoBehaviour
    {
        AABB aabb;
        void Start()
        {
            aabb = GetComponent<AABB>();
            ZoneScript.main.powerups.Add(aabb);
        }

        private void OnDestroy()
        {
            if (ZoneScript.main == null) return;
            ZoneScript.main.powerups.Remove(aabb);
        }

        /// <summary>
        /// This function should be overidden by child classes (hence 'virtual')
        /// </summary>
        /// <param name="pm"></param>
        virtual public void OnOverlap(PlayerMovement pm)
        {

        }


    }
}