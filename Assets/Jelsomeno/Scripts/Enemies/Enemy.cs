using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelsomeno
{
    public class Enemy : MonoBehaviour
    {
        public GameObject player;
        private Transform playerPos;
        private Vector2 currentPos;
        public float distance;
        public float speed;

        void Start()
        {
            playerPos = player.GetComponent<Transform>();
            currentPos = GetComponent<Transform>().position;

        }

        void Update()
        {
            if (Vector2.Distance(transform.position, playerPos.position) < distance)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
            }
            else
            {
                if(Vector2.Distance(transform.position, currentPos) <= 0)
                {

                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, currentPos, speed * Time.deltaTime);
                    
                }
            }

        }
    }
}
