using UnityEngine;

public class Interruptores : MonoBehaviour 
{
	public string TagPlayer = "Player";
	
	public GameObject[] AActivar;
	
	public bool Activado = false;
	
	void OnTriggerEnter(Collider other) 
	{
		if(!Activado)
		{
			if(other.CompareTag(TagPlayer))
			{
				Activado = true;
				print("activado interrutor");
				for(int i = 0; i < AActivar.Length; i++)
				{
					AActivar[i].SetActive(true);
				}
			}
		}
	}
}
