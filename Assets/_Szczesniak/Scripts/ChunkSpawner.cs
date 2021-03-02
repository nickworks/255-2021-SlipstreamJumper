using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Szczesniak
{
    /// <summary>
    /// This script manages chunks so that we always have 5.
    /// </summary>
    public class ChunkSpawner : MonoBehaviour
    {

        public List<Transform> prefab = new List<Transform>();
        public Transform boasterPrefab;

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
        private List<Transform> boastersInGame = new List<Transform>();

        /// <summary>
        /// This will increment to know when to spawn the boaster box
        /// </summary>
        private float boasterSpawnNum = 0;

        float randomNumBoaster = 1;

        void Start()
        {


            for (int i = 0; i < 5; i++)
            {
                SpawningFloor();
            }

        }


        void Update()
        {
            if (transform.position.x - 50 > playerSpawnX - (floorLength * amountOfFloors))
            {
                SpawningFloor();
                DestroyFloor();
                print("going over");
            }
        }


        private void SpawningFloor()
        {
            int y = Random.Range(0, prefab.Count);
            Transform floors = Instantiate(prefab[y], new Vector3(floorID * 25, 0, 0), Quaternion.identity);
            inGameFloors.Add(floors);


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
            playerSpawnX += floorLength;
            floorID++;
            
        }

        private void DestroyFloor()
        {
            // Destroy object
            Destroy(inGameFloors[0].gameObject);
            inGameFloors.RemoveAt(0); // Remove from list at location 0

            // If the amount of boasters is greater than or equal to 5 in game
            if (boastersInGame.Count >= 5)
            {
                Destroy(boastersInGame[0].gameObject);
                boastersInGame.RemoveAt(0);
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
}