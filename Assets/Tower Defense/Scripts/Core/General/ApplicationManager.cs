using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;
using Core.Patterns;
using TheraBytes.BetterUi;


namespace Core.General
{
    public class ApplicationManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            InitializeHeroSelection();
            InitializeStructuresProgress();
        }

        // Update is called once per frame
        void Update()
        {

        }


        // Private (Methods) [START]
        private void InitializeHeroSelection()
        {
            string selectedHero = PlayerPrefs.GetString("selectedHero");

            if (selectedHero.Length <= 0)
            {
                AgentSO hero = Resources.LoadAll<AgentSO>("SO's/Agents").ToList().Where(agt => agt.subtype == AgentSubTypeEnum.HERO).ToList().First();

                PlayerPrefs.SetString("selectedHero", hero.name);

                PlayerPrefs.Save();
            }
        }
        private void InitializeStructuresProgress()
        {
            float projectileStructureProgress = PlayerPrefs.GetFloat("projectileStructureProgress");
            float smiteStructureProgress = PlayerPrefs.GetFloat("smiteStructureProgress");
            float bombingStructureProgress = PlayerPrefs.GetFloat("bombingStructureProgress");
            float lightningStructureProgress = PlayerPrefs.GetFloat("lightningStructureProgress");
            float manaStructureProgress = PlayerPrefs.GetFloat("manaStructureProgress");
            float desintegrationStructureProgress = PlayerPrefs.GetFloat("desintegrationStructureProgress");

            if (projectileStructureProgress == 0)
                PlayerPrefs.SetFloat("projectileStructureProgress", 0f);
            if (smiteStructureProgress == 0)
                PlayerPrefs.SetFloat("smiteStructureProgress", 0f);
            if (bombingStructureProgress == 0)
                PlayerPrefs.SetFloat("bombingStructureProgress", 0f);
            if (lightningStructureProgress == 0)
                PlayerPrefs.SetFloat("lightningStructureProgress", 0f);
            if (manaStructureProgress == 0)
                PlayerPrefs.SetFloat("manaStructureProgress", 0f);
            if (desintegrationStructureProgress == 0)
                PlayerPrefs.SetFloat("desintegrationStructureProgress", 0f);

            PlayerPrefs.Save();
        }
        // Private (Methods) [END]

        // Public (Methods) [START]
        public void QuitGame()
        {
            Application.Quit();
        }
        // Public (Methods) [END]
    }
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////