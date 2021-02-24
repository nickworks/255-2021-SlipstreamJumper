using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jelsomeno
{
    public class SpringBlock : OverlapObject
    {

        public void PlayerHit(PlayerMovement pm)
        {
            pm.LaunchPlayer(new Vector3(0, 10, 0));
        }

    }
}
