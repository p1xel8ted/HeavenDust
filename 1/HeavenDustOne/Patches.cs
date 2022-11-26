using System.Linq;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UIElements;
using UnityStandardAssets.ImageEffects;

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
    public static void CameraCtl_Awake(ref CameraCtl __instance)
    {
        
        var cam = __instance.GetComponent<Camera>();
        cam.allowMSAA = true;
        
        var effects = __instance.GetComponentInChildren<VignetteAndChromaticAberration>();
        if (Plugin.DisableVignette.Value)
        {
            effects.intensity = 0f;
        }
        
        if (Plugin.DisableChromaticAberration.Value)
        {
            effects.chromaticAberration = 0f;
        }

        if (Plugin.HideOverlay.Value)
        {
            __instance.m_canvas.GetComponentInChildren<CanvasRenderer>().SetAlpha(0f);
        }
    }
}