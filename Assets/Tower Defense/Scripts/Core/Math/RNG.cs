using System;
using System.Collections;
using System.Collections.Generic;

namespace Core.Math
{
    /// <summary>
    /// Random Number Generator
    /// </summary>
    public static class RNG
    {
        public static float Float(float min = 0f, float max = 1f)
        {
            Random rand = new Random();
            double range = max - min;
            double sample = rand.NextDouble();
            double scaled = (sample * range) + min;

            return (float)scaled;
        }

        public static float Float(float max = 1f)
        {
            Random rand = new Random();
            double range = max;
            double sample = rand.NextDouble();
            double scaled = (sample * range);

            return (float)scaled;
        }

        public static int Int(int min = 0, int max = 1)
        {
            Random rand = new Random();
            return rand.Next(min, max);
        }

        public static int Int(int max = 1)
        {
            Random rand = new Random();
            return rand.Next(0, max);
        }

        public static UnityEngine.Vector3 Vector3(UnityEngine.Vector3 initial, float min, float max)
        {
            return new UnityEngine.Vector3(initial.x + Float(min, max), initial.y + Float(min, max), initial.z + Float(min, max));
        }

        public static UnityEngine.Vector3 Vector3(float min, float max)
        {
            return new UnityEngine.Vector3(Float(min, max), Float(min, max), Float(min, max));
        }

        public static UnityEngine.Vector3 Vector3(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
        {
            return new UnityEngine.Vector3(Float(minX, maxX), Float(minY, maxY), Float(minZ, maxZ));
        }
    }
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////