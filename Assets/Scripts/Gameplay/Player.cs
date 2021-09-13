using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public int Dinero = 0;
	public int IdPlayer = 0;
	
	public Bag[] Bags;
	int CantBolsAct = 0;
	public string TagBolsas = "";
	
	public enum Estados{EnDescarga, EnConduccion, EnCalibracion, EnTutorial}
	public Estados EstAct = Estados.EnConduccion;
	
	public bool EnConduccion = true;
	public bool EnDescarga = false;
	
	public ControladorDeDescarga ContrDesc;
	public ContrCalibracion ContrCalib;
	public ContrTutorial ContrTuto;
	
	Visualizacion MiVisualizacion;
	void Start () 
	{
		for(int i = 0; i< Bags.Length;i++)
			Bags[i] = null;
		
		MiVisualizacion = GetComponent<Visualizacion>();
	}
	
	void Update () 
	{
	
	}
	
	public bool AgregarBolsa(Bag b)
    {
        if(CantBolsAct + 1 <= Bags.Length)
		{
			Bags[CantBolsAct] = b;
			CantBolsAct++;
			Dinero += (int)b.Monto;
			b.Desaparecer();
			return true;
		}

        return false;
    }
	
	public void VaciarInv()
	{
		for(int i = 0; i< Bags.Length;i++)
			Bags[i] = null;
		
		CantBolsAct = 0;
	}
	
	public bool ConBolasas()
	{
		for(int i = 0; i< Bags.Length;i++)
		{
			if(Bags[i] != null)
			{
				return true;
			}
		}
		return false;
	}
	
	public void SetContrDesc(ControladorDeDescarga contr)
	{
		ContrDesc = contr;
	}
	
	public ControladorDeDescarga GetContr()
	{
		return ContrDesc;
	}
	
	public void CambiarACalibracion()
	{
		MiVisualizacion.CambiarACalibracion();
		EstAct = Estados.EnCalibracion;
	}
	
	public void CambiarATutorial()
	{
		MiVisualizacion.CambiarATutorial();
		EstAct = Estados.EnTutorial;
		ContrTuto.Iniciar();
	}
	
	public void CambiarAConduccion()
	{
		MiVisualizacion.CambiarAConduccion();
		EstAct = Estados.EnConduccion;
	}
	
	public void CambiarADescarga()
	{
		MiVisualizacion.CambiarADescarga();
		EstAct = Estados.EnDescarga;
	}
	
	public void SacarBolasa()
	{
		for(int i = 0; i < Bags.Length; i++)
		{
			if(Bags[i] != null)
			{
				Bags[i] = null;
				return;
			}				
		}
	}
}
