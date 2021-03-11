using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// The UI element that keeps track of how many bandages the player had.
/// </summary>
namespace Kortge
{
    public class BandageCounter : MonoBehaviour
    {
        /// <summary>
        /// Decides if a platform can be passed through from the bottom.
        /// </summary>
        public PlayerMovement player;
        /// <summary>
        /// The number meant to reflect how many lives the player has.
        /// </summary>
        private Text text;

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