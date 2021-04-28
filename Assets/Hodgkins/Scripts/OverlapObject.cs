using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hodgkins
{
    public class OverlapObject : MonoBehaviour
    {
        AABB aabb;
        // Start is called before the first frame update
        void Start()
        {
            aabb = GetComponent<AABB>();
            Zone.main.powerups.Add(aabb);
        }

        /// <summary>
        /// When an overlap-object has to be destroyed
        /// </summary>
        private void OnDestroy()
        {
            if (Zone.main == null) return; // do nothing
            Zone.main.powerups.Remove(aabb);
        }

        /// <summary>
        /// This function should be overridden by child classes (hence 'virtual')
        /// </summary>
        /// <param name="pm"></param>
        virtual public void OnOverlap(PlayerMovement pm)
        {
            //pm.LaunchPlayer(new Vector3(0, 20, 0));
        }
    }
}