using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace Core.General
{
    public class SceneLoader : MonoBehaviour
    {
        [Required]
        public Slider LoadingSlider;

        void Start()
        {
            LoadScene();
        }

        public void LoadScene()
        {
            string sceneName = PlayerPrefs.GetString("sceneToLoad");

            StartCoroutine(LoadSceneAsync(sceneName));
        }

        IEnumerator LoadSceneAsync(string sceneName)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

            while (!operation.isDone)
            {
                float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

                LoadingSlider.value = progressValue;

                yield return null;
            }
        }
    }
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////