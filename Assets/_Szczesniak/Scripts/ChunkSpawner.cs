using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Szczesniak
{
    public class ChunkSpawner : MonoBehaviour {

        public Transform prefab;

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
        /// Creating list to store and manage the floors
        /// </summary>
        private List<Transform> inGameFloors = new List<Transform>();

        void Start() {


            for (int i = 0; i < 5; i++) {
                SpawningFloor();
            }

        }


        void Update() {
            if (transform.position.x - 30 > playerSpawnX - (floorLength * amountOfFloors)) {
                SpawningFloor();
                DestroyFloor();
                print("going over");
            }
        }


        private void SpawningFloor() {
            float y = Random.Range(-2f, 2f);
            Transform floors = Instantiate(prefab, new Vector3(floorID * 25, y, 0), Quaternion.identity);
            inGameFloors.Add(floors);
            playerSpawnX += floorLength;
            floorID++;
        }

        private void DestroyFloor() {
            // Destroy object
            Destroy(inGameFloors[0].gameObject);
            inGameFloors.RemoveAt(0); // Remove from list at location 0
        }


    }
}