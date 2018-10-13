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
                energy = GameObject.Find("EnergyPanel");
                
                progress_counter = GameObject.Find("Counter");
                progress_score = GameObject.Find("ScoreCounter");
                fcdisplay_ring = GameObject.Find("FCRing");
                tweaks_time = GameObject.Find("Clock Canvas");

                progress_rank = (FindObjectsOfType(typeof(GameObject)) as GameObject[]).Where(o => o.GetComponent<TextMeshPro>() != null && o.GetComponent<TextMeshPro>().text == "SSS").FirstOrDefault();
            }

            Console.WriteLine("[ImmersiveMode] Found objects. Initializing HUDHider. --------------------------");

            Init();
        }

        void Init()
        {
            Console.WriteLine("[ImmersiveMode] Initialized. --------------------------");

            multi.SetActive(false);
            combo.SetActive(false);
            front.SetActive(false);
            if (energy != null) energy.SetActive(false);

            if (progress_counter != null) progress_counter.SetActive(false);
            if (progress_score != null) progress_score.SetActive(false);
            if (progress_rank != null) progress_rank.SetActive(false);
            if (fcdisplay_ring != null) fcdisplay_ring.SetActive(false);
            if (tweaks_time != null) Destroy(tweaks_time);
            
            Console.WriteLine("[ImmersiveMode] Disabled UI objects. --------------------------");
        }
        
        void Update()
        {
            if (FindObjectsOfType<FlyingTextEffect>().Count() > 0)
                foreach (FlyingTextEffect f in FindObjectsOfType<FlyingTextEffect>())
                    f.gameObject.SetActive(false);
        }
    }
}
