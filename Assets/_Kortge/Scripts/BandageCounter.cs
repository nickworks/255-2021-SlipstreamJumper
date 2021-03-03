using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kortge
{
    public class BandageCounter : MonoBehaviour // The UI element that keeps track of how many bandages the player had.
    {
        public PlayerMovement player; // The player character that is holding all of the lives.
        private Text text; // The number meant to reflect how many lives the player has.

        // Start is called before the first frame update
        void Start() // Get text.
        {
            text = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            text.text = (player.bandages.ToString() + "/5"); // Updates text with how many bandages the player has.
        }
    }
}