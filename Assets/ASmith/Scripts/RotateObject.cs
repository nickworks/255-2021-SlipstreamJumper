using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASmith
{
    public class RotateObject : MonoBehaviour
    {
        /// <summary>
        /// References rotation speed on the x-axis, value editable in Inspector
        /// </summary>
        public int xRotationSpeed;

        /// <summary>
        /// References rotation speed on the y-axis, value editable in Inspector
        /// </summary>
        public int yRotationSpeed;

        /// <summary>
        /// References rotation speed on the z-axis, value editable in Inspector
        /// </summary>
        public int zRotationSpeed;

        void Update()
        {
            transform.Rotate(xRotationSpeed * Time.deltaTime, yRotationSpeed * Time.deltaTime, zRotationSpeed * Time.deltaTime); // Rotates the object every frame
        }
    }
}
