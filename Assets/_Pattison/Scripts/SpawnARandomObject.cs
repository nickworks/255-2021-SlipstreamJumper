using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnARandomObject : MonoBehaviour
{
    public Transform[] thingsThatCouldSpawn;


    void Start()
    {
        if (Random.Range(0, 100) > 50) return;


        // pick a random thing

        // spawn here
    }
}
