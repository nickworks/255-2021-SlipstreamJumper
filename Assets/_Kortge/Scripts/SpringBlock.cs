using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Shoots the player into the air upon collision.
/// </summary>
namespace Kortge
{
    public class SpringBlock : OverlapObject
    {
        public override void OnOverlap(PlayerMovement pm)
        {
            pm.LaunchPlayer(new Vector3(0, 20, 0));
        }
    }
}