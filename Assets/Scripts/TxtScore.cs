using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TxtScore : MonoBehaviour {

	private Text _txt;
	
	void Start () {
		_txt = GetComponent<Text>();
		ForcaController.Instance.OnGameEnd += GetScoreTxt;
		ForcaController.Instance.OnGameStart += ResetTxt;
	}
	
	public void GetScoreTxt()
	{
		_txt.text = ForcaController.Instance.qtdAnswerRight.ToString();
	}

	public void ResetTxt()
	{
		_txt.text = "";
	}
	
}
