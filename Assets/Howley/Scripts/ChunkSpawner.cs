using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Howley
{
    public class ChunkSpawner : MonoBehaviour
    {

        public Transform prefab;

        // Start is called before the first frame update
        void Start()
        {
            for(int i = 0; i < 5; i++)
            {
                float y = Random.Range(-2, 2f);
                Instantiate(prefab, new Vector3(i * 18, y, 0), Quaternion.identity);
            }

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

