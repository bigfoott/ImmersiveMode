using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ImmersiveMode
{
    class WaitForFailText : MonoBehaviour
    {
        public GameObject failText;

        void Awake()
        {
            failText = GameObject.Find("LevelFailedTextEffect");
        }

        void LateUpdate()
        {
            if (failText == null) Awake();
            if (failText == null) return;
            Console.WriteLine(" __     ________ ______ _______ \n" +
                " \\ \\   / /  ____|  ____|__   __|\n" +
                "  \\ \\_/ /| |__  | |__     | |   \n" +
                "   \\   / |  __| |  __|    | |   \n" +
                "    | |  | |____| |____   | |   \n" +
                "    |_|  |______|______|  |_|   ");

            failText.layer = 26;
        }
    }
}
