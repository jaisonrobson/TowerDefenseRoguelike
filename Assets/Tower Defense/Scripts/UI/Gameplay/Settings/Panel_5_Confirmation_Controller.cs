using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using Sirenix.OdinInspector;
using Core.Patterns;
using TheraBytes.BetterUi;

[HideMonoScript]
public class Panel_5_Confirmation_Controller : MonoBehaviour
{
    // Public (Variables) [START]
    [Required]
    [SceneObjectsOnly]
    public BetterText descriptionText;
    // Public (Variables) [END]

    // Private (Variables) [START]
    private ConfirmationTypeEnum confirmationType = ConfirmationTypeEnum.None;
    // Private (Variables) [END]

    // Public (Properties) [START]
    public ConfirmationTypeEnum ConfirmationType { get { return confirmationType; } set { confirmationType = value; } }
    // Public (Properties) [END]

    // (Unity) Methods [START]
    void Start()
    {

    }

    void Update()
    {
        
    }
    // (Unity) Methods [END]


    // Private (Methods) [START]
    private string GenerateConfirmationDescriptionText()
    {
        switch(confirmationType)
        {
            case ConfirmationTypeEnum.ExitToSystem:
                return "Exit to System";
            case ConfirmationTypeEnum.LeaveMatch:
                return "Leave Match";
        }

        return "Error";
    }
    private void OpenConfirmationScreen(ConfirmationTypeEnum pCTE)
    {
        ConfirmationType = pCTE;
        descriptionText.text = GenerateConfirmationDescriptionText();
        gameObject.SetActive(true);
    }
    // Private (Methods) [END]

    // Public (Methods) [START]
    public void CallExitToSystem() => OpenConfirmationScreen(ConfirmationTypeEnum.ExitToSystem);
    public void CallLeaveMatch() => OpenConfirmationScreen(ConfirmationTypeEnum.LeaveMatch);
    public void OnConfirm()
    {
        switch (confirmationType)
        {
            case ConfirmationTypeEnum.LeaveMatch:
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
                return;

            case ConfirmationTypeEnum.ExitToSystem:
                Application.Quit();
                return;
        }
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////