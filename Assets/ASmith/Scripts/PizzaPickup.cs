using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASmith
{
    public class PizzaPickup : OverlapObject
    {
        public float score = 0;
        public GameObject pickup;

        public override void OnOverlap(PlayerMovement pm)
        {
            AddScore();
        }

        private void AddScore()
        {
            score += 100;
            print("MAMMA MIA " + score + "pts.");
            SoundEffectBoard.PlayPointPickup(); // plays point audio
            Destroy(pickup);
        }
    }
    
}

