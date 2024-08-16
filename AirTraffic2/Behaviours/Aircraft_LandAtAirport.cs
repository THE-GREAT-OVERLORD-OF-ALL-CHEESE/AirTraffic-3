using CheeseMods.AirTraffic3.Components;
using UnityEngine;

namespace CheeseMods.AirTraffic3.Behaviours
{
    public class Aircraft_LandAtAirport : Behaviour_Aircraft
    {
        public AirportManager airport;
        public AirportHelper helper;
        public AirportReference refrence;

        public Aircraft_LandAtAirport(Agent agent, AirportReference reference) : base(agent)
        {
            this.refrence = reference;
            airport = reference.GetAirport();
            helper = AirportHelper.airportToHelper[airport];
        }

        public override bool CanBegin()
        {
            return aircraft.aircraft.aiPilot.commandState == AIPilot.CommandStates.Orbit
                && Time.time - aircraft.timeTakeoff > Settings.minFlyingTime * 60f
                && CloseToAirport()
                && helper.taxiway.HasSpace(aircraft);
        }

        public override void Start()
        {
            base.Start();

            aircraft.RegisterAirspace(helper.taxiway);
            aircraft.aircraft.Land(refrence);
            Debug.Log($"It has been {Time.time - aircraft.timeTakeoff} secconds since we last took off, landing!");
        }

        public override BehaviourState Update()
        {
            if (aircraft.aircraft.aiPilot.commandState == AIPilot.CommandStates.Orbit)
            {
                return BehaviourState.Failed;
            }

            if (aircraft.aircraft.aiPilot.commandState != AIPilot.CommandStates.Park)
            {
                return BehaviourState.Running;
            }

            aircraft.UnregisterAirspace(GlobalAirspace.instance.airspace);
            aircraft.ReportLanded();
            return BehaviourState.Success;
        }

        public override void End()
        {
            base.End();

            aircraft.UnregisterAirspace(helper.taxiway);
        }

        private bool CloseToAirport()
        {
            Vector3 offset = aircraft.aircraft.aiPilot.transform.position - airport.transform.position;
            offset.y = 0;
            return offset.magnitude < 5000f;
        }
    }
}
