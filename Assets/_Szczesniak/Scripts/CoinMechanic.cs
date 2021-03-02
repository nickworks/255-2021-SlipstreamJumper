using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Szczesniak {
    
    /// <summary>
    /// This Class's purpose is to rotate and make the coin object 'float'
    /// </summary>
    public class CoinMechanic : MonoBehaviour
    {
        /// <summary>
        /// The Rotation of the object
        /// </summary>
        private float rot = 5;

        /// <summary>
        /// The rotation speed the developer wants it to be
        /// </summary>
        public float rotationSpeed = 20;

        /// <summary>
        /// The vector/position of the coin game object
        /// </summary>
        private Vector3 coinPos = new Vector3();
        
        /// <summary>
        /// This float causes the floating effect
        /// </summary>
        private float upOrDown = 0;

        private void Start() {
            coinPos = transform.localPosition; // Getting the posisiton of the coin using local since it is a child object
        }

        void Update() {
            transform.rotation = Quaternion.Euler(0, rot += rotationSpeed * Time.deltaTime, 0); // causing the rotation effect

            // Creating the floating effect
            if (transform.localPosition.y <= 0)
            {
                upOrDown = 3;
            }
            if (transform.localPosition.y >= .03f)
            {
                upOrDown = -3;
            }

            // using Unity's Sin function
            coinPos.y += Mathf.Sin(Time.deltaTime * upOrDown) * .5f;
            
            transform.position += coinPos * Time.deltaTime; // this causes the effect
        }

    }
}