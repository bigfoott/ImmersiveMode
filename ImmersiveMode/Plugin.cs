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

        private readonly string[] env = { "DefaultEnvironment", "BigMirrorEnvironment", "TriangleEnvironment", "NiceEnvironment" };
        
        private void OnActiveSceneChanged(Scene arg0, Scene arg1)
        {
            if (env.Contains(arg1.name) && ModPrefs.GetBool("ImmersiveMode", "Enabled", false, true))
                new GameObject("HUDHider").AddComponent<HUDHider>();
            else if (SettingsUI.isMenuScene(arg1))
            {
                var subMenu = SettingsUI.CreateSubMenu("Immersive Mode");
                var enabled = subMenu.AddBool("Enabled");
                enabled.GetValue += delegate { return ModPrefs.GetBool("ImmersiveMode", "Enabled", false, true); };
                enabled.SetValue += delegate (bool value) { ModPrefs.SetBool("ImmersiveMode", "Enabled", value); };
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