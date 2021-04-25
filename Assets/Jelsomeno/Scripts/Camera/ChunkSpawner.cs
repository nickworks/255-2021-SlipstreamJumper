using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelsomeno
{
    /// <summary>
    /// this randomly generats the prefabs I created 
    /// </summary>
    public class ChunkSpawner : MonoBehaviour
    {
        /// <summary>
        /// hold the prefab chuncks in the inspector
        /// </summary>
        public List<Transform> prefab = new List<Transform>();

        /// <summary>
        /// starts the player on the x-axis
        /// </summary>
        private float playerSpawn = 0;

        /// <summary>
        /// how mnay prefabs can be generated at a time
        /// </summary>
        private float totalPrefabs = 5;

        /// <summary>
        /// length of the prefabs x-asix, each one is around 35 in length
        /// </summary>
        private float floorLength = 35;

        /// <summary>
        /// helps to calculate the distance with the Instantiate Function
        /// </summary>
        private float prefabID = 0;

        /// <summary>
        /// manaages the prefabs
        /// </summary>
        private List<Transform> inGamePrefabs = new List<Transform>();


        void Start()
        {
            // spawns the prefabs at the start
            for (int i = 0; i < 5; i++)
            {
                SpawnPrefab();
            }

        }

        void Update()
        {
            // checking for where the players position is so the prefabs can be deleted after the player has passed them
            if(transform.position.x - 50 > playerSpawn - (floorLength * totalPrefabs))
            {
                SpawnPrefab();
                DestroyPrefab();
            }
        }

        /// <summary>
        /// spawns the prefabs 
        /// </summary>
        private void SpawnPrefab()
        {
            // randomly decideds which prefab number in the list it will spawn
            int y = Random.Range(0, prefab.Count);

            //instantiantes the prefab
            Transform prefabs = Instantiate(prefab[y], new Vector3(prefabID * 35, 0, 0), Quaternion.identity);
            inGamePrefabs.Add(prefabs);// adds prefab to list


            playerSpawn += floorLength;// player location and floor length working together
            prefabID++; // spawn number of the prefab in increments

        }

        /// <summary>
        /// destroys the prefabs after a certain point to make sure the game is running smoother
        /// </summary>
        private void DestroyPrefab()
        {
            //destroys the last prefab the player was on
            Destroy(inGamePrefabs[0].gameObject);
            inGamePrefabs.RemoveAt(0);//at location zero then it is removed from list 
        
        }
    }
}

