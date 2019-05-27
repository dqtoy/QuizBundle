using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InterativaSystem.Views.HUD.ActionCall;

public class ButtonConfigEnter : MonoBehaviour {

	private int cont = 0;

	public GameObject Panel;

	public void ClicksOpen() 
	{
		cont++;

		if (cont == 2) 
		{
			cont = 0;
			Panel.SetActive(false);
		}
			

	}

	public void ResetConts() {
		cont = 0;
		Panel.SetActive(true);
	}
	
	
}
