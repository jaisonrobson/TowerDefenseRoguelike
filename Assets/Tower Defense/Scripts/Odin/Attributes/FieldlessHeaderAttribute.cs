#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System;

public class FieldlessHeaderAttribute : PropertyGroupAttribute
{
    public string title = "";
    public string subtitle = "";
    public TextAlignment textAlignment = TextAlignment.Left;
    public bool horizontalLine = true;
    public bool boldLabel = true;

    public FieldlessHeaderAttribute(string title, string subtitle = "", TextAlignment textAlignment = TextAlignment.Left, bool horizontalLine = true, bool boldLabel = true) : base(title)
    {
        this.title = title;
        this.subtitle = subtitle;
        this.textAlignment = textAlignment;
        this.horizontalLine = horizontalLine;
        this.boldLabel = boldLabel;
    }
}

public class FieldlessHeaderAttributeDrawer : OdinAttributeDrawer<FieldlessHeaderAttribute> {
    protected override void DrawPropertyLayout(GUIContent label)
    {
        //string title, string subtitle, TextAlignment textAlignment, bool horizontalLine, bool boldLabel = true;

        SirenixEditorGUI.Title(base.Attribute.title, base.Attribute.subtitle, base.Attribute.textAlignment, base.Attribute.horizontalLine, base.Attribute.boldLabel);
    }
}
#endif

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////