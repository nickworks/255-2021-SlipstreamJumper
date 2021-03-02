using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Szczesniak {
    public class SpringBlock : OverlapObject {

        public void PlayerHit(PlayerMovement pm) {
        }

        public override void OnOverlap(PlayerMovement pm) {
            pm.LaunchPlayer(new Vector3(50, 25, 0));
        }
        
    }
}