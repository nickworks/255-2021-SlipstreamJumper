using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Howley
{
    public class ChunkSpawner : MonoBehaviour
    {

        public Chunk prefab;

        private List<Chunk> chunk = new List<Chunk>();

        // Start is called before the first frame update
        void Start()
        {
            for(int i = 0; i < 5; i++)
            {
                Vector3 pos = Vector3.zero;

                if (chunk.Count > 0)
                {
                    Chunk lastChunk = chunk[chunk.Count - 1];
                    pos = lastChunk.connectionPoint.position;
                }


                //float y = Random.Range(-2, 2f);
                //pos.y += y;

                Chunk newChunk = Instantiate(prefab, pos, Quaternion.identity);
                chunk.Add(newChunk);
            }

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

