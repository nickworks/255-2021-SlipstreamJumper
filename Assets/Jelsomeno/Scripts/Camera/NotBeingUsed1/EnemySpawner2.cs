using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelsomeno
{

    public class EnemySpawner2 : MonoBehaviour
    {
        public Transform enemy;


        void Start()
        {


            for (int i = 0; i < 10; i++)
            {

                float y = Random.Range(40, 50f);
                Instantiate(enemy, new Vector3(i * 15, y, 0), Quaternion.identity);



            }

        }

        void Update()
        {

        }
    }
}
