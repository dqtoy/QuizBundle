using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardsController : MonoBehaviour {

	public List<Button> buttonsKeys;
	public Sprite buttonCorrect;
	public Sprite buttonWrong;
	public Sprite buttonRaw;

	public Color colorDefault;
	public Color colorClick;

	void Start ()
	{
		ForcaController.Instance.OnGameEnd += ResetImageButtons;
		ForcaController.Instance.OnReceiveLetter += ChangeButtonImage;
	}
	
	public void ChangeButtonImage(int index)
	{
		if (ForcaController.Instance.letterIsCorrect)
		{
			buttonsKeys[index].image.sprite = buttonCorrect;
			DisableInteractable(index);
			AudioManagerr.Instance.PlayRightSong();
		}
		else
		{
			ForcaController.Instance.qtdAnswerRight--;
			buttonsKeys[index].image.sprite = buttonWrong;
			DisableInteractable(index);
			AudioManagerr.Instance.PlayWrongSong();
			ForcaController.Instance.GetCurrentSprite();
		}
		buttonsKeys[index].GetComponentInChildren<Text>().color = colorClick;
	}

	public void DisableInteractable(int index)
	{
		buttonsKeys[index].interactable = false;
	}

	public void ResetImageButtons()
	{
		for(int i = 0; i< buttonsKeys.Count; i++)
		{
			buttonsKeys[i].image.sprite = buttonRaw;
		}

		ActiveInteractableAll();
	}

	public void ActiveInteractableAll()
	{
		for (int i = 0; i < buttonsKeys.Count; i++)
		{
			buttonsKeys[i].interactable = true;
			buttonsKeys[i].GetComponentInChildren<Text>().color = colorDefault;
		}
	}
	


	
}
