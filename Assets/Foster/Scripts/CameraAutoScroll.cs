using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Foster
{
    public class CameraAutoScroll : MonoBehaviour
    {

        public Vector3 scrollSpeed = new Vector3();

        void Start()
        {

        }

   
        void Update()
        {
            transform.position += scrollSpeed * Time.deltaTime;
        }
    }
}