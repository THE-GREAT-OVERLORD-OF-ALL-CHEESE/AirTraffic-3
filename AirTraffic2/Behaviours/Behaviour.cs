using CheeseMods.AirTraffic3.Components;

namespace CheeseMods.AirTraffic3.Behaviours
{
    public enum BehaviourState
    {
        Running,
        Success,
        Failed
    }

    public class Behaviour
    {
        public Agent agent;

        public Behaviour(Agent agent)
        {
            this.agent = agent;
        }

        public virtual bool CanBegin()
        {
            return false;
        }

        public virtual void Start()
        {

        }

        public virtual BehaviourState Update()
        {
            return BehaviourState.Failed;
        }

        public virtual void End()
        {

        }
    }
}
