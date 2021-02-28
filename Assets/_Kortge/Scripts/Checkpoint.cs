using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kortge {
    public class Checkpoint : MonoBehaviour
    {
        public GameObject cam;
        public GameObject player;
        public PlayerMovement movement;

        // Start is called before the first frame update
        void Start()
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera");
            player = GameObject.FindGameObjectWithTag("Player");
            movement = GetComponent<PlayerMovement>();
        }

        // Update is called once per frame
        void Update()
        {
            if (transform.position.x - cam.transform.position.x < 11.3f)
            {
                movement.checkpoint = transform.position;
            }
        }
    }
}