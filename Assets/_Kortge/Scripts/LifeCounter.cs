using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// The UI element that keeps track of how many lives the player had.
/// </summary>
namespace Kortge
{
    public class LifeCounter : MonoBehaviour
    {
        /// <summary>
        /// The player character that is holding all of the lives.
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
            text.text = player.lives.ToString(); // Updates text with how many lives the player has.
        }
    }
}