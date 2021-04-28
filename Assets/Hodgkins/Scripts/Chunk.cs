using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hodgkins
{
    public class Chunk : MonoBehaviour
    {
        public Transform connectionPoint; // chunk prefabs have this script to reference 
                                          // its connection point in ChunkSpawner
    }
}