using System;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace HeavenDustOne
{
    [BepInPlugin(PluginGuid, PluginName, PluginVer)]
    public class Plugin : BaseUnityPlugin
    {
        private const string PluginGuid = "p1xel8ted.heavendust.one";
        private const string PluginName = "HeavenDust 1 UltraWide Fix";
        private const string PluginVer = "0.1.0";
        private static readonly Harmony Harmony = new(PluginGuid);
        
        internal static ConfigEntry<bool> HideOverlay = null!;
        internal static ConfigEntry<int> Width = null!;
        internal static ConfigEntry<int> Height = null!;

        private void Awake()
        {
            HideOverlay = Config.Bind("General", "Hide Overlay", true, "Hide the camera overlay.");
            Width = Config.Bind("Resolution", "Width", Display.main.systemWidth);
            Height = Config.Bind("Resolution", "Height", Display.main.systemHeight);
            Logger.LogInfo($"Plugin {PluginName} is loaded!");
        }

        private void OnEnable()
        {
            Harmony.PatchAll();
        }
                
        private void OnDisable()
        {
            Harmony.UnpatchSelf();
        }
    }
}