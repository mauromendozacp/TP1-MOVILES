using UnityEngine;
using System.Collections;

public class ContrTutorial : MonoBehaviour 
{
	public Player Pj;
	public float TiempTuto = 15;
	public float Tempo = 0;
	
	public bool Finalizado = false;
	
	GameManager GM;
	
	//------------------------------------------------------------------//

	// Use this for initialization
	void Start () 
	{
		GM = GameObject.Find("GameMgr").GetComponent<GameManager>();
		
		Pj.ContrTuto = this;
	}
	
	// Update is called once per frame
	void Update () 
	{
		/*
		if(Iniciado)
		{
			if(Tempo < TiempTuto)
			{
				Tempo += Time.deltaTime;
				if(Tempo >= TiempTuto)
				{
					Finalizar();
				}
			}
		}
		*/
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<Player>() == Pj)
			Finalizar();
	}
	
	//------------------------------------------------------------------//
	
	public void Iniciar()
	{
		Pj.GetComponent<Frenado>().RestaurarVel();
	}
	
	public void Finalizar()
	{
		Finalizado = true;
		GM.FinTutorial(Pj.IdPlayer);
		Pj.GetComponent<Frenado>().Frenar();
		Pj.GetComponent<Rigidbody>().velocity = Vector3.zero;
		Pj.VaciarInv();
	}
}
