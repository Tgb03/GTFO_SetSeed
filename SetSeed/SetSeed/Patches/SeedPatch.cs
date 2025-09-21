using GameData;
using HarmonyLib;
using SNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetSeed.Patches;

[HarmonyPatch]
internal class SeedPatch
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(RundownManager), nameof(RundownManager.SetActiveExpedition))]
    public static void SetActiveExp(ref pActiveExpedition expPackage, ref ExpeditionInTierData expTierData)
    {
        if (Data.session_seed != 0 && SNet.IsMaster)
            expPackage.sessionSeed = Data.session_seed;

        Plugin.L.LogMessage($"Current Seeds ->> Session: {expPackage.sessionSeed} --- Build: {expTierData.Seeds.BuildSeed}");
    }
}
