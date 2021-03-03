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

        public List<AABB> hazards = new List<AABB>();

        public List<AABB> oneWayPlatforms = new List<AABB>();

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

        public void AddOneWayPlatform(AABB oneWayPlatform)
        {
            oneWayPlatforms.Add(oneWayPlatform);
        }

        public void RemoveOneWayPlatform(AABB oneWayPlatform)
        {
            oneWayPlatforms.Remove(oneWayPlatform);
        }
        private void OnDestroy()
        {
            if (main == this) main = null;
        }


        // Update is called once per frame
        void LateUpdate()
        {
            if (!player) return; //no player don't do collision detection

            PlayerMovement pm = player.GetComponent<PlayerMovement>();

            //checking collision between PLAYER and all PLATFORMS:
            foreach(AABB box in platforms)
            {
                if(player.OverlapCheck(box))
                {
                    
                    pm.ApplyFix(player.FindFix(box)); 
                }
            }

            //checking collision between PLAYER and all OVERLAP-OBJECTS
            foreach(AABB power in powerups)
            {
                if(player.OverlapCheck(power))
                {
                    //player collides with powerup

                    OverlapObject oo = power.GetComponent<OverlapObject>();

                    if(oo)
                    {
                        oo.OnOverlap(pm);
                    }

                    

                }

                foreach (AABB oneWay in oneWayPlatforms)
                {
                    if(player.OverlapCheck(oneWay))
                    {
                        pm.ApplyOneWay(player.FindOneWay(oneWay));
                    }
                }
            }
        }
    }
}
