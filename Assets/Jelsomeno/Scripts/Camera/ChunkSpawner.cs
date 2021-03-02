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


            for (int i = 0; i < 5; i++)
            {
                // this will randomize how far the chunks can spawn the y and x axises
                float y = Random.Range(20, 30f);
                float x = Random.Range(15, 55f);
                Instantiate(prefab, new Vector3(i * 35, y, 0), Quaternion.identity);



            }

        }

        void Update()
        {

        }
    }
}

