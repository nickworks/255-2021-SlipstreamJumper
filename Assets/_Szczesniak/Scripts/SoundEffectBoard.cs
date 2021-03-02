using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Szczesniak {
    
    /// <summary>
    /// This class if for when the player does certain action it will play sound files
    /// </summary>
    public class SoundEffectBoard : MonoBehaviour {

        /// <summary>
        /// This is a Singleton!
        /// </summary>
        public static SoundEffectBoard main;

        /// <summary>
        /// When the player jumps
        /// </summary>
        public AudioClip soundJump;

        /// <summary>
        /// Sound when the player dies
        /// </summary>
        public AudioClip soundDie;

        /// <summary>
        /// Sound when the plaeyr picks up coins
        /// </summary>
        public AudioClip pickupCoin;

        /// <summary>
        /// Creating audio source to play sounds on
        /// </summary>
        private AudioSource player;

        // Start is called before the first frame update
        void Start() {

            if (main == null) {
                main = this;
                player = GetComponent<AudioSource>();
            } else {
                Destroy(this.gameObject);
            }

        }

        /// <summary>
        /// Plays when the player jumps at a specific point in the world
        /// </summary>
        /// <param name="pos"></param>
        public static void PlayJump(Vector3 pos) {
            AudioSource.PlayClipAtPoint(main.soundJump, pos);
        }

        /// <summary>
        /// Plays when the player jumps
        /// </summary>
        public static void PlayJump2() {
            main.player.PlayOneShot(main.soundJump);
        }
    }
}