using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelsomeno
{

    public class EnemySpawner1 : MonoBehaviour
    {
        public Transform enemy;


        void Start()
        {


            for (int i = 0; i < 5; i++)
            {
                // this will randomize how far the chunks can spawn the y and x axises
                float y = Random.Range(30, 50f);
                float x = Random.Range(25, 55f);
                Instantiate(enemy, new Vector3(i * 30, y, 0), Quaternion.identity);



            }

        }

        void Update()
        {

        }
    }
}
