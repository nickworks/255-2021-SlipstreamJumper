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

        /// <summary>
        /// AudioClips for all available sounds in the game 
        /// </summary>
        public AudioClip soundJump;
        public AudioClip soundDoubleJump;
        public AudioClip soundDie;
        public AudioClip soundDamage;
        public AudioClip soundPointPickup;
        public AudioClip soundSpringBlock;

        /// <summary>
        /// Property for the AudioSource used to play AudioClips
        /// </summary>
        public AudioSource player;

        void Start()
        {
            if (main == null)
            {
                main = this;
                player = GetComponent<AudioSource>(); // Gets a reference to the AudioSource
            }
            else
            {
                Destroy(this.gameObject);
            }
            //PlayMusic(); // Originally used to play the background music before it was given its' own AudioSource in the Inspector
        }

        //public static void PlayJump(Vector3 pos) // still considered 3D sound DOES NOT WORK!!!
        //{
        //    AudioSource.PlayClipAtPoint(main.soundJump, pos); // creates the sound at a required position
        //}

        public static void PlayJump2() // plays jump audio in 2D space
        {
            main.player.PlayOneShot(main.soundJump);
        }

        public static void PlayDoubleJump() // plays double jump audio
        {
            main.player.PlayOneShot(main.soundDoubleJump);
        }

        public static void PlayDamage() // plays damage audio
        {
            main.player.PlayOneShot(main.soundDamage);
        }

        public static void PlayDie() // plays death audio
        {
            main.player.PlayOneShot(main.soundDie);
        }

        public static void PlaySpringBlock() // plays springblock audio
        {
            main.player.PlayOneShot(main.soundSpringBlock);
        }

        public static void PlayPointPickup() // plays point pickup audio
        {
            main.player.PlayOneShot(main.soundPointPickup);
        }
    }
}

