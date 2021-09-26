using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSettings : MonoBehaviourSingleton<GameSettings>
{

    public enum EstadoJuego2
    {
        Menu,
        Jugando,
        Finalizado
    }

    public int playersCount = 1;
    public int difficulty = 1;

    public void ChangeScene(EstadoJuego2 estGame)
    {
        string sceneName = "";

        switch (estGame)
        {
            case EstadoJuego2.Menu:
                sceneName = "Mainmenu";
                break;
            case EstadoJuego2.Jugando:
                sceneName = "Gameplay";
                break;
            case EstadoJuego2.Finalizado:
                sceneName = "Gameover";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(estGame), estGame, null);
        }

        SceneManager.LoadScene(sceneName);
    }
}
