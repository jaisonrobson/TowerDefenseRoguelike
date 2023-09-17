using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using System.Reflection;

public class AssetNamePropertyProcessor<T> : OdinPropertyProcessor<T> where T : BaseOptionDataSO
{
    private string assetName = "";

    public override void ProcessMemberProperties(List<InspectorPropertyInfo> propertyInfos)
    {
        propertyInfos.AddValue(
            "Asset Name",
            (ref BaseOptionDataSO bod) => {
                string result = AssetDatabase.GetAssetOrScenePath(bod);

                string[] assetPathing = result.Split("/");
                string assetName = assetPathing[assetPathing.Length - 1].Replace(".asset", "");

                if (this.assetName == "")
                    this.assetName = assetName;

                return this.assetName;
            },
            (ref BaseOptionDataSO bod, string newName) => {
                bod.assetPath = AssetDatabase.GetAssetOrScenePath(bod);
                bod.newName = newName;

                assetName = newName;
            },
            new InlineButtonAttribute("SetAssetName", "Apply"),
            new TitleAttribute("Metadata")
        );

        propertyInfos.AddValue(
            "Asset Path",
            (ref BaseOptionDataSO bod) => { return AssetDatabase.GetAssetPath(bod); },
            (ref BaseOptionDataSO bod, string newName) => {},
            new PropertySpaceAttribute(0f, 10f)
        );

        for (int i = 0; i < propertyInfos.Count; i++)
        {
            if (propertyInfos[i].PropertyName == "Asset Name")
            {
                propertyInfos.Insert(0, propertyInfos[i]);
                propertyInfos.RemoveAt(i + 1);
            }
            if (propertyInfos[i].PropertyName == "Asset Path")
            {
                propertyInfos.Insert(1, propertyInfos[i]);
                propertyInfos.RemoveAt(i + 1);
            }
        }
    }


}


////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////