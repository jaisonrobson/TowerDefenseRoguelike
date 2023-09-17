using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Math
{
    public static class Slerp
    {
        public static IEnumerable<Vector3> EvaluateSlerpPoints(Vector3 start, Vector3 end, float centerOffset, float startTime, float journeyTime)
        {
            Vector3 centerPivot = (start + end) * 0.5f;
            
            centerPivot -= new Vector3(0, -centerOffset);

            Vector3 startRelativeCenter = start - centerPivot;
            Vector3 endRelativeCenter = end - centerPivot;

            float fractionCompleted = (Time.fixedTime - startTime) / journeyTime;

            yield return Vector3.Slerp(startRelativeCenter, endRelativeCenter, fractionCompleted) + centerPivot;
        }

        public static Vector3 EvaluateSlerpPointsVector3(Vector3 start, Vector3 end, float centerOffset, float startTime, float journeyTime)
        {
            Vector3 centerPivot = (start + end) * 0.5f;

            centerPivot -= new Vector3(0, centerOffset);

            Vector3 startRelativeCenter = start - centerPivot;
            Vector3 endRelativeCenter = end - centerPivot;

            float fractionCompleted = Mathf.InverseLerp(startTime, journeyTime, Time.fixedTime);

            return Vector3.Slerp(startRelativeCenter, endRelativeCenter, fractionCompleted) + centerPivot;
        }
    }
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////