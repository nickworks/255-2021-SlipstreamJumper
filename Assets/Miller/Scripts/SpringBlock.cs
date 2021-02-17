using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Miller
{
    public class SpringBlock : MonoBehaviour
    {

        AABB aabb;

        void Start()
        {
            aabb = GetComponent<AABB>();
            Zone.main.powerups.Add(aabb);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDestroy()
        {
            if (Zone.main == null) return; // do nothing
            Zone.main.powerups.Remove(aabb);
        }

        public void PlayerHit(PlayerMovement pm)
        {
            pm.LaunchPlayer(new Vector3(0, 20, 0));
        }
    }
}
