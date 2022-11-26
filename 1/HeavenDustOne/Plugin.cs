using System;
using BepInEx;
using HarmonyLib;

namespace HeavenDustOne
{
    [BepInPlugin(PluginGuid, PluginName, PluginVer)]
    public class Plugin : BaseUnityPlugin
    {
        private const string PluginGuid = "p1xel8ted.heavendust.one";
        private const string PluginName = "HeavenDust 1 UltraWide Fix";
        private const string PluginVer = "0.1.0";
        private static readonly Harmony Harmony = new(PluginGuid);

        private void Awake()
        {
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