using IllusionPlugin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace ImmersiveMode
{
    class HUDHider : MonoBehaviour
    {
        GameObject multi;
        GameObject combo;
        GameObject energy;
        GameObject front;

        GameObject progress_counter;
        GameObject progress_score;
        GameObject progress_rank;
        GameObject fcdisplay_ring;
        GameObject tweaks_time;
        
        private static readonly string[] cameraplugins = { "CameraPlus", "CameraPlusOrbitEdition", "DynamicCamera" };

        private void Awake()
        {
            Console.WriteLine("[ImmersiveMode] Object initialized. Waiting for load.");
            StartCoroutine(WaitForLoad());
        }

        IEnumerator WaitForLoad()
        {
            while (multi == null || combo == null || front == null)
            {
                yield return new WaitForSeconds(0.02f);

                multi = GameObject.Find("MultiplierPanel");
                combo = GameObject.Find("ComboPanel");
                front = GameObject.Find("FrontPanel");
            }

            Console.WriteLine("[ImmersiveMode] Found objects. Initializing HUDHider.");

            StartCoroutine(Init());
        }

        IEnumerator Init()
        {
            Console.WriteLine("[ImmersiveMode] Initialized.");

            yield return new WaitForSeconds(0.05f);

            energy = GameObject.Find("EnergyPanel");

            progress_counter = GameObject.Find("Counter");
            progress_score = GameObject.Find("ScoreCounter");
            fcdisplay_ring = GameObject.Find("FCRing");
            tweaks_time = GameObject.Find("Clock Canvas");
            
            progress_rank = (FindObjectsOfType(typeof(GameObject)) as GameObject[]).Where(
                o => o.GetComponent<TextMeshPro>() != null && o.GetComponent<TextMeshPro>().text == "SSS").FirstOrDefault();
            
            Camera mainCamera = FindObjectsOfType<Camera>().FirstOrDefault(x => x.CompareTag("MainCamera"));
            if (ModPrefs.GetBool("ImmersiveMode", "HMDEnabled", false, true))
                mainCamera.cullingMask &= ~(1 << 26);
            else
                mainCamera.cullingMask |= (1 << 26);

            foreach (var pl in IllusionInjector.PluginManager.Plugins.Where(p => cameraplugins.Contains(p.Name)))
            {
                MonoBehaviour camPlus = ReflectionUtil.GetPrivateField<MonoBehaviour>(pl, "_cameraPlus");
                while (camPlus == null)
                {
                    yield return new WaitForEndOfFrame();
                    camPlus = ReflectionUtil.GetPrivateField<MonoBehaviour>(pl, "_cameraPlus");
                }

                Camera cam = ReflectionUtil.GetPrivateField<Camera>(camPlus, "_cam");
                if (cam != null)
                {
                    if (ModPrefs.GetBool("ImmersiveMode", "MirrorEnabled", false, true))
                        cam.cullingMask &= ~(1 << 26);
                    else
                        cam.cullingMask |= (1 << 26);
                }
                break;
            }

            multi.layer = 26;
            foreach (Transform c in multi.transform) c.gameObject.layer = 26;

            combo.layer = 26;
            foreach (Transform c in combo.transform) c.gameObject.layer = 26;

            front.layer = 26;
            foreach (Transform c in front.transform.GetChild(0).transform) c.gameObject.layer = 26;

            if (energy != null)
            {
                energy.layer = 26;
                foreach (Transform c in energy.transform) c.gameObject.layer = 26;
            }

            if (progress_counter != null)
            {
                progress_counter.layer = 26;
                foreach (Transform c in progress_counter.transform) c.gameObject.layer = 26;
            }

            if (progress_score != null)
            {
                progress_score.layer = 26;
                foreach (Transform c in progress_score.transform) c.gameObject.layer = 26;
            }

            if (progress_rank != null)
            {
                progress_rank.layer = 26;
                foreach (Transform c in progress_rank.transform) c.gameObject.layer = 26;
            }

            if (fcdisplay_ring != null)
            {
                fcdisplay_ring.layer = 26;
                foreach (Transform c in fcdisplay_ring.transform) c.gameObject.layer = 26;
            }

            if (tweaks_time != null) tweaks_time.layer = 26;

            Console.WriteLine("[ImmersiveMode] Applied hidden layer (26) to game objects.");
        }
    }
}
