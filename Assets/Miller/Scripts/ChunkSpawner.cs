using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Miller
{
    public class ChunkSpawner : MonoBehaviour
    {

        public LevelChunk prefab;
        
        private List<LevelChunk> chunks = new List<LevelChunk>();

        // spawns out the chunks
        void Start()
        {

            for (int i = 0; i < 15; i++)
            {
                Vector3 pos = Vector3.zero;

                if (chunks.Count > 0)
                {
                    LevelChunk lastChunk = chunks[chunks.Count - 1];
                    pos = lastChunk.connectionPoint.position;
                }

                //float y = Random.Range(-2, 2f);
                //pos.y += y;


                LevelChunk newChunk = Instantiate(prefab, pos, Quaternion.identity);
                chunks.Add(newChunk);
            }
            
        }
    }
}
