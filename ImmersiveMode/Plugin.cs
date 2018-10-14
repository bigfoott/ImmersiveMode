using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using IllusionPlugin;

namespace ImmersiveMode
{
    public class Plugin : IPlugin
    {

        public string Name => "Immersive Mode";
        public string Version => "1.0.0";

        private static readonly string[] env = { "DefaultEnvironment", "BigMirrorEnvironment", "TriangleEnvironment", "NiceEnvironment" };
        public static readonly string[] cameraplugins = { "CameraPlus", "CameraPlusOrbitEdition", "DynamicCamera" };

        private void OnActiveSceneChanged(Scene arg0, Scene arg1)
        {
            if (env.Contains(arg1.name) && (ModPrefs.GetBool("ImmersiveMode", "HMDEnabled", false, true) || ModPrefs.GetBool("ImmersiveMode", "MirrorEnabled", false, true)))
                new GameObject("HUDHider").AddComponent<HUDHider>();
            else if (SettingsUI.isMenuScene(arg1))
            {
                var subMenu = SettingsUI.CreateSubMenu("Immersive Mode");
                var hmd = subMenu.AddBool("Hide HUD in HMD");
                var mirror = subMenu.AddBool("Hide HUD in mirror");
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