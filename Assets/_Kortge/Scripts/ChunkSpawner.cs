using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Spawns one of the chunks off-screen while connecting it to another chunk.
/// </summary>
namespace Kortge
{
    public class ChunkSpawner : MonoBehaviour
    {
        /// <summary>
        /// The different chunk prefabs that are spawned.
        /// </summary>
        public Chunk finalDestination;
        public Chunk battlefield;
        public Chunk fan;
        public Chunk longjump;
        public Chunk oneWay;
        public Chunk sawblade;
        public Chunk walljump;

        /// <summary>
        /// The next chunk prefab that will be generated.
        /// </summary>
        private Chunk chunkType;

        /// <summary>
        /// A list of all of the chunks that are currently in the game.
        /// </summary>
        private readonly List<Chunk> chunks = new List<Chunk>();

        /// <summary>
        /// A number used to shuffle between different chunk types when assigning them to the chunk type.
        /// </summary>
        private int chunkID = 0;

        void Start() // Adds a chunk to the game if there is less than seven of them.
        {
            for (int i = 1; i < 7; i++) AddChunk();
        }

        public void AddChunk() // Adds a chunk to the game while connecting it to a prior chunk.
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
        }
    }
}