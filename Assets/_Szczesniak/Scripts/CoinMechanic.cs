using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Szczesniak
{
    public class CoinMechanic : MonoBehaviour
    {
        private float rot = 5;
        public float rotationSpeed = 20;
        private Vector3 coinPos = new Vector3();
        private float currentPos = 0;
        private float upOrDown = 0;

        private void Start() {
            coinPos = transform.localPosition;
        }

        void Update() {
            transform.rotation = Quaternion.Euler(0, rot += rotationSpeed * Time.deltaTime, 0);

            if (transform.localPosition.y <= 0)
            {
                upOrDown = 3;
            }
            if (transform.localPosition.y >= .03f)
            {
                upOrDown = -3;
            }

            coinPos.y += Mathf.Sin(Time.deltaTime * upOrDown) * .5f;
            
            transform.position += coinPos * Time.deltaTime;
        }

    }
}