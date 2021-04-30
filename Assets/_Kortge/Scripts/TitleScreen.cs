using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Kortge {
    public class TitleScreen : MonoBehaviour
    {
        private float time = 0;
        private RawImage rawImage;
        public CameraAutoScroll cam;
        // Start is called before the first frame update
        void Start()
        {
            rawImage = GetComponent<RawImage>();
        }

        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime;
            if (time >= 3)
            {
                cam.enabled = true;
                Destroy(gameObject);
            }
            else if (time < 3) rawImage.color = Color.Lerp(Color.white, Color.clear, (time - 2.5f)/0.5f);
            else if (time < 0.5f) rawImage.color = Color.Lerp(Color.black, Color.white, time/0.5f);
        }
    }
}