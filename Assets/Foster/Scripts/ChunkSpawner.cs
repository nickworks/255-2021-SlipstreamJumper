using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Foster
{
    public class ChunkSpawner : MonoBehaviour
    {

        //public Transform prefab;
        public LevelChunk prefab;
        public LevelChunk prefab02;
        public LevelChunk prefab03;

        public LevelChunk prefabLoaded;

        public int prefabType = 0;

        private List<LevelChunk> chunks = new List<LevelChunk>();

        void Start()
        {

            for (int i = 0; i < 5; i++)
            {
               
                //Random number generator, each number is assigned to a prefab and then randomly generated
            prefabType = Random.Range(0 , 3);
            switch (prefabType)
                {
                    case 0:
                        prefabLoaded = prefab;
                        break;
                    case 1:
                        prefabLoaded = prefab02;
                        break;
                    case 2:
                        prefabLoaded = prefab03;
                        break;
                }
                Vector3 pos = Vector3.zero;

                if (chunks.Count > 0)
                {
                    LevelChunk lastChunk = chunks[chunks.Count - 1];
                    pos = lastChunk.connectionPoint.position;
                }
                LevelChunk newLevelChunk = Instantiate(prefabLoaded, pos, Quaternion.identity);
                chunks.Add(newLevelChunk);

                


            }

        }

    }

}
