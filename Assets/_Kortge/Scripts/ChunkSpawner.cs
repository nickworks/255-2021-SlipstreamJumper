using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kortge
{
    public class ChunkSpawner : MonoBehaviour
    {
        public Chunk finalDestination;
        public Chunk battlefield;
        public Chunk fan;
        public Chunk longjump;
        public Chunk oneWay;
        public Chunk sawblade;
        public Chunk walljump;
        private Chunk chunkType;

        private List<Chunk> chunks = new List<Chunk>();

        private int chunkID = 0;

        void Start()
        {
            for (int i = 1; i < 7; i++) AddChunk();
        }

        public void AddChunk()
        {
            Vector3 pos = Vector3.zero;

            if (chunks.Count > 0)
            {
                Chunk lastChunk = chunks[chunks.Count - 1];

                pos = lastChunk.connectionPoint.position + (transform.right * 1);
            }
            float y = Random.Range(-1f, 1f);

            pos.y += y;

            if (pos.y < -5.5f) pos.y = -5.5f;
            else if (pos.y > -3) pos.y = -3f;

            chunkID += Random.Range(1, 6);
            if (chunkID > 6) chunkID -= 6;
            print(chunkID);
            switch (chunkID)
            {
                case 0:
                    chunkType = finalDestination;
                    break;
                case 1:
                    chunkType = battlefield;
                    break;
                case 2:
                    chunkType = fan;
                    break;
                case 3:
                    chunkType = longjump;
                    break;
                case 4:
                    chunkType = oneWay;
                    break;
                case 5:
                    chunkType = sawblade;
                    break;
                case 6:
                    chunkType = walljump;
                    break;
            }

            Chunk newChunk = Instantiate(chunkType, pos, Quaternion.identity);
            newChunk.spawner = GetComponent<ChunkSpawner>();
            chunks.Add(newChunk);

            //Instantiate(chunkType, new Vector3(i * 10, y, 0), Quaternion.identity);
        }
    }
}