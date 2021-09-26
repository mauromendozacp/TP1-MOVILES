using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MngPts : MonoBehaviour 
{
	public float TiempEmpAnims = 2.5f;
	float Tempo = 0;
	
	public Vector2[] DineroPos;
	public Vector2 DineroEsc;
	
	public Vector2 GanadorPos;
	public Vector2 GanadorEsc;
	
	public GameObject Fondo;
	
	public float TiempEspReiniciar = 10;
	
	
	public float TiempParpadeo = 0.7f;
	float TempoParpadeo = 0;
	bool PrimerImaParp = true;
	
	public bool ActivadoAnims = false;

	void Start () 
	{		
		SetGanador();
	}
	
	void Update () 
	{
        if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
        if(Input.GetKeyDown(KeyCode.Backspace))
		{
			//SceneManager.LoadScene(3);
		}		
		
		
		TiempEspReiniciar -= Time.deltaTime;
		if(TiempEspReiniciar <= 0 )
		{
            GameSettings.Instancia.ChangeScene(GameSettings.EstadoJuego2.Menu);
		}
		
		if(ActivadoAnims)
		{
			TempoParpadeo += Time.deltaTime;
			
			if(TempoParpadeo >= TiempParpadeo)
			{
				TempoParpadeo = 0;
				
				if(PrimerImaParp)
					PrimerImaParp = false;
				else
				{
					TempoParpadeo += 0.1f;
					PrimerImaParp = true;
				}
			}
		}
		
		if(!ActivadoAnims)
		{
			Tempo += Time.deltaTime;
			if(Tempo >= TiempEmpAnims)
			{
				Tempo = 0;
				ActivadoAnims = true;
			}
		}
	}
	
	/*void OnGUI()
	{
		if(ActivadoAnims)
		{
			SetDinero();
			SetCartelGanador();
		}
		
		GUI.skin = null;
	}*/

	void SetGanador()
	{
		/*switch(DatosPartida.LadoGanadaor)
		{
		case DatosPartida.Lados.Der:
			GS_Ganador.box.normal.background = Ganadores[1];
			
			break;
			
		case DatosPartida.Lados.Izq:
			GS_Ganador.box.normal.background = Ganadores[0];
			
			break;
		}*/
	}
	
	
	public void DesaparecerGUI()
	{
		ActivadoAnims = false;
		Tempo = -100;
	}
}
