using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelsomeno
{
    public class ChunkSpawner : MonoBehaviour
    {

        public Transform prefab;


        void Start()
        {


            for (int i = 0; i < 1; i++)
            {
                // this will randomize how far the chunks can spawn the y and x axises
                float y = Random.Range(25, 40f);
                float x = Random.Range(35, 65f);
                Instantiate(prefab, new Vector3(i * 55, y, 0), Quaternion.identity);



            }

        }

        void Update()
        {

        }
    }
}

