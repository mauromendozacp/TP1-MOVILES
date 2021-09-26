using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public static Player[] Jugadoers;

    public static GameManager Instancia;

    public float TiempoDeJuego = 60;

    public enum EstadoJuego { Calibrando, Jugando, Finalizado }
    public EstadoJuego EstAct = EstadoJuego.Jugando;

    public PlayerInfo PlayerInfo1 = null;
    public PlayerInfo PlayerInfo2 = null;

    public Player Player1;
    public Player Player2;

    [SerializeField] GameObject[] objetosApagarFacil;
    [SerializeField] GameObject[] objetosApagarNormal;
    [SerializeField] GameObject[] objetosApagarDificil;

    [SerializeField] GameSettings mg;

    [SerializeField] GameObject[] p2Objects;

    [SerializeField] Camera camp1Calib;
    [SerializeField] Camera camp1Cond;
    [SerializeField] Camera camp1Entrega;

    public Vector3[] PosCamionesCarrera = new Vector3[2];
    public Vector3 PosCamion1Tuto = Vector3.zero;
    public Vector3 PosCamion2Tuto = Vector3.zero;

    public Transform Esqueleto1;
    public Transform Esqueleto2;
    public Vector3[] PosEsqsCarrera;

    public GameObject[] ObjsCalibracion1;
    public GameObject[] ObjsCalibracion2;
    public GameObject[] ObjsTuto1;
    public GameObject[] ObjsTuto2;
    public GameObject[] ObjsCarrera;

    bool ConteoRedresivo = true;
    public Rect ConteoPosEsc;
    public float ConteoParaInicion = 3;
    public GUISkin GS_ConteoInicio;
    public Rect TiempoGUI = new Rect();
    public GUISkin GS_TiempoGUI;
    public float TiempEspMuestraPts = 3;

    void Awake()
    {
        Instancia = this;
    }

    void Start()
    {
        IniciarCalibracion();
        //para testing
        //PosCamionesCarrera[0].x+=100;
        //PosCamionesCarrera[1].x+=100;
        StartCoroutine(Play());
        mg = FindObjectOfType<GameSettings>();

        if (mg != null)
            switch (GameSettings.Instancia.difficulty)
            {
                case 1:
                    for (int i = 0; i < objetosApagarFacil.Length; i++)
                        if (objetosApagarFacil[i] != null)
                            objetosApagarFacil[i].SetActive(false);
                    break;
                case 2:
                    for (int i = 0; i < objetosApagarNormal.Length; i++)
                        if (objetosApagarNormal[i] != null)
                            objetosApagarNormal[i].SetActive(false);
                    break;
                case 3:
                    for (int i = 0; i < objetosApagarDificil.Length; i++)
                        if (objetosApagarDificil[i] != null)
                            objetosApagarDificil[i].SetActive(false);
                    break;
            }

        for (int i = 0; i < p2Objects.Length; i++)
            if (p2Objects[i] != null)
                p2Objects[i].SetActive(false);


        if (GameSettings.Instancia.playersCount == 2)
        {
            for (int i = 0; i < p2Objects.Length; i++)
                if (p2Objects[i] != null)
                    p2Objects[i].SetActive(true);
        }
        else
        {
            for (int i = 0; i < p2Objects.Length; i++)
                if (p2Objects[i] != null)
                    p2Objects[i].SetActive(false);

            camp1Calib.rect = new Rect(0f, 0f, 1f, 1f);
            camp1Entrega.rect = new Rect(0f, 0f, 1f, 1f);
            camp1Cond.rect = new Rect(0f, 0f, 1f, 1f);
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1) &&
            Input.GetKey(KeyCode.Keypad0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        switch (EstAct)
        {
            case EstadoJuego.Calibrando:
                if (Input.GetKey(KeyCode.Mouse0) &&
                    Input.GetKey(KeyCode.Keypad0))
                {
                    if (PlayerInfo1 != null || PlayerInfo2 != null)
                    {
                        FinCalibracion(0);
                        FinTutorial(0);

                        if (GameSettings.Instancia.playersCount == 2)
                        {
                            FinCalibracion(1);
                            FinTutorial(1);
                        }
                    }
                }
                if (PlayerInfo1.PJ == null && Input.GetKeyDown(KeyCode.W))
                {
                    PlayerInfo1 = new PlayerInfo(0, Player1);
                    PlayerInfo1.LadoAct = Visualizacion.Lado.Izq;
                    SetPosicion(PlayerInfo1);
                }
                if (GameSettings.Instancia.playersCount == 2)
                {
                    if (PlayerInfo2.PJ == null && Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        PlayerInfo2 = new PlayerInfo(1, Player2);
                        PlayerInfo2.LadoAct = Visualizacion.Lado.Der;
                        SetPosicion(PlayerInfo2);
                    }
                }
                if (PlayerInfo1.PJ != null || PlayerInfo2.PJ != null)
                {
                    if (PlayerInfo1.FinTuto2 && GameSettings.Instancia.playersCount == 1)
                    {
                        EmpezarCarrera();
                    }
                    if (PlayerInfo1.FinTuto2 && PlayerInfo2.FinTuto2 && GameSettings.Instancia.playersCount == 2)
                    {
                        EmpezarCarrera();
                    }
                }

                break;
            case EstadoJuego.Jugando:

                if (Input.GetKey(KeyCode.Mouse1) &&
                    Input.GetKey(KeyCode.Keypad0))
                {
                    TiempoDeJuego = 0;
                }

                if (TiempoDeJuego <= 0)
                {
                    FinalizarCarrera();
                }
                if (ConteoRedresivo)
                {
                    ConteoParaInicion -= T.GetDT();
                    if (ConteoParaInicion < 0)
                    {
                        EmpezarCarrera();
                        ConteoRedresivo = false;
                    }
                }
                else
                {
                    TiempoDeJuego -= T.GetDT();
                }

                break;

            case EstadoJuego.Finalizado:
                TiempEspMuestraPts -= Time.deltaTime;
                if (TiempEspMuestraPts <= 0)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
        }
    }

    IEnumerator Play()
    {
        yield return new WaitForSeconds(TiempoDeJuego);
        FinalizarCarrera();
        GameSettings.Instancia.ChangeScene(GameSettings.EstadoJuego2.Finalizado);
        StopCoroutine(Play());
        yield return null;
    }

    void EmpezarCarrera()
    {
        Player1.GetComponent<Frenado>().RestaurarVel();
        Player1.GetComponent<ControlDireccion>().Habilitado = true;

        Player2.GetComponent<Frenado>().RestaurarVel();
        Player2.GetComponent<ControlDireccion>().Habilitado = true;
    }

    void FinalizarCarrera()
    {
        EstAct = GameManager.EstadoJuego.Finalizado;

        TiempoDeJuego = 0;

        if (Player1.Dinero > Player2.Dinero)
        {
            //lado que gano
            if (PlayerInfo1.LadoAct == Visualizacion.Lado.Der)
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Der;
            else
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Izq;

            //puntajes
            DatosPartida.PtsGanador = Player1.Dinero;
            DatosPartida.PtsPerdedor = Player2.Dinero;
        }
        else
        {
            //lado que gano
            if (PlayerInfo2.LadoAct == Visualizacion.Lado.Der)
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Der;
            else
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Izq;

            //puntajes
            DatosPartida.PtsGanador = Player2.Dinero;
            DatosPartida.PtsPerdedor = Player1.Dinero;
        }

        Player1.GetComponent<Frenado>().Frenar();
        if (GameSettings.Instancia.playersCount == 2)
            Player2.GetComponent<Frenado>().Frenar();

        Player1.ContrDesc.FinDelJuego();
        if (GameSettings.Instancia.playersCount == 2)
            Player2.ContrDesc.FinDelJuego();
    }

    public void IniciarCalibracion()
    {
        for (int i = 0; i < ObjsCalibracion1.Length; i++)
        {
            ObjsCalibracion1[i].SetActive(true);
            ObjsCalibracion2[i].SetActive(true);
        }

        for (int i = 0; i < ObjsTuto2.Length; i++)
        {
            ObjsTuto2[i].SetActive(false);
            ObjsTuto1[i].SetActive(false);
        }

        for (int i = 0; i < ObjsCarrera.Length; i++)
        {
            ObjsCarrera[i].SetActive(false);
        }


        Player1.CambiarACalibracion();

        if (GameSettings.Instancia.playersCount==2)
        {
            Player2.CambiarACalibracion();
        }
    }

    void SetPosicion(PlayerInfo pjInf)
    {
        pjInf.PJ.GetComponent<Visualizacion>().SetLado(pjInf.LadoAct);
        pjInf.PJ.ContrCalib.IniciarTesteo();

        if (pjInf.PJ == Player1)
        {
            Player2.GetComponent<Visualizacion>().SetLado(pjInf.LadoAct == Visualizacion.Lado.Izq
                ? Visualizacion.Lado.Der
                : Visualizacion.Lado.Izq);
        }
        else
        {
            if (pjInf.LadoAct == Visualizacion.Lado.Izq)
                Player1.GetComponent<Visualizacion>().SetLado(Visualizacion.Lado.Der);
            else
                Player1.GetComponent<Visualizacion>().SetLado(Visualizacion.Lado.Izq);
        }

    }

    public void FinTutorial(int playerID)
    {
        if (playerID == 0)
        {
            PlayerInfo1.FinTuto2 = true;
        }
        else if (playerID == 1)
        {
            PlayerInfo2.FinTuto2 = true;
        }
        if (PlayerInfo1.FinTuto2 && PlayerInfo2.FinTuto2)
        {
            CambiarACarrera();
        }
    }

    public void FinCalibracion(int playerID)
    {
        if (playerID == 0)
        {
            PlayerInfo1.FinTuto1 = true;

        }
        else if (playerID == 1)
        {
            PlayerInfo2.FinTuto1 = true;
        }

        if (GameSettings.Instancia.playersCount == 2)
        {
            if (PlayerInfo1.PJ != null && PlayerInfo2.PJ != null)
                if (PlayerInfo1.FinTuto1 && PlayerInfo2.FinTuto1)
                    CambiarACarrera();
        }
        else
        {
            if (PlayerInfo1.PJ != null)
                if (PlayerInfo1.FinTuto1)
                    CambiarACarrera();
        }
    }

    void CambiarACarrera()
    {
        Esqueleto1.transform.position = PosEsqsCarrera[0];
        Esqueleto2.transform.position = PosEsqsCarrera[1];

        for (int i = 0; i < ObjsCarrera.Length; i++)
        {
            ObjsCarrera[i].SetActive(true);
        }

        PlayerInfo1.FinCalibrado = true;

        for (int i = 0; i < ObjsTuto1.Length; i++)
        {
            ObjsTuto1[i].SetActive(true);
        }

        for (int i = 0; i < ObjsCalibracion1.Length; i++)
        {
            ObjsCalibracion1[i].SetActive(false);
        }

        PlayerInfo2.FinCalibrado = true;

        for (int i = 0; i < ObjsCalibracion2.Length; i++)
        {
            ObjsCalibracion2[i].SetActive(false);
        }
        for (int i = 0; i < ObjsTuto2.Length; i++)
        {
            ObjsTuto2[i].SetActive(true);
        }
        if (PlayerInfo1.LadoAct == Visualizacion.Lado.Izq)
        {
            Player1.gameObject.transform.position = PosCamionesCarrera[0];
            Player2.gameObject.transform.position = PosCamionesCarrera[1];
        }
        else
        {
            Player1.gameObject.transform.position = PosCamionesCarrera[1];
            Player2.gameObject.transform.position = PosCamionesCarrera[0];
        }

        Player1.transform.forward = Vector3.forward;
        Player1.GetComponent<Frenado>().Frenar();
        Player1.CambiarAConduccion();

        Player2.transform.forward = Vector3.forward;
        Player2.GetComponent<Frenado>().Frenar();
        Player2.CambiarAConduccion();

        Player1.GetComponent<Frenado>().RestaurarVel();
        Player2.GetComponent<Frenado>().RestaurarVel();
        Player1.GetComponent<ControlDireccion>().Habilitado = false;
        Player2.GetComponent<ControlDireccion>().Habilitado = false;
        Player1.transform.forward = Vector3.forward;
        Player2.transform.forward = Vector3.forward;

        EstAct = EstadoJuego.Jugando;
    }

    [System.Serializable]
    public class PlayerInfo
    {
        public PlayerInfo(int tipoDeInput, Player pj)
        {
            TipoDeInput = tipoDeInput;
            PJ = pj;
        }

        public bool FinCalibrado = false;
        public bool FinTuto1 = false;
        public bool FinTuto2 = false;

        public Visualizacion.Lado LadoAct;

        public int TipoDeInput = -1;

        public Player PJ;
    }

}