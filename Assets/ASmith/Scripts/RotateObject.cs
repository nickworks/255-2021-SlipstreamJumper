using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASmith
{
    public class RotateObject : MonoBehaviour
    {
        public int xRotationSpeed;
        public int yRotationSpeed;
        public int zRotationSpeed;

        void Update()
        {
            transform.Rotate(xRotationSpeed * Time.deltaTime, yRotationSpeed * Time.deltaTime, zRotationSpeed * Time.deltaTime);
        }
    }
}
