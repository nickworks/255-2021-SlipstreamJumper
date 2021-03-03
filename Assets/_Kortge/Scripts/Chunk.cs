using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kortge
{
    public class Chunk : MonoBehaviour // A prefab that is spawned to create a row of obstacles the player must get through.
    {
        public ChunkSpawner spawner; // The object that spawned this chunk.

        public GameObject cam; // The camera used to keep track of what chunks are on-screen.
        public GameObject player; // The player character that the "player movement" component is gotten from.

        public Transform connectionPoint; // The end of the chunk, with the chunk spawner spawning chunks to the right of it.

        public PlayerMovement movement; // The "player movement" component of the player character. Used to alter its checkpoint.

        private bool checkpointSet = false; // Keeps track of if a checkpoint has been set, because there would be no need to reset it afterwards.

        // Start is called before the first frame update
        void Start() // Gets the camera and player movement components.
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera");
            player = GameObject.FindGameObjectWithTag("Player");
            movement = player.GetComponent<PlayerMovement>();
        }

        // Update is called once per frame
        void Update()
        {
            if (transform.position.x - cam.transform.position.x < 11.3f && !checkpointSet) { // Sets checkpoint the chunk is within the camera.
                movement.checkpoint = transform.position;
                checkpointSet = true;
            }

            if (transform.position.x - cam.transform.position.x < -21.3f) // Spawns a new chunk and destroys this one when it is off screen.
            {
                spawner.AddChunk();
                Destroy(gameObject);
            }
        }
    }
}