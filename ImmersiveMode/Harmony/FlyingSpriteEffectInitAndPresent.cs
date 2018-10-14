using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ImmersiveMode.Harmony
{
    [HarmonyPatch(typeof(FlyingSpriteEffect))]
    [HarmonyPatch("InitAndPresent")]
    [HarmonyPatch(new Type[] { typeof(float), typeof(Vector3), typeof(Color), typeof(bool) })]
    class FlyingSpriteEffectInitAndPresent
    {
        static void Prefix(FlyingSpriteEffect __instance, float duration, Vector3 targetPos, Color color, bool shake)
        {
            __instance.gameObject.layer = 26;
        }
    }
}
