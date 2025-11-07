using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Resource
{   
    public static class ResourceManager
    {
        public static int ScrollWidth { get { return 20; } }
        public static float ScrollSpeed { get { return 30; } }
        public static float MinCameraHeight { get { return 10; } }
        public static float MaxCameraHeight { get { return 40; } }
        public static float RotateAmount { get { return 45; } }
        public static float RotateSpeed { get { return 60; } }
    }
}