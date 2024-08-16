using CheeseMods.AirTraffic3.Components;
using UnityEngine;

namespace CheeseMods.AirTraffic3.Behaviours
{
    public class Aircraft_Park : Behaviour_Aircraft
    {
        private readonly float waitTime;
        private float parkStartTime;

        public Aircraft_Park(Agent agent, float waitTime) : base(agent)
        {
            this.waitTime = waitTime;
        }

        public override bool CanBegin()
        {
            return aircraft.aircraft.aiPilot.commandState == AIPilot.CommandStates.Park;
        }

        public override void Start()
        {
            base.Start();
            parkStartTime = Time.time;
        }

        public override BehaviourState Update()
        {
            if (Time.time - parkStartTime < waitTime)
            {
                return BehaviourState.Running;
            }

            return BehaviourState.Success;
        }
    }
}
