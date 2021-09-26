using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainmenuUI : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel = null;
    [SerializeField] private GameObject creditsPanel = null;
    [SerializeField] private GameObject difficultyPanel = null;

    public void PlayGame(int difficulty)
    {
        GameSettings.Instancia.difficulty = difficulty;
        GameSettings.Instancia.ChangeScene(GameSettings.EstadoJuego2.Jugando);
    }

    public void SelectDifficulty(int playersCount)
    {
        GameSettings.Instancia.playersCount = playersCount;

        menuPanel.SetActive(false);
        difficultyPanel.SetActive(true);
    }

    public void ShowMenu()
    {
        menuPanel.SetActive(true);
        difficultyPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void ShowCredits()
    {
        menuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
