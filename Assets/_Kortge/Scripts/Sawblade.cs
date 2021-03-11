using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Spins the sawblade on the saw prefab.
/// </summary>

namespace Kortge
{
    public class Sawblade : MonoBehaviour
    {

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0, 0, 720 * Time.deltaTime);
        }
    }
}