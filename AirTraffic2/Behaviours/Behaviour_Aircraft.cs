using CheeseMods.AirTraffic3.Components;

namespace CheeseMods.AirTraffic3.Behaviours
{
    public class Behaviour_Aircraft : Behaviour
    {
        public Agent_Aircraft aircraft;

        public Behaviour_Aircraft(Agent agent) : base(agent)
        {
            aircraft = agent as Agent_Aircraft;
        }
    }
}
