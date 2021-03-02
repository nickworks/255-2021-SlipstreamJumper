using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Szczesniak {

    /// <summary>
    /// This Class's purpose is to add coin score and destroy the coin game object
    /// </summary>
    public class CoinScoreManager : OverlapObject {

        /// <summary>
        /// When the player overlaps with the coins in the game:
        /// </summary>
        /// <param name="pm"></param>
        public override void OnOverlap(PlayerMovement pm) {
            ScoreSystem coinScore = pm.GetComponent<ScoreSystem>();

            // If the coinScore is stored
            if (coinScore) {
                coinScore.coinsCollected += 5; // add 5 points to the coinsCollected

                Destroy(gameObject); // Destroy the coin game object
            }
        }
    }
}