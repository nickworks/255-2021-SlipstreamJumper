using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Szczesniak {

    /// <summary>
    /// This Class's purpose is to manage the scores of the coins and distance
    /// </summary>
    public class ScoreSystem : MonoBehaviour {
        
        /// <summary>
        /// Holds the score amount for distance
        /// </summary>
        public float scoreAmt = 0;

        /// <summary>
        /// The start position the player is at
        /// </summary>
        private Vector3 startPos;

        /// <summary>
        /// The text for the distance score
        /// </summary>
        public Text scoreText;

        /// <summary>
        /// The text for the coin score
        /// </summary>
        public Text coinText;

        /// <summary>
        /// The total distance traveled
        /// </summary>
        private float totalTravel = 0;

        /// <summary>
        /// Coins that are collected
        /// </summary>
        public int coinsCollected = 0;

        private void Start() {
            // Assigning the start posiion of where the player is at
            startPos = transform.position; 
        }

        void Update() {
            // if the player goes backwards then the player won't have points being reversed
            if (totalTravel <= transform.position.x) {
                scoreAmt = Mathf.RoundToInt(Mathf.Abs(transform.position.x - startPos.x));
                totalTravel++;
            }
            
            // displays the distance score
            scoreText.text = "Distance: " + scoreAmt + "m";

            // displays the coins score
            coinText.text = "Coins: " + coinsCollected;
        }
    }
}