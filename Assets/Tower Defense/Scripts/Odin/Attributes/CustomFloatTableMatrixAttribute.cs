using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class CustomFloatTableMatrixAttribute : Attribute
{
    public string drawRowsHeaderLabelsAction;
    public string drawColumnsHeaderLabelsAction;

    public CustomFloatTableMatrixAttribute(string rowsAction, string columnsAction)
    {
        this.drawRowsHeaderLabelsAction = rowsAction;
        this.drawColumnsHeaderLabelsAction = columnsAction;
    }
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////