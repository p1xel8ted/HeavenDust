using HarmonyLib;
using UnityEngine;

namespace HeavenDustOne;

[HarmonyPatch]
public static class Patches
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(Utils), nameof(Utils.OnChangeFullScreen))]
    public static bool Utils_OnChangeFullScreen()
    {
        Screen.SetResolution(Display.main.systemWidth, Display.main.systemHeight, true);
        return false;

    }
}