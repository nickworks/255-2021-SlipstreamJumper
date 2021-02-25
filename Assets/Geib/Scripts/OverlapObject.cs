using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geib
{
    [RequireComponent(typeof(AABB))]
    public class OverlapObject : MonoBehaviour
    {
        AABB aabb;
     
        void Start()
        {
            aabb = GetComponent<AABB>();
            Zone.main.powerups.Add(this.aabb);
        }

        private void OnDestroy()
        {
            if (Zone.main == null) return; // Do nothing
            Zone.main.powerups.Remove(aabb);
        }
        /// <summary>
        /// This is going to be overridden by child classes (hence 'virtual')
        /// </summary>
        /// <param name="pm"></param>
        virtual public void OnOverlap(PlayerMovement pm)
        {

        }
    }
}
