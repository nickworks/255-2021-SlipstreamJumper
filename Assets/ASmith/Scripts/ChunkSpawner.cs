using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASmith
{
    public class ChunkSpawner : MonoBehaviour
    {
        public LevelChunk prefab;

        private List<LevelChunk> chunks = new List<LevelChunk>();
        void Start()
        {
            Vector3 pos = Vector3.zero;

            for (int i = 0; i < 5; i++)
            {
                if (chunks.Count > 0)
                {
                    LevelChunk prevChunk = chunks[chunks.Count - 1];
                pos = prevChunk.connectionPoint.position;
                }

                //float y = Random.Range(-2, 2f);
                //pos.y += y;

                LevelChunk newChunk = Instantiate(prefab, pos, Quaternion.identity);
                chunks.Add(newChunk);
            }
        }

        void Update()
        {

        }
    }
}

