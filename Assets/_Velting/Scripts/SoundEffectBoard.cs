using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Velting
{
    public class SoundEffectBoard : MonoBehaviour
    {
        public static SoundEffectBoard main;

        public AudioClip soundJump;
        public AudioSource soundWalk;
        public AudioSource soundBackground;
        public AudioSource soundPickup;

        private AudioSource player;
        // Start is called before the first frame update
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

        // Update is called once per frame
        void Update()
        {

        }

        public static void PlayJump2()
        {
            main.player.PlayOneShot(main.soundJump);
        }
    }
}