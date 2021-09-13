using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class VidIntrMgr : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//PARA JUGAR
		if(Input.GetKeyDown(KeyCode.KeypadEnter) || 
		   Input.GetKeyDown(KeyCode.Return) ||
		   Input.GetKeyDown(KeyCode.Mouse0))
		{
            SceneManager.LoadScene(1);
		}
		
		//REINICIAR
		if(Input.GetKeyDown(KeyCode.Mouse1) ||
		   Input.GetKeyDown(KeyCode.Keypad0))
		{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		
		//CIERRA LA APLICACION
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
		
		//CALIBRACION DEL KINECT
		if(Input.GetKeyDown(KeyCode.Backspace))
		{
            SceneManager.LoadScene(3);
		}
	}
}
