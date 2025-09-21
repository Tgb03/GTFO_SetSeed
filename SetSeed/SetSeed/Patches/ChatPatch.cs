using Expedition;
using GameData;
using HarmonyLib;
using SNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SetSeed.Patches;

[HarmonyPatch]
internal class ChatPatch
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(PlayerChatManager), nameof(PlayerChatManager.PostMessage))]
    public static bool PostMessage(PlayerChatManager __instance)
    {
        if (SNet.LocalPlayer.IsMaster == false) { return true; }

        var message = __instance.m_currentValue;

        if (message != null && message.ToLower().StartsWith("/setsessionseed")) {
            var words = message.Split(' ');

            if (words.Length > 1 && int.TryParse(words[1], out int seed))
            {
                Data.session_seed = seed;
                
                if (RundownManager.Current != null)
                {
                    RundownManager.Current.m_activeExpedition.SessionSeed = seed;
                    Plugin.L.LogMessage($"Current Seeds ->> SessionSeed: {seed}");
                }

                return SkipOG("SessionSeed", seed);
            }

            string clipboardText = GUIUtility.systemCopyBuffer;

            if (clipboardText != null && int.TryParse(clipboardText, out int clipboard_seed))
            {
                Data.session_seed = clipboard_seed;

                if (RundownManager.Current != null)
                {
                    RundownManager.Current.m_activeExpedition.SessionSeed = clipboard_seed;
                    Plugin.L.LogMessage($"Current Seeds ->> SessionSeed: {clipboard_seed}");
                }

                return SkipOG("SessionSeed", clipboard_seed);
            }
        }

        return true;
    }

    // credit @Auri
    // https://github.com/AuriRex/GTFO_Gamemodes/blob/99872e360e03bbbc8ea7f870fc5cf052d82d3df5/GTFO_Gamemodes/Patches/Required/ChatCommandsHandler.cs#L59
    private static bool SkipOG(string name, int value)
    {
        PlayerChatManager.Current.m_currentValue = $"<color=orange>{name} set to {value}";
        return true;
    }
}
