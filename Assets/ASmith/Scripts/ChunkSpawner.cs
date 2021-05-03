using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASmith
{
    public class ChunkSpawner : MonoBehaviour
    {
        /// <summary>
        /// Stores the prefab for the first level chunk
        /// </summary>
        public LevelChunk prefab;

        /// <summary>
        /// Stores the prefab for the second level chunk
        /// </summary>
        public LevelChunk prefab2;

        /// <summary>
        /// Creates a list that contains all of the chunks that are
        /// randomly spawned in the game on start
        /// </summary>
        private List<LevelChunk> chunks = new List<LevelChunk>();

        void Start()
        {
            Vector3 pos = Vector3.zero;

            for (int i = 0; i < 10; i++) // If the count is < 10 add more chunks to be spawned at start
            {
                if (chunks.Count > 0)
                {
                    LevelChunk prevChunk = chunks[chunks.Count - 1]; // Counts one more chunk in the list
                pos = prevChunk.connectionPoint.position; // gets the previous chunks connection point based off the levelChunk class
                }

                // Randomizes the range of x and y positions that new chunks can spawn in 
                float x = Random.Range(-1, 2);
                pos.x += x;
                float y = Random.Range(-2, 2); 
                pos.y += y;

                // Sets the z position to always be zero when a chunk spawns
                float z = 0;
                pos.z = z;

                LevelChunk newChunk = Instantiate(prefab, pos, Quaternion.identity);
                // TODO: prefab2 doesnt spawn in correct area
                LevelChunk newChunk2 = Instantiate(prefab2, pos, Quaternion.identity);
                chunks.Add(newChunk);
                chunks.Add(newChunk2);
            }
        }

        void Update()
        {

        }
    }
}

