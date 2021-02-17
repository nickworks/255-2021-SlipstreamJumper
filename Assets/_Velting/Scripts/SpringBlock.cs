using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Velting
{
    public class SpringBlock : MonoBehaviour
    {

        AABB aabb;
        // Start is called before the first frame update
        void Start()
        {
            aabb = GetComponent<AABB>();
            ZoneScript.main.powerups.Add(aabb);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDestroy()
        {
            if (ZoneScript.main == null) return;
            ZoneScript.main.powerups.Remove(aabb);
        }

        public void PlayerHit(PlayerMovement pm)
        {
            pm.LaunchPlayer(new Vector3(0, 20, 0));
        }
    }
}