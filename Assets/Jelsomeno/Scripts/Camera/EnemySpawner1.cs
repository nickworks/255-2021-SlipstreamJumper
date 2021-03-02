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


            for (int i = 0; i < 4; i++)
            {

                float y = Random.Range(25, 40f);
                Instantiate(enemy, new Vector3(i * 30, y, 0), Quaternion.identity);



            }

        }

        void Update()
        {

        }
    }
}
