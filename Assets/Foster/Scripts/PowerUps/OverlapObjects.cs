using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Foster
{
    [RequireComponent(typeof(AABB))]
    public class OverlapObjects : MonoBehaviour
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

        //this function will be over written by child classes
       virtual public void OnOverlap(PlayerMovement pm)
        {

        }

    }
}
