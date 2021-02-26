using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pattison {
    public class ChunkSpawner : MonoBehaviour {

        public Chunk prefab;
        private List<Chunk> chunks = new List<Chunk>();

        void Start() {

            for (int i = 0; i < 5; i++) {

                Vector3 pos = Vector3.zero;

                if (chunks.Count > 0) {
                    Chunk lastChunk = chunks[chunks.Count - 1];
                    pos = lastChunk.connectionPoint.position;
                }

                //float y = Random.Range(-2, 2f);
                //pos.y += y;

                Chunk newChunk = Instantiate(prefab, pos, Quaternion.identity);
                chunks.Add(newChunk);
            }
        }

        void Update() {

        }
    }
}