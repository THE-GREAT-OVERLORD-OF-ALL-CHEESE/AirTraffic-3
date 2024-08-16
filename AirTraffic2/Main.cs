using CheeseMods.AirTraffic3.Components;
using ModLoader.Framework;
using ModLoader.Framework.Attributes;
using UnityEngine;

namespace CheeseMods.AirTraffic3
{
    [ItemId("cheese.airtraffic3")]
    public class Main : VtolMod
    {
        public void Awake()
        {
            Debug.Log("Cheese's Air Traffic 3: Loaded!");

            gameObject.AddComponent<AgentManager>();
            gameObject.AddComponent<GlobalAirspace>();
        }

        public override void UnLoad()
        {
            Debug.Log("Cheese's Air Traffic 3: Nothing to unload!");
        }
    }
}