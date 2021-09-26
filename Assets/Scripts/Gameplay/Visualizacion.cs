using UnityEngine;

public class Visualizacion : MonoBehaviour 
{
	public enum Lado {Izq, Der}
	public Lado LadoAct;
	
	ControlDireccion Direccion;
	Player Pj;
	
	public Camera CamCalibracion;
	public Camera CamConduccion;
	public Camera CamDescarga;
	
	public Vector2[]DinPos;
	public Vector2 DinEsc = Vector2.zero;
	
	public GUISkin GS_Din;
	
	public Vector2[] VolantePos;
	public float VolanteEsc = 0;
	
	public GUISkin GS_Volante;
	
	public Vector2[] FondoPos;
	public Vector2 FondoEsc = Vector2.zero;

	public Texture2D TexturaVacia;
	public Texture2D TextFondo;
	
	public float Parpadeo = 0.8f;
	public float TempParp = 0;
	public bool PrimIma = true;
	
	public Texture2D[] TextInvIzq;
	public Texture2D[] TextInvDer;
	
	public GUISkin GS_Inv;
	
	public Vector2 BonusPos = Vector2.zero;
	public Vector2 BonusEsc = Vector2.zero;
	
	public Color32 ColorFondoBolsa;	
	public Vector2 ColorFondoPos = Vector2.zero;
	public Vector2 ColorFondoEsc = Vector2.zero;
	
	public Vector2 ColorFondoFondoPos = Vector2.zero;
	public Vector2 ColorFondoFondoEsc = Vector2.zero;
	
	public GUISkin GS_FondoBonusColor;
	public GUISkin GS_FondoFondoBonusColor;
	public GUISkin GS_Bonus;
	
	public Vector2 ReadyPos = Vector2.zero;
	public Vector2 ReadyEsc = Vector2.zero;
	public Texture2D[] ImagenesDelTuto;
	public float Intervalo = 0.8f;
	float TempoIntTuto = 0;
	int EnCurso = -1;
	public Texture2D ImaEnPosicion;
	public Texture2D ImaReady;
	public GUISkin GS_TutoCalib;	
	
	public Texture2D TextNum1; 
	public Texture2D TextNum2;
	public GameObject Techo;
	
	Rect R;

	void Start () 
	{
		TempoIntTuto = Intervalo;
		Direccion = GetComponent<ControlDireccion>();
		Pj = GetComponent<Player>();
	}

	void Update () 
	{
		
	}
	
	void OnGUI()
	{	
		/*switch(Pj.EstAct)
		{
		case Player.Estados.EnConduccion:
			SetInv3();
			SetDinero();
			SetVolante();
			break;
			
		case Player.Estados.EnDescarga:
			SetInv3();
			SetBonus();
			SetDinero();			
			break;
			
		case Player.Estados.EnCalibracion:
			break;
			
		case Player.Estados.EnTutorial:
			SetInv3();
			SetTuto();
			SetVolante();
			break;
		}
		
		GUI.skin = null;*/
	}

	public void CambiarACalibracion()
	{
		CamCalibracion.enabled = true;
		CamConduccion.enabled = false;
		CamDescarga.enabled = false;
	}
	
	public void CambiarATutorial()
	{
		CamCalibracion.enabled = false;
		CamConduccion.enabled = true;
		CamDescarga.enabled = false;
	}
	
	public void CambiarAConduccion()
	{
		CamCalibracion.enabled = false;
		CamConduccion.enabled = true;
		CamDescarga.enabled = false;
	}
	
	public void CambiarADescarga()
	{
		CamCalibracion.enabled = false;
		CamConduccion.enabled = false;
		CamDescarga.enabled = true;
	}
	
	public void SetLado(Lado lado)
	{
		LadoAct = lado;
		
		Rect r = new Rect();
		r.width = CamConduccion.rect.width;
		r.height = CamConduccion.rect.height;
		r.y = CamConduccion.rect.y;
		
		switch (lado)
		{
		case Lado.Der:
			r.x = 0.5f;
			break;
			
			
		case Lado.Izq:
			r.x = 0;
			break;
		}
		
		CamCalibracion.rect = r;
		CamConduccion.rect = r;
		CamDescarga.rect = r;
		
		if(LadoAct == Lado.Izq)
		{
			Techo.GetComponent<Renderer>().material.mainTexture = TextNum1;
		}
		else
		{
			Techo.GetComponent<Renderer>().material.mainTexture = TextNum2;
		}
	}
	
	void SetBonus()
	{
		if(Pj.ContrDesc.PEnMov != null)
		{
			GUI.skin = GS_FondoFondoBonusColor;
			
			R.width = ColorFondoFondoEsc.x *Screen.width /100;
			R.height = ColorFondoFondoEsc.y *Screen.height /100;
			R.x = ColorFondoFondoPos.x *Screen.width /100;
			R.y = ColorFondoFondoPos.y *Screen.height /100;
			if(LadoAct == Lado.Der)
				R.x += Screen.width / 2 ;	
			GUI.Box(R, "");
			
			GUI.skin = GS_FondoBonusColor;
			
			R.width = ColorFondoEsc.x *Screen.width /100;
			R.height = (ColorFondoEsc.y *Screen.height /100) * (Pj.ContrDesc.Bonus / (int)Pallet.Valores.Valor2);
			R.x = ColorFondoPos.x *Screen.width /100;
			R.y = (ColorFondoPos.y *Screen.height /100) - R.height;
            if (LadoAct == Lado.Der)
                R.x += Screen.width / 2;
            GUI.Box(R, "");
			
			GUI.skin = GS_Bonus;
		
			R.width = BonusEsc.x *Screen.width /100;
			R.height = R.width /2;
			R.x = BonusPos.x *Screen.width /100;
			R.y = BonusPos.y *Screen.height /100;
			if(LadoAct == Lado.Der)
				R.x += Screen.width / 2;
			GUI.Box(R, "     $" + Pj.ContrDesc.Bonus.ToString("0"));
		}
	}
	
	void SetDinero()
	{
		GUI.skin = GS_Din;
		
		R.width = DinEsc.x *Screen.width /100;
		R.height = DinEsc.y *Screen.height /100;
		R.x = DinPos[0].x *Screen.width /100;
		R.y = DinPos[0].y *Screen.height /100;
		if(LadoAct == Lado.Der)
			R.x = DinPos[1].x *Screen.width /100;
		GUI.Box(R, "$" + PrepararNumeros(Pj.Dinero));
	}
	
	void SetTuto()
	{
		if(Pj.ContrTuto.Finalizado)
		{
			GUI.skin = GS_TutoCalib;
			
			R.width = ReadyEsc.x *Screen.width /100;
			R.height = ReadyEsc.y *Screen.height /100;
			R.x = ReadyPos.x *Screen.width /100;
			R.y = ReadyPos.y *Screen.height /100;
			if(LadoAct == Visualizacion.Lado.Der)
				R.x = (Screen.width) - R.x - R.width;
			
			GUI.Box(R,"ESPERANDO AL OTRO JUGADOR");
		}
	}
	
	void SetVolante()
	{
		GUI.skin = GS_Volante;
		
		R.width = VolanteEsc * Screen.width /100;
		R.height = VolanteEsc * Screen.width /100;
		R.x = VolantePos[0].x *Screen.width /100;
		R.y = VolantePos[0].y *Screen.height /100;
		
		if(LadoAct == Lado.Der)
			R.x = VolantePos[1].x *Screen.width /100;
		
		Vector2 centro;
		centro.x = R.x + R.width/2;
		centro.y = R.y + R.height/2;
		float angulo = 100 * Direccion.GetGiro();
		
		GUIUtility.RotateAroundPivot(angulo, centro);
				
		GUI.Box(R,"");
		
		GUIUtility.RotateAroundPivot(angulo*(-1), centro);
	}
	
	void SetInv3()
	{
		GUI.skin = GS_Inv;
		
		R.width = FondoEsc.x * Screen.width /100;
		R.height = FondoEsc.y * Screen.width /100;
		R.x = FondoPos[0].x * Screen.width /100;
		R.y = FondoPos[0].y * Screen.height /100;
		
		int contador = 0;
		for(int i = 0; i < 3; i++)
		{
			if(Pj.Bags[i]!=null)
				contador++;
		}
		
		if(LadoAct == Visualizacion.Lado.Der)
		{
			//R.x = (Screen.width) - (Screen.width/2) - R.x;
			R.x = FondoPos[1].x * Screen.width /100;
			
			if(contador < 3)
				GS_Inv.box.normal.background = TextInvDer[contador];
			else
			{
				TempParp += T.GetDT();
				
				if(TempParp >= Parpadeo)
				{
					TempParp = 0;
					if(PrimIma)
						PrimIma = false;
					else
						PrimIma = true;
				}
				
				if(PrimIma)
				{
					GS_Inv.box.normal.background = TextInvDer[3];
				}
				else
				{
					GS_Inv.box.normal.background = TextInvDer[4];
				}
				
			}
		}
		else
		{
			if(contador < 3)
				GS_Inv.box.normal.background = TextInvIzq[contador];
			else
			{
				TempParp += T.GetDT();
				
				if(TempParp >= Parpadeo)
				{
					TempParp = 0;
					if(PrimIma)
						PrimIma = false;
					else
						PrimIma = true;
				}
				
				if(PrimIma)
				{
					GS_Inv.box.normal.background = TextInvIzq[3];
				}
				else
				{
					GS_Inv.box.normal.background = TextInvIzq[4];
				}
			}
		}
		
		GUI.Box(R,"");
	}
	
	public string PrepararNumeros(int dinero)
	{
		string strDinero = dinero.ToString();
		string res = "";
		
		if(dinero < 1)//sin ditero
		{
			res = "";
		}else if(strDinero.Length == 6)//cientos de miles
		{
			for(int i = 0; i < strDinero.Length; i++)
			{
				res += strDinero[i];
				
				if(i == 2)
				{
					res += ".";
				}
			}
		}else if(strDinero.Length == 7)//millones
		{
			for(int i = 0; i < strDinero.Length; i++)
			{
				res += strDinero[i];
				
				if(i == 0 || i == 3)
				{
					res += ".";
				}
			}
		}
		
		return res;
	}
	
	
	
}
