using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleVisibleCaret : MonoBehaviour {

	public List<InputFieldOnScreenKeyboard> _InputField;
	public int posFirst;
	[Space]
	public Color CaretColor;
	private Color CaretNotFade;
	[Space]
	public List<Text> labels;
	public Color labelInit;
	public Color labelCliked;

	private void Start() {

		CaretNotFade = CaretColor;
		CaretNotFade.a = 0;
		RestartCaretPos();
	}

	public void testeDown(int id) {

		for(int i = 0; i< _InputField.Count; i++) 
		{

			/*if(i == id )
				_InputField[i].caretColor = new Color(0, 0, 0, 1);
			else
				_InputField[i].caretColor = new Color(0, 0, 0, 0);
				*/
			if (i == id)
			{
				_InputField[i].caretColor = CaretColor;
				ClikedOn(i);
			}
			else
			{
				_InputField[i].caretColor = CaretNotFade;
				ClikedOff(i);
			}
		}
	}

	public void ClikedOn(int index)
	{
		_InputField[index].GetComponent<Outline>().enabled = true;
		SetLabelOn(index);
	}

	public void ClikedOff(int index)
	{
		_InputField[index].GetComponent<Outline>().enabled = false;
		SetLabelOff(index);
	}

	public void SetLabelOn(int index)
	{
		labels[index].color = labelCliked;
	}

	public void SetLabelOff(int index)
	{
		labels[index].color = labelInit;
	}

	public void RestartCaretPos() {

		for (int i = 0; i < _InputField.Count; i++) {

		
			if (i == posFirst)
			{
				_InputField[i].caretColor = CaretColor;
				ClikedOn(i);
			}
			else
			{
				_InputField[i].caretColor = CaretNotFade;
				ClikedOff(i);
			}
		}

	}
}
