using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Szczesniak {
    public class ScoreSystem : MonoBehaviour {
        
        public float scoreAmt = 0;
        private Vector3 startPos;

        public Text scoreText;
        public Text coinText;

        private float totalTravel = 0;

        public int coinsCollected = 0;

        private void Start() {
            startPos = transform.position;
        }

        void Update() {
            if (totalTravel <= transform.position.x) {
                scoreAmt = Mathf.RoundToInt(Mathf.Abs(transform.position.x - startPos.x));
                totalTravel++;
            }
            scoreText.text = " " + scoreAmt;

            coinText.text = " " + coinsCollected;
        }
    }
}