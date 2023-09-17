using System;
using UnityEngine;

namespace Core.Math
{
    public static class ConvertionTools
    {
        public static double ConvertToDouble<T>(T value)
        {
            return Convert.ToDouble(value);
        }

        public static T ConvertFromDouble<T>(double value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static int ConvertToInt<T>(T value)
        {
            return Convert.ToInt32(value);
        }

        public static T ConvertFromInt<T>(int value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static float ConvertToFloat<T>(T value)
        {
            return Convert.ToSingle(value);
        }

        public static T ConvertFromFloat<T>(float value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////