using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_2_1_FlagButton_Controller : MonoBehaviour
{

    // Public (Methods) [START]
    public void SetPlayerCommandCasting()
    {
        PlayerCommandsManager.instance.Command = PlayerCommandEnum.CASTING;
        CursorManager.instance.Mode = CursorModeEnum.CASTING_FLAG;
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////