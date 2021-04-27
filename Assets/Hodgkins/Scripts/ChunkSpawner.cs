using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hodgkins
{
    public class ChunkSpawner : MonoBehaviour
    {
        /// <summary>
        /// All chunk prefabs have their own slot to be put in in the editor
        /// </summary>
        public Chunk prefab;
        public Chunk prefab2;
        public Chunk prefab3;
        public Chunk prefab4;

        public List<Chunk> chunks = new List<Chunk>();

        void Start()
        {            
            for (int i = 0; i < 5; i++)
            {
                Vector3 pos = Vector3.zero;

                if (chunks.Count > 0)
                {
                    Chunk lastChunk = chunks[chunks.Count - 1];
                    pos = lastChunk.connectionPoint.position;
                }

                //float y = Random.Range(-2, 2f);
                //pos.y += y;
                int nextChunk = Random.Range(0, 4);

                /// <summary>
                /// the next chunk to spawn is decided by a random number
                /// </summary>

                if (nextChunk == 0)
                {
                    Chunk newChunk = Instantiate(prefab, pos, Quaternion.identity);
                    chunks.Add(newChunk);
                }
                if (nextChunk == 1)
                {
                    Chunk newChunk = Instantiate(prefab2, pos, Quaternion.identity);
                    chunks.Add(newChunk);
                }
                if (nextChunk == 2)
                {
                    Chunk newChunk = Instantiate(prefab3, pos, Quaternion.identity);
                    chunks.Add(newChunk);
                }
                if (nextChunk == 3)
                {
                    Chunk newChunk = Instantiate(prefab4, pos, Quaternion.identity);
                    chunks.Add(newChunk);
                }
            }
        }


        void Update()
        {

        }
    }
}
