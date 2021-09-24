using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainmenuUI : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel = null;
    [SerializeField] private GameObject creditsPanel = null;
    [SerializeField] private GameObject difficultyPanel = null;

    public void Start()
    {
        GameManager.Instancia.EstAct = GameManager.EstadoJuego.Menu;
    }

    public void PlayGame(int difficulty)
    {
        GameManager.Instancia.difficulty = difficulty;
        GameManager.Instancia.ChangeScene(GameManager.EstadoJuego.Calibrando);
    }

    public void SelectDifficulty(int playersCount)
    {
        GameManager.Instancia.playersCount = playersCount;

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
