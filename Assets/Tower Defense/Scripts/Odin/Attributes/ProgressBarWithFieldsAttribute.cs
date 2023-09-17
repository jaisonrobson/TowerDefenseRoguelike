using System;

public class ProgressBarWithFieldsAttribute : Attribute
{
    public float min;
    public float max;
    public float r;
    public float g;
    public float b;

    public ProgressBarWithFieldsAttribute(float pMin = 0f, float pMax = 100f, float pR = 1f, float pG = 1f, float pB = 1f)
    {
        this.min = pMin;
        this.max = pMax;
        this.r = pR;
        this.g = pG;
        this.b = pB;
    }
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////