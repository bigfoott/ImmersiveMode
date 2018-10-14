using IllusionPlugin;
using Ryder.Lightweight;
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

        private static Redirection flyingTextEffect_InitAndPresent;
        private static Redirection flyingSpriteEffect_InitAndPresent;

        private void Awake()
        {
            Console.WriteLine("[ImmersiveMode] Object initialized. Waiting for load. --------------------------");
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

            Console.WriteLine("[ImmersiveMode] Found objects. Initializing HUDHider. --------------------------");

            StartCoroutine(Init());
        }

        IEnumerator Init()
        {
            Console.WriteLine("[ImmersiveMode] Initialized. --------------------------");

            yield return new WaitForSeconds(0.05f);

            flyingTextEffect_InitAndPresent = new Redirection(typeof(FlyingTextEffect).GetMethod("InitAndPresent"), typeof(HUDHider).GetMethod("FlyingTextInit"), true);
            flyingSpriteEffect_InitAndPresent = new Redirection(typeof(FlyingSpriteEffect).GetMethod("InitAndPresent"), typeof(HUDHider).GetMethod("FlyingSpriteInit"), true);

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

            foreach (var pl in IllusionInjector.PluginManager.Plugins.Where(p => Plugin.cameraplugins.Contains(p.Name)))
            {
                MonoBehaviour _cameraPlus = ReflectionUtil.GetPrivateField<MonoBehaviour>(pl, "_cameraPlus");
                while (_cameraPlus == null)
                {
                    yield return new WaitForEndOfFrame();
                    _cameraPlus = ReflectionUtil.GetPrivateField<MonoBehaviour>(pl, "_cameraPlus");
                }
                Camera cam = ReflectionUtil.GetPrivateField<Camera>(_cameraPlus, "_cam");
                if (cam != null)
                {
                    if (ModPrefs.GetBool("ImmersiveMode", "MirrorEnabled", false, true))
                        cam.cullingMask &= ~(1 << 26);
                    else
                        cam.cullingMask |= (1 << 26);
                }
                break;
            }

            try
            {
                multi.layer = 26;
                foreach (Transform c in multi.transform)
                    c.gameObject.layer = 26;

                combo.layer = 26;
                foreach (Transform c in combo.transform)
                    c.gameObject.layer = 26;
                
                front.layer = 26;
                foreach (Transform c in front.transform.GetChild(0).transform)
                    c.gameObject.layer = 26;

                if (energy != null)
                {
                    energy.layer = 26;
                    foreach (Transform c in energy.transform)
                        c.gameObject.layer = 26;
                }

                if (progress_counter != null)
                {
                    progress_counter.layer = 26;
                    foreach (Transform c in progress_counter.transform)
                        c.gameObject.layer = 26;
                }
                
                if (progress_score != null)
                {
                    progress_score.layer = 26;
                    foreach (Transform c in progress_score.transform)
                        c.gameObject.layer = 26;
                }

                if (progress_rank != null)
                {
                    progress_rank.layer = 26;
                    foreach (Transform c in progress_rank.transform)
                        c.gameObject.layer = 26;
                }

                if (fcdisplay_ring != null)
                {
                    fcdisplay_ring.layer = 26;
                    foreach (Transform c in fcdisplay_ring.transform)
                        c.gameObject.layer = 26;
                }
                
                if (tweaks_time != null) tweaks_time.layer = 26;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            Console.WriteLine("[ImmersiveMode] Disabled UI objects. --------------------------");
        }

        public static void FlyingTextInit(FlyingTextEffect fte, string text, float duration, Vector3 targetPos, Color color, float fontSize, bool shake)
        {
            flyingTextEffect_InitAndPresent.InvokeOriginal(fte, text, duration, targetPos, color, fontSize, shake);

            //fte.gameObject.layer = 26;
            foreach (Transform c in fte.gameObject.transform)
                c.gameObject.layer = 26;
        }

        public static void FlyingSpriteInit(FlyingSpriteEffect fse, float duration, Vector3 targetPos, Color color, bool shake)
        {
            flyingSpriteEffect_InitAndPresent.InvokeOriginal(fse, duration, targetPos, color, shake);

            fse.gameObject.layer = 26;
        }
    }
}
