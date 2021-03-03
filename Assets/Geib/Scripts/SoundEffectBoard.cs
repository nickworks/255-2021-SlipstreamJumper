using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geib
{
    public class SoundEffectBoard : MonoBehaviour
    {
        /// <summary>
        /// Singleton!
        /// </summary>
        public static SoundEffectBoard main;
        /// <summary>
        /// This stores the audio file for the jumping sound.
        /// </summary>
        public AudioClip soundJump;
        /// <summary>
        /// This stores the audio file for the spring sound.
        /// </summary>
        public AudioClip soundSpring;
        /// <summary>
        /// This stores the audio file for the damage sound.
        /// </summary>
        public AudioClip soundDamage;
        /// <summary>
        /// This stores the audio file for the coin sound.
        /// </summary>
        public AudioClip soundCoin;
        /// <summary>
        /// This stores the audio file for the death sound.
        /// </summary>
        public AudioClip soundDeath;

        private AudioSource player;

        // Start is called before the first frame update
        void Start()
        {
            if (main == null)
            {
                main = this;
                player = GetComponent<AudioSource>();
            } else
            {
                Destroy(this.gameObject);
            }

        }

        public static void PlayJump(Vector3 pos)
        {
            AudioSource.PlayClipAtPoint(main.soundJump, pos);
        }
        public static void PlayJump2()
        {
            main.player.PlayOneShot(main.soundJump);
        }
        public static void PlaySpring()
        {
            main.player.PlayOneShot(main.soundSpring);
        }
        public static void PlayDamage()
        {
            main.player.PlayOneShot(main.soundDamage);
        }
        public static void PlayDeath()
        {
            main.player.PlayOneShot(main.soundDeath);
        }
        public static void PlayCoin()
        {
            main.player.PlayOneShot(main.soundCoin);
        }


    }
}
