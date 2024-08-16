using CheeseMods.AirTraffic3.Components;
using HarmonyLib;

[HarmonyPatch(typeof(AirportManager), "Awake")]
class Patch_AirportManager_Awake
{
    [HarmonyPrefix]
    static bool Awake(AirportManager __instance)
    {
        AirportHelper helper = __instance.gameObject.AddComponent<AirportHelper>();
        helper.airport = __instance.gameObject.GetComponent<AirportManager>();
        return true;
    }
}
