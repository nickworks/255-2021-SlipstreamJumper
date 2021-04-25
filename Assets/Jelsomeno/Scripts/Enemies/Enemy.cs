using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelsomeno
{
    /// <summary>
    /// this is the class for the enemy that follows the player
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        /// <summary>
        /// can assign the object you want the enemy to chase in the inspector
        /// </summary>
        public GameObject player;

        /// <summary>
        /// gets the assiged objects postions
        /// </summary>
        private Transform playerPos;

        /// <summary>
        /// gets the assigned objects current positions
        /// </summary>
        private Vector2 currentPos;
        
        /// <summary>
        /// distance the player is away from enemy
        /// </summary>
        public float distance;

        /// <summary>
        /// how fast the enemy can move, can be changed in the inspector
        /// </summary>
        public float speed;

        void Start()
        {
            playerPos = player.GetComponent<Transform>();// find where the player is first
            currentPos = GetComponent<Transform>().position;//gets the players current postion

        }

        /// <summary>
        /// during the update it is checking to see if the player is in range
        /// </summary>
        void Update()
        {
            //checks if the players is within range
            if (Vector2.Distance(transform.position, playerPos.position) < distance)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);// moves toward the player if they are in range 
            }
            else
            {
                // if not in range do not go after player
                if(Vector2.Distance(transform.position, currentPos) <= 0)
                {

                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, currentPos, speed * Time.deltaTime);// recalcultes to get the players current position if they have moved
                    
                }
            }

        }
    }
}
