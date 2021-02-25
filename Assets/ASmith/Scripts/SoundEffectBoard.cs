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
        public AudioClip soundDoubleJump;
        public AudioClip soundDie;
        public AudioClip soundDamage;
        public AudioClip soundPickup;
        public AudioClip soundMusic;

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
            PlayMusic();
        }

        //public static void PlayJump(Vector3 pos) // still considered 3D sound DOES NOT WORK!!!
        //{
        //    AudioSource.PlayClipAtPoint(main.soundJump, pos); // creates the sound at a required position
        //}

        public static void PlayJump2() // plays jump audio in 2D space
        {
            main.player.PlayOneShot(main.soundJump);
        }

        public static void PlayDoubleJump()
        {
            main.player.PlayOneShot(main.soundDoubleJump);
        }

        public static void PlayDamage()
        {
            main.player.PlayOneShot(main.soundDamage);
        }

        public static void PlayDie()
        {
            main.player.PlayOneShot(main.soundDie);
        }

        public static void PlayMusic()
        {
            main.player.PlayOneShot(main.soundMusic);
        }
    }
}

