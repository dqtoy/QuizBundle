using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAtt : MonoBehaviour {

	public Text _txt;


	private void Start() {

		ConfigQuizController.Instance.FeedBackEvent += FeedBack;
	}

	public void FeedBack(bool value) {

		//_txt.gameObject.SetActive(true);

		if (value)
			_txt.text = "Já foi Atualizado!";
		else
			_txt.text = " Valores Atualizado!";

		StartCoroutine(FadeZuado());
	}
	
	IEnumerator FadeZuado() 
	{
		yield return new  WaitForSeconds(2f);
		_txt.text = "";
		//_txt.gameObject.SetActive(false);

	}
}
