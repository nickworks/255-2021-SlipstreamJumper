using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jelsomeno
{

    public class SwitchingScenes : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            SceneManager.LoadScene(2);
        }
    }
}
