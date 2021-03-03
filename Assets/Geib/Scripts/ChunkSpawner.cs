using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geib {

    public class ChunkSpawner : MonoBehaviour
    {
        /// <summary>
        /// This varaible controlls the position at which new chunks spawn.
        /// </summary>
        public Transform prefab1;
        /// <summary>
        /// This variable holds the prefab for chunk 2.
        /// </summary>
        public Transform prefab2;
        /// <summary>
        /// This variable holds the prefab for chunk 3.
        /// </summary>
        public Transform prefab3;
            /// <summary>
            /// This variable holds the prefab for chunk 4.
            /// </summary>
        public Transform prefab4;
        /// <summary>
        /// This variable holds the prefab for chunk 5.
        /// </summary>
        public Transform prefab5;

        private int spawnChunk;

        void Start()
        {

            for (int i = 0; i < 100; i++)
            {
                float y = Random.Range(-2f, 2f);
                int spawnChunk = Random.Range(1, 6);
                Debug.Log(spawnChunk);
                
                switch (spawnChunk)
                {
                    case 1:
                Instantiate(prefab1, new Vector3(i * 18, y, 0), Quaternion.identity);
                    break;

                    case 2:
                        
                Instantiate(prefab2, new Vector3(i * 18, y, 0), Quaternion.identity);
                    break;
                    case 3:

                        Instantiate(prefab3, new Vector3(i * 18, y, 0), Quaternion.identity);
                        break;
                    case 4:

                        Instantiate(prefab4, new Vector3(i * 18, y, 0), Quaternion.identity);
                        break;
                    case 5:

                        Instantiate(prefab5, new Vector3(i * 18, y, 0), Quaternion.identity);
                        break;
                }
            }
        }
    }
}