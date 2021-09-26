using UnityEngine;
using System.Collections;

public class ContrCalibracion : MonoBehaviour
{
	public Player Pj;

    bool tutorialTerminado = false;
    int faseTutorial = 0;
    bool moving = false;
    [SerializeField] Vector3[] pos;
    [SerializeField] GameObject bolsa;
    [SerializeField] Sprite[] images;
    [SerializeField] SpriteRenderer image;
    enum Teclas
    {
        ASDW,
        FLECHAS
    }

    [SerializeField] Teclas t;

    [SerializeField] GameObject[] botones;

	/*
	public string ManoIzqName = "Left Hand";
	public string ManoDerName = "Right Hand";
	
	bool StayIzq = false;
	bool StayDer = false;
	*/
	/*
	public float TiempCalib = 3;
	float Tempo = 0;
	*/
	public float TiempEspCalib = 3;
	float Tempo2 = 0;
	
	//bool EnTutorial = false;
	
	public enum Estados{Calibrando, Tutorial, Finalizado}
	public Estados EstAct = Estados.Calibrando;
	
	public ManejoPallets Partida;
	public ManejoPallets Llegada;
	public Pallet P;
    public ManejoPallets palletsMover;
	
	GameManager GM;
	
	//----------------------------------------------------//
	
	// Use this for initialization
	void Start () 
	{
        /*
		renderer.enabled = false;
		collider.enabled = false;
		*/
        palletsMover.enabled = false;
        Pj.ContrCalib = this;
		
		GM = GameObject.Find("GameMgr").GetComponent<GameManager>();
		
		P.CintaReceptora = Llegada.gameObject;
		Partida.Recibir(P);
		
		SetActivComp(false);
	}

	void Update()
	{
		if (!tutorialTerminado)
			switch (t)
			{
				case Teclas.ASDW:
					if (!moving)
					{
						if (faseTutorial == 0)
						{
							if (Input.GetKeyDown(KeyCode.W))
							{
								image.sprite = images[0];
								bolsa.gameObject.SetActive(true);
								faseTutorial = 1;
							}
						}
						else if (faseTutorial == 1)
						{
							if (Input.GetKeyDown(KeyCode.A))
							{
								image.sprite = images[1];
								faseTutorial = 2;
								StartCoroutine(Move());
							}
						}
						else if (faseTutorial == 2)
						{
							if (Input.GetKeyDown(KeyCode.S))
							{
								image.sprite = images[2];
								faseTutorial = 3;
							}
						}
						else if (faseTutorial == 3)
						{
							if (Input.GetKeyDown(KeyCode.D))
							{
								StartCoroutine(Move());
								image.sprite = images[3];
							}
						}
					}
					break;
				case Teclas.FLECHAS:
					if (!moving)
					{
						if (faseTutorial == 0)
						{
							if (Input.GetKeyDown(KeyCode.UpArrow))
							{
								image.sprite = images[0];
								bolsa.gameObject.SetActive(true);
								faseTutorial = 1;
							}
						}
						else if (faseTutorial == 1)
						{
							if (Input.GetKeyDown(KeyCode.LeftArrow))
							{
								image.sprite = images[1];
								faseTutorial = 2;
								StartCoroutine(Move());
							}
						}
						else if (faseTutorial == 2)
						{
							if (Input.GetKeyDown(KeyCode.DownArrow))
							{
								image.sprite = images[2];
								faseTutorial = 3;
							}
						}
						else if (faseTutorial == 3)
						{
							if (Input.GetKeyDown(KeyCode.RightArrow))
							{
								StartCoroutine(Move());
								image.sprite = images[3];
							}
						}
					}
					break;
			}

        if (EstAct == ContrCalibracion.Estados.Tutorial)
        {
            if (Tempo2 < TiempEspCalib)
            {
                Tempo2 += Time.deltaTime;
                if (Tempo2 > TiempEspCalib)
                {
                    SetActivComp(true);
                }
            }
        }
	}
	bool boton0tocado;
	bool boton1tocado;
	bool boton2tocado;
	bool boton3tocado;
	public void TocadoBoton(int b)
	{
		if (b == 0 && !boton0tocado)
		{
			image.sprite = images[0];
			bolsa.gameObject.SetActive(true);
			faseTutorial = 1;
			boton0tocado = true;
			botones[1].gameObject.SetActive(true);
		}
		else if (b == 1 && !boton1tocado)
		{
			image.sprite = images[1];
			faseTutorial = 2;
			StartCoroutine(Move());
			boton1tocado = true;
			botones[2].gameObject.SetActive(true);
		}
		else if (b == 2 && !boton2tocado)
		{
			image.sprite = images[2];
			faseTutorial = 3;
			boton2tocado = true;
			botones[3].gameObject.SetActive(true);
		}
		else if (b == 3 && !boton3tocado)
		{
			StartCoroutine(Move());
			image.sprite = images[3];
			boton3tocado = true;

            GameManager.Instancia.SetPlayer(Pj.IdPlayer);
			GameManager.Instancia.FinTutorial(Pj.IdPlayer);
        }
	}

	IEnumerator Move()
	{
		moving = true;
		while (bolsa.transform.position != pos[faseTutorial])
		{
			bolsa.transform.position = Vector3.MoveTowards(bolsa.transform.position, pos[faseTutorial], 100 * Time.deltaTime);
			yield return null;
		}
		moving = false;

		if (faseTutorial == 3)
			tutorialTerminado = true;
		StopCoroutine(Move());
	}

	public bool GetTutorialTerminado()
	{
		return tutorialTerminado;
	}
	/*
	void OnTriggerStay(Collider coll)
	{
		if(coll.name == ManoIzqName)
			StayIzq = true;
		else if(coll.name == ManoDerName)
			StayDer = true;
	}
	
	void OnTriggerExit(Collider coll)
	{
		if(coll.name == ManoIzqName || coll.name == ManoDerName)
			Reiniciar();
	}
	*/
	//----------------------------------------------------//
	/*
	void Reiniciar()
	{
		bool StayIzq = false;
		bool StayDer = false;
		Tempo = 0;
	}
	
	void PrenderVolante()
	{
		VolanteEncendido = true;
		renderer.enabled = true;
		collider.enabled = true;
	}
	*/
	
	void FinCalibracion()
	{
		/*
		Reiniciar();
		GM.CambiarATutorial(Pj.IdPlayer);
		*/
	}
	
	public void IniciarTesteo()
	{
		EstAct = ContrCalibracion.Estados.Tutorial;
        palletsMover.enabled = true;
        //Reiniciar();
    }
	
	public void FinTutorial()
	{
		EstAct = ContrCalibracion.Estados.Finalizado;
        palletsMover.enabled = false;
        GM.FinCalibracion(Pj.IdPlayer);
	}
	
	void SetActivComp(bool estado)
	{
		if(Partida.GetComponent<Renderer>() != null)
			Partida.GetComponent<Renderer>().enabled = estado;
		Partida.GetComponent<Collider>().enabled = estado;
		if(Llegada.GetComponent<Renderer>() != null)
			Llegada.GetComponent<Renderer>().enabled = estado;
		Llegada.GetComponent<Collider>().enabled = estado;
		P.GetComponent<Renderer>().enabled = estado;
	}
}
