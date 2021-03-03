using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geib
{

    public class Coin : OverlapObject
    {

        public override void OnOverlap(PlayerMovement pm)
        {
            
            SoundEffectBoard.PlayCoin();
            
            Destroy(this.gameObject);
        }
    }
}
