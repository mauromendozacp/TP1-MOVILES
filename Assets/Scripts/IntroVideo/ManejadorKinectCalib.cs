using UnityEngine;
using UnityEngine.SceneManagement;

public class ManejadorKinectCalib : MonoBehaviour 
{
	public GameObject[] ParaAct;

	void Start ()
	{
		for(int i = 0; i < ParaAct.Length; i++)
		{
			ParaAct[i].SetActive(false);
		}
	}
	
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Keypad1))
		{
			for(int i = 0; i < ParaAct.Length; i++)
			{
				ParaAct[i].SetActive(false);
			}
			
			if(ParaAct.Length >= 1)
				ParaAct[0].SetActive(true);
		}
		if(Input.GetKeyDown(KeyCode.Keypad2))
		{
			for(int i = 0; i < ParaAct.Length; i++)
			{
				ParaAct[i].SetActive(false);
			}
			
			if(ParaAct.Length >= 2)
				ParaAct[1].SetActive(true);
		}
		if(Input.GetKeyDown(KeyCode.Keypad3))
		{
			for(int i = 0; i < ParaAct.Length; i++)
			{
				ParaAct[i].SetActive(false);
			}
			
			if(ParaAct.Length >= 3)
				ParaAct[2].SetActive(true);
		}
		
		if(Input.GetKeyDown(KeyCode.Return) ||
		   Input.GetKeyDown(KeyCode.Backspace) ||
		   Input.GetKeyDown(KeyCode.KeypadEnter) ||
		   Input.GetKeyDown(KeyCode.Mouse0))
		{
            SceneManager.LoadScene(0);
		}
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
		if(Input.GetKeyDown(KeyCode.Mouse1) ||
		   Input.GetKeyDown(KeyCode.Keypad0))
		{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
