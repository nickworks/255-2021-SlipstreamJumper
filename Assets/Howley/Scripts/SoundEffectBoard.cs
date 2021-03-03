using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Howley
{
    public class SoundEffectBoard : MonoBehaviour
    {
        /// <summary>
        /// Set up singleton
        /// </summary>
        public static SoundEffectBoard main;

        public AudioClip soundJump;
        public AudioClip shoot;
        public AudioClip die;
        public AudioClip hitPowerup;

        private AudioSource player;

        void Start()
        {
            if (main == null)
            {
                main = this;
                player = GetComponent<AudioSource>();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        public static void PlayJump()
        {
            main.player.PlayOneShot(main.soundJump);
        }
    }
}

