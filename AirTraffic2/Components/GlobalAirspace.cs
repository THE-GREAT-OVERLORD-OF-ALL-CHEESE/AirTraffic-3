using UnityEngine;

namespace CheeseMods.AirTraffic3.Components
{
    public class GlobalAirspace : MonoBehaviour
    {
        public static GlobalAirspace instance;

        public Airspace airspace = new Airspace("Global", Settings.maxAirborneAircraft);

        private void Awake()
        {
            instance = this;
        }
    }
}
