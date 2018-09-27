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
        public void OnApplicationStart()
        {
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnActiveSceneChanged(Scene arg0, Scene arg1)
        {
            if (!env.Contains(arg1.name)) return;
            new GameObject("HUDHider").AddComponent<HUDHider>();
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