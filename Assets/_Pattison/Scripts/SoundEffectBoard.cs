using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pattison {
    public class SoundEffectBoard : MonoBehaviour {

        /// <summary>
        /// Singleton!
        /// </summary>
        public static SoundEffectBoard main;

        public AudioClip soundJump;
        public AudioClip soundShoot;
        public AudioClip soundDie;
        public AudioClip soundPickupCoin;

        private AudioSource player;

        // Start is called before the first frame update
        void Start() {
            if(main == null) {
                main = this;
                player = GetComponent<AudioSource>();
            } else {
                Destroy(this.gameObject);
            }
        }

        public static void PlayJump(Vector3 pos) {
            AudioSource.PlayClipAtPoint(main.soundJump, pos);
        }
        public static void PlayJump2() {
            main.player.PlayOneShot(main.soundJump);
        }
    }
}