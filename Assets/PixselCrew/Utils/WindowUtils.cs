using UnityEngine;
using System;

namespace PixselCrew.Utils
{
    public static class WindowUtils
    {
        public static void CreateWindow(string resourcePath)
        {
            var window = Resources.Load<GameObject>(resourcePath);
            var canvas = UnityEngine.Object.FindObjectOfType<Canvas>();
            UnityEngine.Object.Instantiate(window, canvas.transform);
        }
    }
}
