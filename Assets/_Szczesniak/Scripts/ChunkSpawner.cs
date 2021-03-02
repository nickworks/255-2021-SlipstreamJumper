using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Szczesniak
{
    /// <summary>
    /// This script spawns the random level chunks within the prefab list
    /// </summary>
    public class ChunkSpawner : MonoBehaviour
    {
        /// <summary>
        /// This holds the level chunks that the developer puts in through the inspector
        /// </summary>
        public List<Transform> prefab = new List<Transform>();

        /// <summary>
        /// Player's starting spawn on the X axis
        /// </summary>
        private float playerSpawnX = 0;

        /// <summary>
        /// This is how many floors is available when the game is playing.
        /// </summary>
        private float amountOfFloors = 5;

        /// <summary>
        /// The length of the floors X axis
        /// </summary>
        private float floorLength = 25;

        /// <summary>
        /// Floors' ID number when they spawn to help calculate the distance with Unity's Instantiate Function
        /// </summary>
        private float floorID = 0;

        /// <summary>
        /// Creating list to store and manage the floors and boasters
        /// </summary>
        private List<Transform> inGameFloors = new List<Transform>();

        void Start() {
            // Spawns the starting floors
            for (int i = 0; i < 5; i++) {
                SpawningFloor();
            }

        }


        void Update() {
            // Checks the players position to know when to spawn and delete level chunks
            if (transform.position.x - 50 > playerSpawnX - (floorLength * amountOfFloors)) {
                SpawningFloor();
                DestroyFloor();
            }
        }

        /// <summary>
        /// This function spawns the floor(s)
        /// </summary>
        private void SpawningFloor() {
            
            // This chooses a random number to determine the level chunk that will be spawned
            int y = Random.Range(0, prefab.Count);

            // This spawns/instantiate the level chunk
            Transform floors = Instantiate(prefab[y], new Vector3(floorID * 25, 0, 0), Quaternion.identity);
            inGameFloors.Add(floors); // added to the list


            // Make boaster spawn
            // Make limiter/spawn designation for it 

            /*if (boasterSpawnNum > randomNumBoaster)
            {
                Transform boaster = Instantiate(boasterPrefab, new Vector3(floorID * 25 + 10, .5f, 0), Quaternion.identity);
                boastersInGame.Add(boaster);

                randomNumBoaster = Random.Range(1, 6); // Decides when the next boaster will spawn
                boasterSpawnNum = 0;
            }

            boasterSpawnNum++;
            */
            playerSpawnX += floorLength; // increments the floor length to the playerSpawnX
            floorID++; // increments spawn number of the level chunk
            
        }

        /// <summary>
        /// This function destroys the floors
        /// </summary>
        private void DestroyFloor() {
            // Destroys the last level chunk the player pasts
            Destroy(inGameFloors[0].gameObject);
            inGameFloors.RemoveAt(0); // Remove from list at location 0
            }
        }


    }

    /*
public class ChunkSpawner : MonoBehaviour
{

    public Chunk prefab;

    private List<Chunk> chunks = new List<Chunk>();

    void Start()
    {

        for (int i = 0; i < 5; i++)
        {
            Vector3 pos = Vector3.zero;

            if (chunks.Count > 0) {
                Chunk lastChunk = chunks[chunks.Count - 1];
                pos = lastChunk.connectionPoint.position;
            }

            //float y = Random.Range(-2f, 2f);
            Chunk newChunk = Instantiate(prefab, pos, Quaternion.identity);
            chunks.Add(newChunk);
        }
    }

}*/
