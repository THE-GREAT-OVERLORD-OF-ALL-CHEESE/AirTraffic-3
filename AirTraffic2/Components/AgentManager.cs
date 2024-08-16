using System.Collections.Generic;
using UnityEngine;

namespace CheeseMods.AirTraffic3.Components
{
    public class AgentManager : MonoBehaviour
    {
        public static AgentManager instance;

        private List<Agent> agents = new List<Agent>();
        private int currentAgentId;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            Debug.Log($"Agent manager spawned!");
        }

        public void RegisterAgent(Agent agent)
        {
            if (!agents.Contains(agent))
            {
                Debug.Log($"Registered {agent.gameObject.name}");
                agents.Add(agent);
            }
        }

        public void UnregisterAgent(Agent agent)
        {
            Debug.Log($"Unregistered {agent.gameObject.name}");
            agents.Remove(agent);
        }

        private void Update()
        {
            UpdateAgents();
        }

        public void UpdateAgents()
        {
            currentAgentId++;

            if (currentAgentId > agents.Count - 1)
            {
                currentAgentId = 0;
            }
            else
            {
                if (currentAgentId < agents.Count)
                {
                    UpdateAgent(agents[currentAgentId]);
                }
            }
        }

        public void UpdateAgent(Agent agent)
        {
            agent.UpdateAgent();
        }
    }
}
