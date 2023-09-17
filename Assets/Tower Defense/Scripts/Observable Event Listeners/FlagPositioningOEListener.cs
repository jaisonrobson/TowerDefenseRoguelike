using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Core.Patterns;
using Core.Physics;

public class FlagPositioningOEListener : ObservableEventListener
{
    // (Unity) Methods [START]
    protected override void OnEnable()
    {
        oEvent = Resources.LoadAll<ObservableEvent>("Observable Events").ToList().Where(oe => oe.name == "FlagPositioning").FirstOrDefault();

        base.OnEnable();
    }
    public override void OnEventOccurs(GameObject pGameObject)
    {
        if (gameObject == pGameObject)
        {
            PlayableStructure ps = GetComponent<PlayableStructure>();

            if (ps)
            {
                RaycastHit hit = Raycasting.ScreenPointToRay(StructurePlacementController.instance.groundLayer);

                if (!Raycasting.IsHitEmpty(hit))
                    ps.SetFlagPosition(hit.point);
            }
        }
    }
    // (Unity) Methods [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////