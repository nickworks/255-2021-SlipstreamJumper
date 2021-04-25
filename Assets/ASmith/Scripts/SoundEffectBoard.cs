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
        public AudioClip soundJump; // Jump Sound
        public AudioClip soundDoubleJump; // Double Jump Sound
        public AudioClip soundDie; // Death Sound
        public AudioClip soundDamage; // Damage Taken Sound
        public AudioClip soundPointPickup; // Sound when getting points
        public AudioClip soundSpringBlock; // Springblock Sound

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
                Destroy(this.gameObject); // if main doesn't = null, destory the gameObject
            }
        }

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

