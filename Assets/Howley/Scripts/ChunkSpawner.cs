using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Howley
{
    public class ChunkSpawner : MonoBehaviour
    {
        /// <summary>
        /// Reference to the first prefab chunk.
        /// </summary>
        public Chunk prefab;

        /// <summary>
        /// Reference to the second prefab chunk.
        /// </summary>
        public Chunk prefab1;

        /// <summary>
        /// Reference to the third prefab chunk.
        /// </summary>
        public Chunk prefab2;

        /// <summary>
        /// Reference to the fourth prefab chunk.
        /// </summary>
        public Chunk prefab3;

        /// <summary>
        /// Reference to the fifth prefab chunk.
        /// </summary>
        public Chunk prefab4;

        /// <summary>
        /// An array list that holds all of the chunks in the level.
        /// </summary>
        private List<Chunk> chunk = new List<Chunk>();

        /// <summary>
        /// The start funciton is called once before the update begins.
        /// </summary>
        void Start()
        {
            // loop through this for statement 20 times
            for(int i = 0; i < 20; i++)
            {
                Vector3 pos = Vector3.zero;

                if (chunk.Count > 0)
                {
                    Chunk lastChunk = chunk[chunk.Count - 1];
                    pos = lastChunk.connectionPoint.position;
                }

                //float y = Random.Range(-2, 2f);
                //pos.y += y;
                float inst = Random.Range(0, 5); // Pick a random number somewhere between 0 and 5


                // Choose which prefab to spawn based on the random number.
                if (inst >= 0 && inst < 1)
                {
                    Chunk newChunk = Instantiate(prefab, pos, Quaternion.identity);
                    chunk.Add(newChunk);
                }
                if (inst >= 1 && inst < 2)
                {
                    Chunk newChunk1 = Instantiate(prefab1, pos, Quaternion.identity);
                    chunk.Add(newChunk1);
                }
                if (inst >= 2 && inst < 3)
                {
                    Chunk newChunk2 = Instantiate(prefab2, pos, Quaternion.identity);
                    chunk.Add(newChunk2);
                }
                if (inst >= 3 && inst < 4)
                {
                    Chunk newChunk3 = Instantiate(prefab3, pos, Quaternion.identity);
                    chunk.Add(newChunk3);
                }
                if (inst >= 4 && inst <= 5)
                {
                    Chunk newChunk4 = Instantiate(prefab4, pos, Quaternion.identity);
                    chunk.Add(newChunk4);
                }                
            }
        }
    }
}

