using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelsomeno
{

    public class ChunkSpawner2 : MonoBehaviour
    {
        public Transform prefab;


        void Start()
        {


            for (int i = 0; i < 2; i++)
            {
                // this will randomize how far the chunks can spawn the y and x axises
                float y = Random.Range(35, 45f);
                float x = Random.Range(30, 75f);
                Instantiate(prefab, new Vector3(i * 100, y, 0), Quaternion.identity);



            }

        }

        void Update()
        {

        }
    }
}
