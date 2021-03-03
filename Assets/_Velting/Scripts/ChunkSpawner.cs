using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Velting
{
    public class ChunkSpawner : MonoBehaviour
    {

        public Chunk prefab1;
        public Chunk prefab2;
        public Chunk prefab3;

        private List<Chunk> chunks = new List<Chunk>();

        void Start()
        {

            for(int i = 0; i < 10; i++)
            {

                Vector3 pos = Vector3.zero;

                if (chunks.Count > 0)
                {
                    Chunk lastChunk = chunks[chunks.Count - 1];

                    pos = lastChunk.connectionPoint.position;

                }
                // float y = Random.Range(-2f, 2f);
                int whichChunk = Random.Range(0, 3);

                if (whichChunk == 0)
                {
                    Chunk newChunk = Instantiate(prefab1, pos, Quaternion.identity);
                    chunks.Add(newChunk);
                }

                if (whichChunk == 1)
                {
                    Chunk newChunk = Instantiate(prefab2, pos, Quaternion.identity);
                    chunks.Add(newChunk);
                }

                if (whichChunk == 2)
                {
                    Chunk newChunk = Instantiate(prefab3, pos, Quaternion.identity);
                    chunks.Add(newChunk);
                }
            }
        }

        void Update()
        {

        }
    }

}
