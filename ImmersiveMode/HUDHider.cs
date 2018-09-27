using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ImmersiveMode
{
    class HUDHider : MonoBehaviour
    {
        GameObject multi;
        GameObject combo;
        GameObject energy;

        GameObject progress_counter;
        GameObject progress_score;
        GameObject fcdisplay_ring;

        private void Awake()
        {
            StartCoroutine(WaitForLoad());
        }

        IEnumerator WaitForLoad()
        {
            bool loaded = false;
            
            while (!loaded)
            {
                multi = GameObject.Find("MultiplierPanel");
                combo = GameObject.Find("ComboPanel");
                energy = GameObject.Find("EnergyPanel");

                progress_counter = GameObject.Find("Counter");
                progress_score = GameObject.Find("ScoreCounter");
                fcdisplay_ring = GameObject.Find("FCRing");

                if (multi == null || combo == null || energy == null || progress_counter == null || progress_score == null || fcdisplay_ring == null)
                    yield return new WaitForSeconds(0.01f);
                else
                    loaded = true;
            }

            Init();
        }

        void Init()
        {
            multi.SetActive(false);
            combo.SetActive(false);
            energy.SetActive(false);
            
            progress_counter.SetActive(false);
            progress_score.SetActive(false);
            fcdisplay_ring.SetActive(false);
            
            Console.WriteLine("[ImmersiveMode] Disabled UI objects.");
        }
        
        void Update()
        {
            if (FindObjectsOfType<FlyingTextEffect>().Count() > 0)
                foreach (FlyingTextEffect f in FindObjectsOfType<FlyingTextEffect>())
                    f.gameObject.SetActive(false);
        }
    }
}
