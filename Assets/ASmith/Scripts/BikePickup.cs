using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASmith
{
    public class BikePickup : OverlapObject
    {
        //public CameraAutoScroll camScroll;

        /// <summary>
        /// Creates a reference for the GameObject in the inspector
        /// </summary>
        public GameObject pickup;

        /// <summary>
        /// Cooldown for the BikePickup for how long player should have speed boost
        /// </summary>
        private float cooldownBikePickup = 0;

        private void Start()
        {
            //camScroll = GetComponent<CameraAutoScroll>();
        }

        private void Update()
        {
            if (cooldownBikePickup > 0)
            {
                cooldownBikePickup -= Time.deltaTime; // if cooldownBikePickup still has time life, countdown timer
            }
        }

        // If overlapping with object...
        public override void OnOverlap(PlayerMovement pm)
        {
            PowerUp(); // Do PowerUp method
        }

        public void PowerUp()
        {
            print("Now we're movin!");
            ////camScroll.isPickedUp = true; // tells CameraAutoScroll.cs that the bike is picked up
            cooldownBikePickup = 1; // sets cooldown
            ////SoundEffectBoard.PlayPointPickup(); // plays bike audio
            Destroy(pickup); // destroys the game object on overlap
        }
    }
}

