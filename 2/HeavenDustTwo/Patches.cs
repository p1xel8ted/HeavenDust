using FoW;
using HarmonyLib;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

namespace HeavenDustTwo;

[HarmonyPatch]
public static class Patches
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(MainUI), nameof(MainUI.ShowStudioName))]
    public static bool MainUI_ShowStudioName()
    {
        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(OptionMgr), "SetResolution")]
    public static bool OptionMgr_SetResolution()
    {
        Screen.SetResolution(Plugin.Width.Value, Plugin.Height.Value, true);
        return false;
    }


    [HarmonyPostfix]
    [HarmonyPatch(typeof(CameraCtl), "Update")]
    public static void CameraCtl_Update(ref CameraCtl __instance)
    {
        var cam = GameObject.Find("Canvas/BottomLayer/MainUI/ImgMask").GetComponent<CanvasRenderer>();
        if (Plugin.HideOverlay.Value && cam.GetAlpha() > 0f)
        {
            cam.SetAlpha(0f);
        }
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(CameraCtl), "Start")]
    public static void CameraCtl_Start(ref CameraCtl __instance)
    {
        var fow = __instance.GetComponentInChildren<FogOfWarLegacy>();

        if (Plugin.DisableFogOfWar.Value)
        {
            fow.enabled = false;
        }

        var effects = __instance.GetComponentInChildren<MobilePostProcessing>();
        if (Plugin.DisableVignette.Value)
        {
            effects.Vignette = false;
        }

        if (Plugin.DisableChromaticAberration.Value)
        {
            effects.ChromaticAberration = false;
        }

        if (Plugin.HideOverlay.Value)
        {
            var cam = GameObject.Find("Canvas/BottomLayer/MainUI/ImgMask").GetComponent<CanvasRenderer>();
            cam.SetAlpha(0f);
        }
    }
}