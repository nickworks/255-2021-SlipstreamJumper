using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Velting
{

    public class ZoneScript : SlipstreamJumper.Zone

    {

        new public static ZoneInfo info = new ZoneInfo()
                
        {
            zoneName = "",
            creator = "William Velting",
            sceneFile = "ZoneVelting"
        };

        public static ZoneScript main;

        public AABB player;
        

        private List<AABB> platforms = new List<AABB>();

        public List<AABB> powerups = new List<AABB>();

        // Start is called before the first frame update
        public void Awake()
        {
            if(main != null)
            {
                Destroy(gameObject);
            }
            else
            {
                main = this;
            }
        }
        public void AddPlatform(AABB platform)
        {
            platforms.Add(platform);
        }

        public void RemovePlatform(AABB platform)
        {
            platforms.Remove(platform);
        }
        private void OnDestroy()
        {
            if (main == this) main = null;
        }


        // Update is called once per frame
        void LateUpdate()
        {
            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            print(platforms.Count);

            foreach(AABB box in platforms)
            {
                if(player.OverlapCheck(box))
                {
                    
                    pm.ApplyFix(player.FindFix(box)); 
                }
            }

            foreach(AABB power in powerups)
            {
                if(player.OverlapCheck(power))
                {
                    //player collides with powerup
                    SpringBlock sb = power.GetComponent<SpringBlock>();
                    if(sb)
                    {
                        sb.PlayerHit(pm);
                    }
                }
            }
        }
    }
}
