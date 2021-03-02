using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Szczesniak {
    public class CoinScoreManager : OverlapObject {
        public override void OnOverlap(PlayerMovement pm) {
            ScoreSystem coinScore = pm.GetComponent<ScoreSystem>();
            print("false");
            if (coinScore) {
                print("True");
                coinScore.coinsCollected += 5;

                Destroy(gameObject);
            }
        }
    }
}