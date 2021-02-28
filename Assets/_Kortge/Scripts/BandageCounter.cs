using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kortge
{
    public class BandageCounter : MonoBehaviour
    {
        public PlayerMovement player;
        private Text text;

        // Start is called before the first frame update
        void Start()
        {
            text = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            text.text = (player.bandages.ToString() + "/5");
        }
    }
}