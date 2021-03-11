using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Defines the general rules of this level.
/// </summary>
namespace Kortge
{
    public class Zone : SlipstreamJumper.Zone
    {

        new public static ZoneInfo info = new ZoneInfo() // Defines level information that can be seen in the main menu.
        {
            zoneName = "The Binding of Meat Boy",
            creator = "Tyler Kortge",
            sceneFile = "ZoneKortge"
        };

        /// <summary>
        /// singleton:
        /// </summary>
        public static Zone main;

        /// <summary>
        /// The player that collision is checked for.
        /// </summary>
        public AABB player;

        /// <summary>
        /// Physical objects the player cannot pass through.
        /// </summary>
        public List<AABB> platforms = new List<AABB>();

        /// <summary>
        /// All objects the player can overlap and interact with.
        /// </summary>
        public List<AABB> powerups = new List<AABB>();

        private void Awake()
        {
            if(main != null)
            {
                // singleton already exists...
                Destroy(gameObject);
            }
            else
            {
                main = this; // This is the main zone object.
            }
        }
        private void OnDestroy()
        {
            if (main == this) main = null; // The zone object ceases to exist if it is destroyed.
        }

        // Update is called once per frame
        void LateUpdate() // Checks for collision between player and other objects.
        {
            if (!player) return;
            PlayerMovement pm = player.GetComponent<PlayerMovement>();

            // checking collision between PLAYER and all PLATFORMS:
            foreach(AABB box in platforms)
            {
                if (player.OverlapCheck(box))
                {
                    pm.ApplyFix(player.FindFix(box));
                }
            }

            // checking collision between PLAYER and all OVERLAP-OBJECTS
            foreach (AABB power in powerups)
            {
                if (player.OverlapCheck(power))
                {

                    // Player collides with something!
                    // Do something...
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