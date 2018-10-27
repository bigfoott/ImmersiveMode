using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using IllusionPlugin;
using Harmony;
using System.Reflection;

namespace ImmersiveMode
{
    public class Plugin : IPlugin
    {

        public string Name => "Immersive Mode";
        public string Version => "1.0.1";

        private static readonly string[] env = { "DefaultEnvironment", "BigMirrorEnvironment", "TriangleEnvironment", "NiceEnvironment" };

        private void OnActiveSceneChanged(Scene arg0, Scene arg1)
        {
            if (env.Contains(arg1.name) && (ModPrefs.GetBool("ImmersiveMode", "HMDEnabled", false, true) || ModPrefs.GetBool("ImmersiveMode", "MirrorEnabled", false, true)))
            {
                new GameObject("HUDHider").AddComponent<HUDHider>();
                new GameObject("WaitForFailText").AddComponent<WaitForFailText>();
            }
            else if (SettingsUI.isMenuScene(arg1))
            {
                var subMenu = SettingsUI.CreateSubMenu("Immersive Mode");
                
                var hmd = subMenu.AddBool("Hide in HMD");
                var mirror = subMenu.AddBool("Hide in MIRROR");
                
                hmd.GetValue += delegate { return ModPrefs.GetBool("ImmersiveMode", "HMDEnabled", false, true); };
                hmd.SetValue += delegate (bool value) { ModPrefs.SetBool("ImmersiveMode", "HMDEnabled", value); };
                
                mirror.GetValue += delegate { return ModPrefs.GetBool("ImmersiveMode", "MirrorEnabled", false, true); };
                mirror.SetValue += delegate (bool value) { ModPrefs.SetBool("ImmersiveMode", "MirrorEnabled", value); };
            }
        }
        
        public void OnApplicationStart()
        {
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
            SceneManager.sceneLoaded += OnSceneLoaded;

            try
            {
                var harmony = HarmonyInstance.Create("com.bigfoot.BeatSaber.ImmersiveMode");
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception e)
            {
                Console.WriteLine("[ImmersiveMode] This plugin requires Harmony. Make sure you installed the mod correctly.\n" + e);
            }
        }
        public void OnApplicationQuit()
        {
            SceneManager.activeSceneChanged -= OnActiveSceneChanged;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1) { }
        public void OnLevelWasLoaded(int level) { }
        public void OnLevelWasInitialized(int level) { }
        public void OnUpdate() { }
        public void OnFixedUpdate() { }
    }
}