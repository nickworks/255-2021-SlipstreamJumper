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
        public Chunk prefab4;
        public Chunk prefab5;
        public Chunk prefab6;
        public Chunk prefab7;
        public Chunk prefab8;
        public Chunk prefab9;
        public Chunk prefab10;


        private List<Chunk> chunks = new List<Chunk>();

        void Start()
        {

            for(int i = 0; i < 30; i++)
            {

                Vector3 pos = Vector3.zero;

                if (chunks.Count > 0)
                {
                    Chunk lastChunk = chunks[chunks.Count - 1];

                    pos = lastChunk.connectionPoint.position;

                }
                // float y = Random.Range(-2f, 2f);
                int whichChunk = Random.Range(0, 5);

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

                if (whichChunk == 3)
                {
                    Chunk newChunk = Instantiate(prefab4, pos, Quaternion.identity);
                    chunks.Add(newChunk);
                }

                if (whichChunk == 4)
                {
                    Chunk newChunk = Instantiate(prefab5, pos, Quaternion.identity);
                    chunks.Add(newChunk);
                }

                if (whichChunk == 5)
                {
                    Chunk newChunk = Instantiate(prefab6, pos, Quaternion.identity);
                    chunks.Add(newChunk);
                }

                if (whichChunk == 6)
                {
                    Chunk newChunk = Instantiate(prefab7, pos, Quaternion.identity);
                    chunks.Add(newChunk);
                }

                if (whichChunk == 7)
                {
                    Chunk newChunk = Instantiate(prefab8, pos, Quaternion.identity);
                    chunks.Add(newChunk);
                }

                if (whichChunk == 8)
                {
                    Chunk newChunk = Instantiate(prefab9, pos, Quaternion.identity);
                    chunks.Add(newChunk);
                }

                if (whichChunk == 9)
                {
                    Chunk newChunk = Instantiate(prefab10, pos, Quaternion.identity);
                    chunks.Add(newChunk);
                }
            }
        }

        void Update()
        {

        }
    }

}
