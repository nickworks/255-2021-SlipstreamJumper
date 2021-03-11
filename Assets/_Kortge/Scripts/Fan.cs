using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotates the fan prefab with time.
/// </summary>
namespace Kortge
{
    public class Fan : MonoBehaviour
    {

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0, 720 * Time.deltaTime, 0);
        }
    }
}