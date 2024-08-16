using CheeseMods.AirTraffic3.Components;
using UnityEngine;

namespace CheeseMods.AirTraffic3.Behaviours
{
    public class Aircraft_Orbit : Behaviour_Aircraft
    {
        private float waitTime;
        private float orbitStartTime;

        public Aircraft_Orbit(Agent agent, float waitTime) : base(agent)
        {
            this.waitTime = waitTime;
        }

        public override bool CanBegin()
        {
            return aircraft.aircraft.aiPilot.commandState == AIPilot.CommandStates.Orbit;
        }

        public override void Start()
        {
            base.Start();
            orbitStartTime = Time.time;
        }

        public override BehaviourState Update()
        {
            if (Time.time - orbitStartTime < waitTime)
            {
                return BehaviourState.Running;
            }

            return BehaviourState.Success;
        }
    }
}
