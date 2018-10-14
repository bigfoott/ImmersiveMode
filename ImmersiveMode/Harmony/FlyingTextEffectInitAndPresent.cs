using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ImmersiveMode.Harmony
{
    [HarmonyPatch(typeof(FlyingTextEffect))]
    [HarmonyPatch("InitAndPresent")]
    [HarmonyPatch(new Type[] { typeof(string), typeof(float), typeof(Vector3), typeof(Color), typeof(float), typeof(bool) })]
    class FlyingTextEffectInitAndPresent
    {
        static void Prefix(FlyingTextEffect __instance, string text, float duration, Vector3 targetPos, Color color, float fontSize, bool shake)
        {
            foreach (Transform c in __instance.gameObject.transform)
                c.gameObject.layer = 26;
        }
    }
}
