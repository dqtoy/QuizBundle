using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InterativaSystem.Views.HUD.Quiz;

public class EscolhaManager : MonoBehaviour
{
	public Image imgFooter;
	public List<Button> btnAlternatives;

	public List<FamilyAlternativa> famlilys;

    void Start()
    {
        
    }


	public void SetEscolhaID(int id)
	{

		for(int i = 0; i <btnAlternatives.Count; i++)
		{
			btnAlternatives[i].image.sprite = famlilys[id].spriteBtn;
		}

		imgFooter.sprite = famlilys[id].footer;

		for (int i = 0; i < btnAlternatives.Count; i++)
		{
			QuizAswerButton button = btnAlternatives[i].GetComponent<QuizAswerButton>();

			button.Right = famlilys[id].colorRight;
			button.Wrong = famlilys[id].colorWrong;
			button.Selection = famlilys[id].colorSelect;
			button.iniColor = famlilys[id].colorInit;
			button.iniSprite = famlilys[id].spriteBtn;
			Debug.Log("entrou");
		}

		Debug.Log("Change Sprites!");
	}
 
}

[System.Serializable]
public class FamilyAlternativa
{
	public Color colorRight;
	public Color colorWrong;
	public Color colorSelect;
	public Color colorInit;
	public Sprite spriteBtn;
	public Sprite footer;
}

