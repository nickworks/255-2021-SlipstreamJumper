using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Foster
{
    public class ChunkSpawner : MonoBehaviour
    {

        public Transform prefab;

        void Start()
        {


            for (int i = 0;  i < 5; i++)
            {
            float y = Random.Range(-2.5f, 3);

            Instantiate(prefab, new Vector3(i * 13, y, 0), Quaternion.identity);

            }

        }

      
        void Update()
        {

        }
    }
}
