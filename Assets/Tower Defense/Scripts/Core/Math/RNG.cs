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

        public static UnityEngine.Vector3 GetRandomPositionInTorus(float ringRadius, float wallRadius)
        {
            // get a random angle around the ring
            //float rndAngle = UnityEngine.Random.value * 6.28f; // use radians, saves converting degrees to radians
            UnityEngine.Random.InitState(Int(0, 99));

            float rndAngle = UnityEngine.Random.rotation.eulerAngles.y;

            // determine position
            float cX = UnityEngine.Mathf.Sin(rndAngle);
            float cZ = UnityEngine.Mathf.Cos(rndAngle);

            UnityEngine.Vector3 ringPos = new UnityEngine.Vector3(cX, 0, cZ);
            ringPos *= ringRadius;

            // At any point around the center of the ring
            // a sphere of radius the same as the wallRadius will fit exactly into the torus.
            // Simply get a random point in a sphere of radius wallRadius,
            // then add that to the random center point
            UnityEngine.Vector3 sPos = UnityEngine.Random.insideUnitSphere * wallRadius;

            return (ringPos + sPos);
        }
    }
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////