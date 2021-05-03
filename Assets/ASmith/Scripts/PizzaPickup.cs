using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASmith
{
    public class PizzaPickup : OverlapObject
    {
        /// <summary>
        /// Score tracker
        /// </summary>
        public float score = 0;

        /// <summary>
        /// Creates a reference for the GameObject in the inspector
        /// </summary>
        public GameObject pickup;

        public override void OnOverlap(PlayerMovement pm)
        {
            AddScore(); // Do AddScore method
        }

        private void AddScore()
        {
            score += 100; // Adds 100pts to score
            SoundEffectBoard.PlayPointPickup(); // plays point audio
            Destroy(pickup); // destroys the game object on overlap
        }
    }    
}

