using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SlipstreamJumper {
    public class GameOverMenu : MonoBehaviour {

        public void BttnBackToMainMenu() {
            SceneManager.LoadScene("MainMenu");
        }
        public void BttnBackToPlaying() {
            SceneManager.LoadScene("SceneSwitcher");
        }
    }
}