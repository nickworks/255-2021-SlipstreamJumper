using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Foster
{
    public class CameraFollow : MonoBehaviour
    {

        public Transform target;

        void Start()
        {



        }

        
        void LateUpdate()
        {

            Vector3 pos = transform.position;
            pos.x = target.position.x;
            pos.y = target.position.y;

            //transform.position = pos;


            //easing
            transform.position += (pos - transform.position) * Time.deltaTime * 10;

        }
    }
}