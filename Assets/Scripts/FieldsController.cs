using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using System.Globalization;

public class FieldsController : MonoBehaviour {

	public List<GroupField> groupsFields;

	private string _answerCurrent;
	private int lineOne, lineTwo, lineTree;
	private Image _img;

	void Start () {
		ForcaController.Instance.OnGameStart += FillFields;
		ForcaController.Instance.OnReceiveAnswer += CheckLetterAnswer;
		ForcaController.Instance.OnGameEnd += ResetFields;

		lineOne = groupsFields[0].Fields.Count;
		lineTwo = groupsFields[0].Fields.Count + groupsFields[1].Fields.Count;
		lineTree = groupsFields[0].Fields.Count + groupsFields[1].Fields.Count + groupsFields[2].Fields.Count;

	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void FillFields()
	{
		int lineSpace = 0;

		_answerCurrent = ForcaController.Instance.GetCurrentAnswer();

		string[] split = _answerCurrent.Split();

		//TODO dar split na resposta para ser mostrada
		string[] splitResp = _answerCurrent.Split();

		lineSpace = split.Length - 1;
		int qtdTotalSpace = _answerCurrent.Length + lineSpace;
	

		if( qtdTotalSpace <= lineOne)
		{
			groupsFields[0].groupObject.SetActive(true);
			int idField = 0;

			for (int i = 0; i < split.Length; i++)
			{
				for (int j = 0; j < split[i].Length; j++, idField++)
				{
					groupsFields[0].Fields[idField].Field.SetActive(true);
					groupsFields[0].Fields[idField].Txt.text = split[i][j].ToString();
					groupsFields[0].Fields[idField].Txt.DOFade(0.0f, 0.01f);
					groupsFields[0].Fields[idField].Txt.enabled = false;
				}

				ActiveLineSpace(0, idField);
				idField++;

			}
		}
		else if (qtdTotalSpace <= lineTwo)
		{
			groupsFields[0].groupObject.SetActive(true);
			groupsFields[1].groupObject.SetActive(true);

			int idField = 0;
			int groupId = 0;

			for (int i = 0; i < split.Length; i++)
			{
				for (int j = 0; j < split[i].Length; j++, idField++)
				{
					//ODOR  =4  = 
					//SE o tamanho da palavra  - a quantidade de campos da linha for maior do que 0 E J = 0;
					if((split[i].Length - 1) - GetAllFieldsOff(groupId) >= 0 && j == 0)
					{
						groupId++;
						idField = 0;
					}

					groupsFields[groupId].Fields[idField].Field.SetActive(true);
					groupsFields[groupId].Fields[idField].Txt.text = split[i][j].ToString();
					groupsFields[groupId].Fields[idField].Txt.DOFade(0.0f, 0.01f);
					groupsFields[groupId].Fields[idField].Txt.enabled = false;
				}

				ActiveLineSpace(groupId, idField);
				idField++;

			}

		}
		else
		{
			groupsFields[0].groupObject.SetActive(true);
			groupsFields[1].groupObject.SetActive(true);
			groupsFields[2].groupObject.SetActive(true);

			int idField = 0;
			int groupId = 0;

			for (int i = 0; i < split.Length; i++)
			{
				for (int j = 0; j < split[i].Length; j++, idField++)
				{

					if((split[i].Length - 1) - GetAllFieldsOff(groupId) > 0  && j ==0)
					{
						groupId++;
						idField = 0;
					}

					groupsFields[groupId].Fields[idField].Field.SetActive(true);
					groupsFields[groupId].Fields[idField].Txt.text = split[i][j].ToString();
					groupsFields[groupId].Fields[idField].Txt.DOFade(0.0f, 0.01f);
					groupsFields[groupId].Fields[idField].Txt.enabled = false;
				}

				ActiveLineSpace(groupId, idField);
				idField++;

			}
		}

		
	}

	public void ActiveLineSpace(int groupID,int fieldID)
	{
		if(GetAllFieldsOff(groupID) > 0)
		{
			groupsFields[groupID].Fields[fieldID].Field.SetActive(true);
			_img = groupsFields[groupID].Fields[fieldID].Field.GetComponent<Image>();
			_img.enabled = false;
		}

	}
	//retorna o numero de campos que eu tenho baseando-se no index atual
	public int GetAllFieldsOff(int index)
	{
		int aux = 0;

		for(int i = 0; i< groupsFields[index].Fields.Count; i++)
		{
			if (!groupsFields[index].Fields[i].Field.activeSelf)
				aux++;
		}
		return aux;
	}

	public void CheckLetterAnswer(string letter)
	{
		StartCoroutine(ShowLetter(letter));
		/*for (int i = 0; i < groupsFields.Count; i++)
		{
			for(int j = 0; j < groupsFields[i].Fields.Count; j++)
			{
				if (IsActiveField(i, j))
				{
					if (groupsFields[i].Fields[j].Txt.text.ToUpper() == letter.ToUpper())
						ShowLetterCorrect(i,j);
					
				}

			}
		}*/
	}

	public void ShowLetterCorrect(int groupID, int fieldID)
	{
		//funcao para mostrar letra correta
		groupsFields[groupID].Fields[fieldID].Txt.enabled = true;
		groupsFields[groupID].Fields[fieldID].Txt.DOFade(1f, 0.8f);

		//vou criar esse groupfields alternativo
		//// vou 
		//groupsFieldsAlternativo[groupID].Fields[fieldID].Txt.enabled = true;
		//groupsFieldsAlternativo[groupID].Fields[fieldID].Txt.DOFade(1f, 1f);
		AudioManagerr.Instance.PlayLetterSong();

		ForcaController.Instance.letterIsCorrect = true;
		ForcaController.Instance.isOver = CheckAllTxtEnabled();

	}

	public bool CheckAllTxtEnabled()
	{
		for (int i = 0; i < groupsFields.Count; i++)
		{
			if (groupsFields[i].groupObject.activeSelf)
			{
				for (int j = 0; j < groupsFields[i].Fields.Count; j++)
				{
					if (IsActiveField(i, j))
					{
						_img = groupsFields[i].Fields[j].Field.GetComponent<Image>();
						if (_img.isActiveAndEnabled)
						{
							if (!groupsFields[i].Fields[j].Txt.isActiveAndEnabled)
								return false;
						}

					}

				}
			}
		}

		return true;
	}

	public bool IsActiveField(int groupIndex, int fieldIndex)
	{
		if (groupsFields[groupIndex].Fields[fieldIndex].Field.activeSelf)
			return true;
		else
			return false;
	}

	public void ResetFields()
	{

		for (int i = 0; i < groupsFields.Count; i++)
		{
			groupsFields[i].groupObject.SetActive(true);
		}

		for (int i = 0; i < groupsFields.Count; i++)
		{

			//if (groupsFields[i].groupObject.activeSelf)
		//	{
				for (int j = 0; j < groupsFields[i].Fields.Count; j++)
				{
					//if (IsActiveField(i, j))
					//{
						groupsFields[i].Fields[j].Txt.text = "";
						groupsFields[i].Fields[j].Field.GetComponent<Image>().enabled = true;
						groupsFields[i].Fields[j].Field.SetActive(false);

					//}

				}
		//	}
		}

		for(int i = 0; i< groupsFields.Count; i++)
		{
			groupsFields[i].groupObject.SetActive(false);
		}

	}

	IEnumerator ShowLetter(string letter)
	{
		for (int i = 0; i < groupsFields.Count; i++)
		{
			for (int j = 0; j < groupsFields[i].Fields.Count; j++)
			{
				if (IsActiveField(i, j))
				{
				
					
					if (string.Compare(groupsFields[i].Fields[j].Txt.text, letter, CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace)==0 )
					{
						ShowLetterCorrect(i, j);
						yield return new WaitForSeconds(0.8f);
					}

				}

			}
		}

	}

}

[System.Serializable]
public class FieldAnswer
{
	public GameObject Field;
	public Text Txt;
}

[System.Serializable]
public class GroupField
{
	public int GroupID;
	public List<FieldAnswer> Fields;
	public GameObject groupObject;
}