using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A prefab that is spawned to create a row of obstacles the player must get through.
/// </summary>
namespace Kortge
{
    public class Chunk : MonoBehaviour
    {
        /// <summary>
        /// The object that spawned this chunk.
        /// </summary>
        public ChunkSpawner spawner;

        /// <summary>
        /// The camera used to keep track of what chunks are on-screen.
        /// </summary>
        public GameObject cam;
        /// <summary>
        /// The player character that the "player movement" component is gotten from.
        /// </summary>
        public GameObject player;

        /// <summary>
        /// The end of the chunk, with the chunk spawner spawning chunks to the right of it.
        /// </summary>
        public Transform connectionPoint;

        /// <summary>
        /// The "player movement" component of the player character. Used to alter its checkpoint.
        /// </summary>
        public PlayerMovement movement;

        /// <summary>
        /// Keeps track of if a checkpoint has been set, because there would be no need to reset it afterwards.
        /// </summary>
        private bool checkpointSet = false;

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