using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    [SerializeField] ContrCalibracion p1;
    [SerializeField] ContrCalibracion p2;
    bool cambiandoEscena;
    [SerializeField] GameObject[] cameras;

    [SerializeField] GameObject[] botones;
    [SerializeField] Camera camP1;
    [SerializeField] GameObject Escena2;
    void Start()
    {
        for (int i = 0; i < botones.Length; i++)
        {
            if (i > 3 && GameSettings.Instancia.playersCount == 1)
            {
                if (botones[i] != null)
                    botones[i].SetActive(false);
            }
        }
            


#if UNITY_EDITOR 
        if (GameSettings.Instancia.playersCount == 1)
        {
            camP1.rect = new Rect(0, 0, 1, 1);
            Escena2.SetActive(false);
        }
#elif UNITY_ANDROID || UNITY_IOS
        botones[0].SetActive(true);
        if(GameSettings.Instancia.playersCount == 1) {
            camP1.rect = new Rect(0, 0, 1, 1);
            Escena2.SetActive(false);
        }
        else{
            botones[4].SetActive(true);
        }
#endif

    }

    // Update is called once per frame
    void Update()
    {
        if (GameSettings.Instancia.playersCount == 2)
        {
            if (p1.GetTutorialTerminado() && p2.GetTutorialTerminado() && !cambiandoEscena)
            {
                cambiandoEscena = true;
                cameras[0].SetActive(false);
                cameras[1].SetActive(false);
                GameManager.Instancia.FinCalibracion(0);
                GameManager.Instancia.FinCalibracion(1);

                GameManager.Instancia.FinTutorial(0);
                GameManager.Instancia.FinTutorial(1);
            }
        }
        else
        {
            if (p1.GetTutorialTerminado() && !cambiandoEscena)
            {
                cambiandoEscena = true;
                cameras[0].SetActive(false);

                GameManager.Instancia.FinCalibracion(0);
                GameManager.Instancia.FinTutorial(0);
            }
        }
    }
}