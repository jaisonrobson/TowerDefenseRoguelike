using UnityEngine;
using System.Collections;
public class FPSCounterGUI : MonoBehaviour
{
    private float count;
    private bool isEnabled = true;
    private IEnumerator Start()
    {
        GUI.depth = 2;
        while (true)
        {
            count = 1f / Time.unscaledDeltaTime;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            isEnabled = !isEnabled;
    }

    private void OnGUI()
    {
        if (isEnabled)
        {
            Rect location = new Rect(5, 5, 85, 25);
            string text = $"FPS: {Mathf.Round(count)}";
            Texture black = Texture2D.linearGrayTexture;
            GUI.DrawTexture(location, black, ScaleMode.StretchToFill);
            GUI.color = Color.black;
            GUI.skin.label.fontSize = 18;
            GUI.Label(location, text);
        }
    }
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////