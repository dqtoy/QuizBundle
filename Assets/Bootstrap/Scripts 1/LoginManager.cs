using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using HeathenEngineering.OSK.v2;

public class LoginManager : MonoBehaviour {

	private InputField inputField;

	public OnScreenKeyboard OSKeyboard;
	public Text FeedBackTxt;

	[SerializeField]
	private string senha;


	void Start () {

		inputField = GetComponent<InputField>();
		if (OSKeyboard != null) {
			OSKeyboard.KeyPressed += new KeyboardEventHandler(KeyboardKeyPressed);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected void KeyboardKeyPressed(OnScreenKeyboard sender, OnScreenKeyboardArguments args) {
		
		int caretPos;

		InputField outputText = inputField;

		switch (args.KeyPressed.type) {
				case KeyClass.Backspace:
					if (outputText.text.Length > 0 || outputText.caretPosition > 0) {
						caretPos = outputText.caretPosition;

						outputText.text = outputText.text.Remove(outputText.caretPosition - 1, 1);

						outputText.caretPosition = caretPos - 1;
					}
					break;
				case KeyClass.Return:
					outputText.text += args.KeyPressed.ToString();
					break;
				case KeyClass.Shift:
					//No need to do anything here as the keyboard will sort that on its own
					break;
				case KeyClass.String:
					if (outputText.characterLimit > 0 && outputText.text.Length >= outputText.characterLimit) break;

					caretPos = outputText.caretPosition;
					string s = outputText.text;
					outputText.text = outputText.text.Insert(outputText.caretPosition, args.KeyPressed.ToString());
					outputText.caretPosition = caretPos + 1;
					break;
			}
	}

	public void CheckSenha() {

		if(senha == inputField.text) 
		{
			inputField.text = null;
			ConfigQuizController.Instance.OnCallEvent(1);
		}
		else {

			FeedBackTxt.text = "Senha Invalida!";
			inputField.text = null;
			StartCoroutine(WaitTwoTime());

		}
	}

	IEnumerator WaitTwoTime() {

		yield return new WaitForSeconds(1f);
		FeedBackTxt.text = "Digite sua senha!";

	}
}
