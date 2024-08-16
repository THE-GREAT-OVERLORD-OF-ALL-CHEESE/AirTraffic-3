using CheeseMods.AirTraffic3.Behaviours;
using System.Collections.Generic;
using UnityEngine;

namespace CheeseMods.AirTraffic3.Components
{
    public class Agent_Aircraft : Agent
    {
        public AIAircraftSpawn aircraft;

        public List<Airspace> airspaces = new List<Airspace>();

        public Waypoint waypoint;
        private GameObject waypointObject;

        public float defaultOrbitHeight;
        public float defaultOrbitRadius;

        public float timeLanded = float.NegativeInfinity;
        public float timeTakeoff = float.NegativeInfinity;

        private void Start()
        {
            defaultOrbitHeight = aircraft.aiPilot.defaultAltitude;
            defaultOrbitRadius = aircraft.aiPilot.orbitRadius;

            waypoint = new Waypoint();
            waypointObject = new GameObject();
            waypointObject.AddComponent<FloatingOriginTransform>();
            waypoint.SetTransform(waypointObject.transform);


            behaviours.Add(new Aircraft_TakeOff(this));
            behaviours.Add(new Aircraft_Park(this, Settings.parkingLength));

            behaviours.Add(new Aircraft_Orbit(this, Settings.orbitLength));

            foreach (string airportId in VTScenario.current.GetAllAirportIDs())
            {
                AirportReference reference = new AirportReference(airportId);
                AirportManager airportManager = reference.GetAirport();

                behaviours.Add(new Aircraft_FlyToAirport(this, airportManager));
                behaviours.Add(new Aircraft_LandAtAirport(this, reference));
            }

            aircraft.health.OnDeath.AddListener(OnDeath);
        }

        private void OnDestroy()
        {
            OnDeath();
        }

        private void OnDeath()
        {
            waypoint = null;
            GameObject.Destroy(waypointObject);
            waypointObject = null;

            while (airspaces.Count > 0)
            {
                UnregisterAirspace(airspaces[0]);
            }

            aircraft.health.OnDeath.RemoveListener(OnDeath);
        }

        public void RegisterAirspace(Airspace airspace)
        {
            if (!airspace.aircraft.Contains(this))
            {
                airspace.RegisterAircraft(this);
            }
            if (!airspaces.Contains(airspace))
            {
                airspaces.Add(airspace);
            }
        }

        public void UnregisterAirspace(Airspace airspace)
        {
            if (airspace.aircraft.Contains(this))
            {
                airspace.UnregisterAircraft(this);
            }
            if (airspaces.Contains(airspace))
            {
                airspaces.Remove(airspace);
            }
        }

        public void ReportLanded()
        {
            timeLanded = Time.time;
        }

        public void ReportTakeOff()
        {
            timeTakeoff = Time.time;
        }

        private void OnCollisionEnter()
        {
            //aircraft.health.Kill();
        }
    }
}
