using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Foster
{
    public class SoundEffectBoard : MonoBehaviour
    {
        public static SoundEffectBoard main;

        public AudioClip soundJump;
        public AudioClip soundDeath;

        void Start()
        {
            if(main == null){
                main = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }


        void Update()
        {

        }

        public static void PlayJump(Vector3 pos)
        {
            AudioSource.PlayClipAtPoint(main.soundJump, pos);
        }
    }
}
