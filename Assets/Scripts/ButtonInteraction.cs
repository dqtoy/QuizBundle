using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteraction : MonoBehaviour {

	private Button _btn;
	private string letter;

	public int buttonId;

	void Start () {

		_btn = GetComponent<Button>();
		letter = _btn.GetComponentInChildren<Text>().text;

		_btn.onClick.AddListener(() =>
	   {
		   ForcaController.Instance.ReceiveLetter(letter, buttonId);
	   });

	}
	
}
