using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameoverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textPj1;
    [SerializeField] TextMeshProUGUI textPj2;

    [SerializeField] GameObject[] imagenGanadores;
    void Start()
    {
        textPj1.text = "$: " + GameSettings.Instancia.puntosP1 / 1000;
        textPj2.text = "$: " + GameSettings.Instancia.puntosP2 / 1000;

        if (GameSettings.Instancia.puntosP1 > GameSettings.Instancia.puntosP2)
        {
            imagenGanadores[0].SetActive(true);
            imagenGanadores[1].SetActive(false);
        }
        else
        {
            imagenGanadores[0].SetActive(false);
            imagenGanadores[1].SetActive(true);
        }
    }

    public void BackToMenu()
    {
        GameSettings.Instancia.ChangeScene(GameSettings.EstadoJuego2.Menu);
    }
}
