using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Gives the player a bandage on overlap with this object.
/// </summary>
namespace Kortge
{
    [RequireComponent(typeof(AABB))]
    public class Bandage : OverlapObject
    {
        public override void OnOverlap(PlayerMovement pm)
        {
            pm.AddBandage();
            Destroy(gameObject);
        }
    }
}