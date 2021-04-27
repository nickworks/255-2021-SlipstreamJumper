using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hodgkins
{       
    public class SoundEffectBoard : MonoBehaviour
    {
        public static SoundEffectBoard main;
        /// <summary>
        /// plug ins for all audio sources in editor
        /// </summary>
        public AudioClip soundJump;
        public AudioClip soundSpring;
        public AudioClip soundHit;
        public AudioClip soundDie;
        public AudioClip soundPowerup;

        private AudioSource player;

        void Start()
        {
            if(main == null)
            {
                main = this;
                player = GetComponent<AudioSource>(); // makes sure the object is an AudioSource
            } else {
                Destroy(this.gameObject); // if not, destroy it
            }
        }
        /// <summary>
        /// All sound functions for use in other scripts
        /// </summary>
        public static void PlayJump()
        {
            main.player.PlayOneShot(main.soundJump);
        }

        public static void PlaySpring()
        {
            main.player.PlayOneShot(main.soundSpring);
        }

        public static void PlayHit()
        {
            main.player.PlayOneShot(main.soundHit);
        }

        public static void PlayDie()
        {
            main.player.PlayOneShot(main.soundDie);
        }
        public static void PlayPowerup()
        {
            main.player.PlayOneShot(main.soundPowerup);
        }
    }
}