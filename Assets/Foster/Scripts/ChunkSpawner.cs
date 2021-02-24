using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Foster
{
    public class ChunkSpawner : MonoBehaviour
    {

        //public Transform prefab;
        public LevelChunk prefab;

        private List<LevelChunk> chunks = new List<LevelChunk>();

        void Start()
        {


            for (int i = 0; i < 5; i++)
            {
                Vector3 pos = Vector3.zero;

                if (chunks.Count > 0)
                {
                    LevelChunk lastChunk = chunks[chunks.Count - 1];
                    pos = lastChunk.connectionPoint.position;
                }

                //  float y = Random.Range(-2.5f, 3);
                //  pos.y += y;

                LevelChunk newLevelChunk = Instantiate(prefab, pos, Quaternion.identity);
                chunks.Add(newLevelChunk);

                // Instantiate(prefab, new Vector3(i * 13, y, 0), Quaternion.identity);


            }

        }

    }

}
