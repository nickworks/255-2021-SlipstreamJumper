using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Moves the camera with time.
/// </summary>
namespace Kortge
{
    public class CameraAutoScroll : MonoBehaviour
    {
        public Vector3 scrollSpeed = new Vector3(); // How much to add to position every second. (m/s)

        // Update is called once per frame
        void Update()
        {
            transform.position += scrollSpeed * Time.deltaTime;
        }
    }
}