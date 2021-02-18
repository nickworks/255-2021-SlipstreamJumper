using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kortge
{
    [RequireComponent(typeof(AABB))]
    public class SpringBlock : MonoBehaviour
    {
        AABB aabb;
        // Start is called before the first frame update
        void Start()
        {
            aabb = GetComponent<AABB>();
            print(aabb);
            Zone.main.powerups.Add(aabb);
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnDestroy()
        {
            if (Zone.main == null) return;
            Zone.main.powerups.Remove(aabb);
        }
        public void PlayerHit(PlayerMovement pm)
        {
            pm.LaunchPlayer(new Vector3(0, 20, 0));
        }
    }
}