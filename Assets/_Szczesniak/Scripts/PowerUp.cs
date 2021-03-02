using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Szczesniak{
    public class PowerUp : OverlapObject {
        public override void OnOverlap(PlayerMovement pm) {
            PlayerMovement doubleJump = pm.GetComponent<PlayerMovement>();

            if (doubleJump) {
                doubleJump.powerUpSpeed += 2;
                Destroy(gameObject);
            }
        }
    }
}