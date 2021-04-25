using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Szczesniak {
    public class BackgroundFollowPlayer : MonoBehaviour {

        public Transform playerPos;

        void Update() {
            Vector3 backgroundPos = new Vector3(playerPos.position.x, transform.position.y, transform.position.z);
            transform.position = backgroundPos;
            
        }
    }
}