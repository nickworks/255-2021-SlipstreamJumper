using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JSmith
{
    public class ChunkSpawner : MonoBehaviour
    {
        public Transform prefab;

        void Start()
        {

            for (int i = 0; i < 5; i++)
            {
                float y = Random.Range(-2, 2f);
                Instantiate(prefab, new Vector3(i * 40, -1, 0), Quaternion.identity);
            }

        }


        void Update()
        {

        }
    }
}
