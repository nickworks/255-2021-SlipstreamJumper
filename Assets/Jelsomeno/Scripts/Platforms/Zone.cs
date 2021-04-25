using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelsomeno
{

    public class Zone : SlipstreamJumper.Zone
    {

        new public static ZoneInfo info = new ZoneInfo()
        {
            zoneName = " Zone Rocco",
            creator = "Rocco Jelsomeno",
            sceneFile = "ZoneJelsomeno"
        };

        //singleton
        public static Zone main;

        public AABB player;

        // one variable to hold all of the platforms 
        private List<AABB> platforms = new List<AABB>();

        // one variable to hold all of the powerups (did not use)
        public List<AABB> powerups = new List<AABB>();


        // only runs once
        private void Awake()
        {
            if(main != null) // singleton already exist
            {
                Destroy(gameObject);

            }
            else
            {
                main = this;
            }
        }

        // if destroyed
        private void OnDestroy()
        {
            if (main == this) main = null;
        }

        /// <summary>
        /// updates the last and compares to the other updates
        /// </summary>
        void LateUpdate()
        {
            //if (!player) return; // no player, do not do collision detection

            PlayerMovement pm = player.GetComponent<PlayerMovement>();

            // checking collision between and Player and the platforms
            foreach(AABB box in platforms)
            {
                if (player.OverlapCheck(box))
                {
                    // move player out of the platf
                    pm.ApplyFix(player.FindFix(box));
                }
            }

            // checking collsion with players and any of the overlap objects
            foreach(AABB power in powerups)
            {
                if (player.OverlapCheck(power))
                {
                    //player collides with powerup!
                    // do something
                    SpringBlock sb = power.GetComponent<SpringBlock>();
                    if (sb)
                    {
                        sb.PlayerHit(pm);
                    }

                    OverlapObject oo = power.GetComponent<OverlapObject>();

                    if (oo)
                    {
                        oo.OnOverlap(pm);
                    }


                }
            }


        }


        /// <summary>
        /// This function adds a platform to our big list of platforms.
        /// </summary>
        /// <param name="platform"></param>
        public void AddPlatform(AABB platform)
        {
            platforms.Add(platform);

        }

        /// <summary>
        /// This function removes a platform from our big list of platforms.
        /// </summary>
        /// <param name="platform"></param>
        public void RemovePlatform(AABB platform)
        {
            platforms.Remove(platform);
        }
     
           
       

    }
}
