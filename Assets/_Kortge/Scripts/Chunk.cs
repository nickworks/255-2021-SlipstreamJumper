using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kortge
{
    public class Chunk : MonoBehaviour
    {
        public ChunkSpawner spawner;
        public Transform connectionPoint;
        public GameObject cam;
        public GameObject player;
        public PlayerMovement movement;
        private bool checkpointSet;
        private bool quittingApplication = false;

        // Start is called before the first frame update
        void Start()
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera");
            player = GameObject.FindGameObjectWithTag("Player");
            movement = player.GetComponent<PlayerMovement>();
            print(player);
        }

        // Update is called once per frame
        void Update()
        {
            if (transform.position.x - cam.transform.position.x < 11.3f && !checkpointSet) { 
                movement.checkpoint = transform.position;
                checkpointSet = true;
            }

            if (transform.position.x - cam.transform.position.x < -21.3f) Destroy(gameObject);
        }

        private void OnDestroy()
        {
            if (!quittingApplication)spawner.AddChunk();
        }

        private void OnApplicationQuit()
        {
            quittingApplication = true;
        }
    }
}