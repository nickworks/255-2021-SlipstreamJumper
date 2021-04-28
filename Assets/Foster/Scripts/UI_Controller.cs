using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Foster
{
    public class UI_Controller : MonoBehaviour
    {
        public Scrollbar healthBar;
        

        void Update()
        {
            healthBar.size = (HealthSystem.health / 100);
        }
    }
}