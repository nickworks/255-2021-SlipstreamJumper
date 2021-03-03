using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Miller
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
            if (Zone.main == null) return; // do nothing
            Zone.main.powerups.Remove(aabb);
        }

        /// <summary>
        /// this function should be overwritten by child classes (hence 'virtual')
        /// </summary>
        /// <param name="pm"></param>
       virtual public void OnOverlap(PlayerMovement pm)
        {

        }
    }
}
