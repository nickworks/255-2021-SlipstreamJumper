using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geib
{
    [RequireComponent(typeof(AABB))]
    public class SpringBlock : MonoBehaviour
    {

        AABB aabb;

        // Start is called before the first frame update
        void Start()
        {
            aabb = GetComponent<AABB>();
            Zone.main.powerups.Add(this.aabb);
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnDestroy()
        {
            if (Zone.main == null) return; // Do nothing
            Zone.main.powerups.Remove(aabb);
        }
        public void PlayerHit(PlayerMovement pm)
        {
            pm.LaunchPlayer(new Vector3(0, 20, 0));
        }
    }
}
