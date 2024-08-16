using CheeseMods.AirTraffic3.Components;
using System.Collections.Generic;
using UnityEngine;

namespace CheeseMods.AirTraffic3
{
    public class Airspace
    {
        public string airspaceName;
        public List<Agent_Aircraft> aircraft = new List<Agent_Aircraft>();
        public int maxAircraft;

        public Airspace(string airspaceName, int maxAircraft)
        {
            this.airspaceName = airspaceName;
            this.maxAircraft = maxAircraft;
        }

        public void RegisterAircraft(Agent_Aircraft aircraft)
        {
            if (!this.aircraft.Contains(aircraft))
            {
                this.aircraft.Add(aircraft);
                Debug.Log($"There are now {this.aircraft.Count}/{maxAircraft} in the {airspaceName} airspace.");
            }
        }

        public void UnregisterAircraft(Agent_Aircraft aircraft)
        {
            if (this.aircraft.Contains(aircraft))
            {
                this.aircraft.Remove(aircraft);
                Debug.Log($"There are now {this.aircraft.Count}/{maxAircraft} in the {airspaceName} airspace.");
            }
        }

        public bool HasSpace(Agent_Aircraft aircraft)
        {
            if (this.aircraft.Contains(aircraft))
            {
                return true;
            }
            else
            {
                return maxAircraft - this.aircraft.Count > 0;
            }
        }
    }
}
