using CheeseMods.AirTraffic3.Components;
using UnityEngine;

namespace CheeseMods.AirTraffic3.Behaviours
{
    public class Aircraft_TakeOff : Behaviour_Aircraft
    {
        public AirportManager closestAirport;
        public AirportHelper airportHelper;

        public Aircraft_TakeOff(Agent agent) : base(agent)
        {
        }

        public override bool CanBegin()
        {
            if (aircraft.aircraft.aiPilot.commandState != AIPilot.CommandStates.Park
                || Time.time - aircraft.timeLanded < Settings.minParkedTime * 60f
                || !GlobalAirspace.instance.airspace.HasSpace(aircraft))
                return false;

            closestAirport = GetClosestAirport();
            AirportHelper.airportToHelper.TryGetValue(closestAirport, out airportHelper);

            return closestAirport == null
                || AirportHelper.airportToHelper[closestAirport].taxiway.HasSpace(aircraft);
        }

        public override void Start()
        {
            base.Start();
            aircraft.aircraft.TakeOff();


            aircraft.RegisterAirspace(GlobalAirspace.instance.airspace);

            closestAirport = GetClosestAirport();
            AirportHelper.airportToHelper.TryGetValue(closestAirport, out airportHelper);
            if (airportHelper != null)
            {
                aircraft.RegisterAirspace(airportHelper.taxiway);
            }

            Debug.Log($"It has been {Time.time - aircraft.timeLanded} secconds since we landed, taking off!");
        }

        public override BehaviourState Update()
        {
            if (aircraft.aircraft.aiPilot.commandState != AIPilot.CommandStates.Orbit)
            {
                return BehaviourState.Running;
            }

            return BehaviourState.Success;
        }

        public override void End()
        {
            base.End();
            
            if (airportHelper != null)
            {
                aircraft.UnregisterAirspace(airportHelper.taxiway);
            }

            aircraft.ReportTakeOff();
        }

        private AirportManager GetClosestAirport()
        {
            AirportManager closestAirport = null;
            float num = float.MaxValue;
            foreach (AirportManager airportManager in VTScenario.current.GetAllAirports())
            {
                float sqrMagnitude = (airportManager.transform.position - aircraft.aircraft.transform.position).sqrMagnitude;
                if (sqrMagnitude < num)
                {
                    num = sqrMagnitude;
                    closestAirport = airportManager;
                }
            }

            return closestAirport;
        }
    }
}
