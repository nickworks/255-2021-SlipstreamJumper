using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jelsomeno
{
    [RequireComponent(typeof(AABB))]

    public class OverlapObject : MonoBehaviour
    {
        AABB aabb;

        void Start()
        {
            aabb = GetComponent<AABB>();
            Zone.main.powerups.Add(aabb);
        }

        private void OnDestroy()
        {
            if (Zone.main == null) return;
            Zone.main.powerups.Remove(aabb);
        }


        /// <summary>
        /// this function should be overidden by the child classes 
        /// </summary>
        /// <param name="pm"></param>
        virtual public void OnOverlap(PlayerMovement pm)
        {

        }

    }
}
