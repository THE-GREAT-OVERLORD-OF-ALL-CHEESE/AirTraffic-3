using System.Collections.Generic;
using UnityEngine;

namespace CheeseMods.AirTraffic3.Components
{
    public class AirportHelper : MonoBehaviour
    {
        public static Dictionary<AirportManager, AirportHelper> airportToHelper
            = new Dictionary<AirportManager, AirportHelper>();

        public AirportManager airport;

        public Airspace taxiway = new Airspace("Taxiway", 2);

        private void OnEnable()
        {
            airport = GetComponent<AirportManager>();

            airportToHelper.Add(airport, this);
            taxiway.airspaceName = $"{airport.airportName} taxiway";
        }

        private void OnDisable()
        {
            airportToHelper.Remove(airport);
        }
    }
}
