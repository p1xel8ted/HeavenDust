using FoW;
using HarmonyLib;
using UnityEngine;

namespace HeavenDustTwo;

[HarmonyPatch]
public static class Patches
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(MainUI), nameof(MainUI.ShowStudioName))]
    public static bool MainUI_ShowStudioName()
    {
        Plugin.SetResolution();
        return false;
    }
    
    [HarmonyPostfix]
    [HarmonyPatch(typeof(CameraCtl), "Update")]
    public static void CameraCtl_Update(ref CameraCtl __instance)
    {
        var cam = GameObject.Find("Canvas/BottomLayer/MainUI/ImgMask").GetComponent<CanvasRenderer>();

        if (Screen.currentResolution.height != Plugin.Height.Value || Screen.currentResolution.width != Plugin.Width.Value)
        {
            Plugin.SetResolution(__instance);
        }

        if (Plugin.HideOverlay.Value && cam.GetAlpha() > 0f)
        {
            cam.SetAlpha(0f);
        }
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(SplashScreen), nameof(SplashScreen.DoStart))]
    public static void SplashScreen_DoStart(ref bool ___m_bDone)
    {
        ___m_bDone = true;
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(CameraCtl), "Start")]
    public static void CameraCtl_Start(ref CameraCtl __instance)
    {
        Plugin.SetResolution(__instance);

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

        if (!Plugin.HideOverlay.Value) return;
        
        var cam = GameObject.Find("Canvas/BottomLayer/MainUI/ImgMask").GetComponent<CanvasRenderer>();
        cam.SetAlpha(0f);
    }
}