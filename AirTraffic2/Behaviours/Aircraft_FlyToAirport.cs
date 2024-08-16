using CheeseMods.AirTraffic3.Components;
using UnityEngine;

namespace CheeseMods.AirTraffic3.Behaviours
{
    public class Aircraft_FlyToAirport : Behaviour_Aircraft
    {
        public AirportManager airport;

        public Aircraft_FlyToAirport(Agent agent, AirportManager airport) : base(agent)
        {
            this.airport = airport;
        }

        public override bool CanBegin()
        {
            return aircraft.aircraft.aiPilot.commandState == AIPilot.CommandStates.Orbit
                && CloseToAirport() == false;
        }

        public override void Start()
        {
            base.Start();

            aircraft.waypoint.GetTransform().position = airport.transform.position;

            aircraft.aircraft.SetOrbitNow(aircraft.waypoint, 1000f, 1000f);
        }

        public override BehaviourState Update()
        {
            if (CloseToAirport() == false)
            {
                return BehaviourState.Running;
            }

            return BehaviourState.Success;
        }

        public override void End()
        {
            base.End();

            aircraft.aircraft.SetOrbitNow(aircraft.waypoint, aircraft.defaultOrbitHeight, aircraft.defaultOrbitHeight);
        }

        private bool CloseToAirport()
        {
            Vector3 offset = aircraft.aircraft.aiPilot.transform.position - airport.transform.position;
            offset.y = 0;
            return offset.magnitude < 5000f;
        }
    }
}
