using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASmith
{
    public class SoundEffectBoard : MonoBehaviour
    {
        /// <summary>
        /// Singleton
        /// </summary>
        public static SoundEffectBoard main;

        public AudioClip soundJump;
        public AudioClip soundDie;
        public AudioClip soundDamage;
        public AudioClip soundPickup;

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

        //public static void PlayJump(Vector3 pos) // still considered 3D sound DOES NOT WORK!!!
        //{
        //    AudioSource.PlayClipAtPoint(main.soundJump, pos); // creates the sound at a required position
        //}

        public static void PlayJump2() // plays jump audio in 2D space
        {
            main.player.PlayOneShot(main.soundJump);
        }
    }
}

