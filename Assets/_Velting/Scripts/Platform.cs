using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Velting
{
    [RequireComponent(typeof(AABB))]
    public class Platform : MonoBehaviour
    {

        AABB aabb;
        // Start is called before the first frame update
        void Start()
        {
            aabb = GetComponent<AABB>();

            ZoneScript.main.AddPlatform(aabb);

        }

        // Update is called once per frame
        void OnDestroy()
        {
            if (ZoneScript.main == null)
            {
                return;
            }
            ZoneScript.main.RemovePlatform(aabb);
        }
    }

}
