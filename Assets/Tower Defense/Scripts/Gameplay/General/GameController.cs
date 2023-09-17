using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

[RequireComponent(typeof(GameManager))]
[HideMonoScript]
public class GameController : MonoBehaviour
{
	// (Unity) Methods [START]
    void Update()
    {
        HandleGameFinishing();
    }
    // (Unity) Methods [END]

    // Private (Methods) [START]
    private void HandleGameFinishing()
    {
        if (GameManager.instance.IsRunning)
        {
            //Reunir todos as provaveis causas que podem causar o fim do jogo aqui
            //seja de forma positiva ou negativa para o jogador.
            if (!WaveController.instance.IsRunning && !WaveController.instance.IsAnyAgentAlive)
                GameManager.instance.EndGame();

            if (!PlayerManager.instance.IsPlayerAlive)
                GameManager.instance.EndGame();
        }
    }
    // Private (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////