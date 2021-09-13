using UnityEngine;
using System.Collections;

public class Bag : MonoBehaviour
{
	public Pallet.Valores Monto;
	public string TagPlayer = "";
	public Texture2D ImagenInventario;
	Player Pj = null;
	
	bool Desapareciendo;
	public GameObject Particulas;
	public float TiempParts = 2.5f;

	void Start () 
	{
		Monto = Pallet.Valores.Valor2;
		
		if(Particulas != null)
			Particulas.SetActive(false);
	}
	
	void Update ()
	{
		if(Desapareciendo)
		{
			TiempParts -= Time.deltaTime;
			if(TiempParts <= 0)
			{
				GetComponent<Renderer>().enabled = true;
				GetComponent<Collider>().enabled = true;
				
				Particulas.GetComponent<ParticleSystem>().Stop();
				gameObject.SetActive(false);
			}
		}
		
	}
	
	void OnTriggerEnter(Collider coll)
	{
		if(coll.tag == TagPlayer)
		{
			Pj = coll.GetComponent<Player>();
			if(Pj.AgregarBolsa(this))
				Desaparecer();
		}
	}
	
	public void Desaparecer()
	{
		Particulas.GetComponent<ParticleSystem>().Play();
		Desapareciendo = true;
		
		GetComponent<Renderer>().enabled = false;
		GetComponent<Collider>().enabled = false;
		
		if(Particulas != null)
		{
			Particulas.GetComponent<ParticleSystem>().Play();
		}
	}
}
