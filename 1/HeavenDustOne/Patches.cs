using HarmonyLib;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UIElements;

namespace HeavenDustOne;

[HarmonyPatch]
public static class Patches
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(Utils), nameof(Utils.OnChangeFullScreen))]
    public static bool Utils_OnChangeFullScreen()
    {
        Screen.SetResolution(Plugin.Width.Value, Plugin.Height.Value, true);
        return false;

    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(CameraCtl), "Awake")]
    //[HarmonyPatch(typeof(CameraCtl), "Update")]
    public static void CameraCtl_Awake(ref CameraCtl __instance)
    {
        if (Plugin.HideOverlay.Value)
        {
            __instance.m_canvas.GetComponentInChildren<CanvasRenderer>().SetAlpha(0f);
        }
    }
}