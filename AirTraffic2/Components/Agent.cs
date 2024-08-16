using CheeseMods.AirTraffic3.Behaviours;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CheeseMods.AirTraffic3.Components
{
    public class Agent : MonoBehaviour
    {
        public List<Behaviours.Behaviour> behaviours = new List<Behaviours.Behaviour>();
        public Behaviours.Behaviour currentBehaviour;

        private void OnEnable()
        {
            AgentManager.instance.RegisterAgent(this);
        }

        private void OnDisable()
        {
            AgentManager.instance.UnregisterAgent(this);
        }

        public void UpdateAgent()
        {
            if (currentBehaviour != null)
            {
                switch (currentBehaviour.Update())
                {
                    case BehaviourState.Running:
                        break;
                    case BehaviourState.Success:
                    case BehaviourState.Failed:
                        currentBehaviour.End();
                        //Debug.Log($"{gameObject.name} ended behavior {nameof(currentBehaviour)}");
                        currentBehaviour = null;
                        break;
                }
                return;
            }
            else
            {
                List<Behaviours.Behaviour> startableBehaviours = behaviours.Where(b => b.CanBegin()).ToList();
                if (startableBehaviours.Count > 0)
                {
                    currentBehaviour = startableBehaviours[UnityEngine.Random.Range(0, startableBehaviours.Count)];
                    currentBehaviour.Start();
                    //Debug.Log($"{gameObject.name} started behavior {nameof(currentBehaviour)}");
                }
            }
        }
    }
}
