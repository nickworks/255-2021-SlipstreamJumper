using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foster
{
    public class FallGuard : OverlapObjects
    {
        public override void OnOverlap(PlayerMovement pm)
        {
            print("hit");
            SlipstreamJumper.Game.GameOver();

        }
    }
}