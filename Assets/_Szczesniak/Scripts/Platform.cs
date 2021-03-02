using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Szczesniak
{
    [RequireComponent(typeof(AABB))] // Every platform component MUST have a AABB component
    
    /// <summary>
    /// This class is for all of the level chunks that the player will walk and interact with
    /// </summary>
    public class Platform : MonoBehaviour
    {
        /// <summary>
        /// Getting the AABB class
        /// </summary>
        AABB aabb;

        void Start()
        {
            aabb = GetComponent<AABB>(); // Getting the AABB

            // register this platform!
            Zone.main.AddPlatform(aabb);
        }

        /// <summary>
        /// Remove platform from list
        /// </summary>
        private void OnDestroy() {
            // remove this platform from the list:
            Zone.main.RemovePlatform(aabb);
        }
    }
}