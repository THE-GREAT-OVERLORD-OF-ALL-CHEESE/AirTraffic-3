using CheeseMods.AirTraffic3.Components;
using HarmonyLib;

[HarmonyPatch(typeof(AIPilot), "Start")]
class Patch_AIPilot_Start
{
    [HarmonyPrefix]
    static bool Start(AIPilot __instance)
    {
        Agent_Aircraft aircraft = __instance.gameObject.AddComponent<Agent_Aircraft>();
        aircraft.aircraft = __instance.gameObject.GetComponent<AIAircraftSpawn>();
        return true;
    }
}
