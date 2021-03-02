using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Szczesniak
{
    /// <summary>
    /// This class is used to have a point to where the Level Chunk should spawn at
    /// </summary>
    public class Chunk : MonoBehaviour
    {
        /// <summary>
        /// The point where the next level chunk to spawn at
        /// </summary>
        public Transform connectionPoint; 
    }
}