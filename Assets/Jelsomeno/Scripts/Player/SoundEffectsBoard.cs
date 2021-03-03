using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelsomeno
{



    public class SoundEffectsBoard : MonoBehaviour
    {
        /// <summary>
        /// Singleton
        /// </summary>
        public static SoundEffectsBoard main;

        public AudioClip Jump;
        public AudioClip Shoot;
        public AudioClip pickUp;

        // Start is called before the first frame update
        void Start()
        {
            if(main == null)
            {
                main = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public static void PlayJump(Vector3 pos)
        {
            AudioSource.PlayClipAtPoint(main.Jump, pos);
        }


    }
}
