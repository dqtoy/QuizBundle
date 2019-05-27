using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ForcaController : Singleton<ForcaController> {
	// Events 
	public event Events.SimpleEvent OnGameStart, OnGameEnd;
	public event Events.SimpleIntEvent OnReceiveLetter,CallAction;
	public event Events.SimpleStringEvent OnReceiveAnswer;
	public event Events.RegisterEvent OnRegister;

	private ForcaData _forcaData;
	private int[] _orderQuestion;

	[HideInInspector]
	public bool letterIsCorrect;
	[HideInInspector]
	public int currentQuestionId = -1;
	[HideInInspector]
	public int qtdAnswerRight = 6;
	[HideInInspector]
	public bool isOver;

	public Text TxtQuestion;
	public int questionsLimit;
	public float endTime;
	public List<Sprite> spritesForca;
	public Image forcaContainer;

	private void Start()
	{
		_forcaData = GetComponent<ForcaData>();
		currentQuestionId = -1;
	}

	public void GameStart()
	{
		if (!_forcaData.TryLoad())
			Debug.LogError("Data not found");
		//sdsds

		if (currentQuestionId == _forcaData.questions.Count - 1)
			currentQuestionId = -1;

		currentQuestionId++;
		//Debug.Log(currentQuestionId);

		forcaContainer.sprite = spritesForca[0];
		RandomQuestions();
		isOver = false;
		qtdAnswerRight = 6;

		if (OnGameStart != null) OnGameStart();
		GetCurrentQuestion();
	}

	public void GetCurrentSprite()
	{
		switch (qtdAnswerRight)
		{
			case 0:
				forcaContainer.sprite = spritesForca[6];
				break;
			case 1:
				forcaContainer.sprite = spritesForca[5];
				break;
			case 2:
				forcaContainer.sprite = spritesForca[4];
				break;
			case 3:
				forcaContainer.sprite = spritesForca[3];
				break;
			case 4:
				forcaContainer.sprite = spritesForca[2];
				break;
			case 5:
				forcaContainer.sprite = spritesForca[1];
				break;
		}
	}

	private void RandomQuestions()
	{
		//_orderQuestion = _Shuffle.Shuffle(_forcaData.questions.Count);
		//_orderQuestion = new int[_forcaData.questions.Count];
		//Debug.Log(_orderQuestion.Length);
	}

	public void ReceiveLetter(string letter, int buttonId)
	{
		letterIsCorrect = false;
		if (OnReceiveAnswer != null) OnReceiveAnswer(letter);
		if (OnReceiveLetter != null) OnReceiveLetter(buttonId);

		StartCoroutine(WaitingEnd(endTime));
	}

	public void GameEnd()
	{
		ToCallResult();
		if (OnGameEnd != null) OnGameEnd();
	}

	public void ToCallResult()
	{
		if(qtdAnswerRight > 0  && !TimeForcaController.Instance.timeOut)
		{
			CallAction(1);
			if (OnRegister != null) OnRegister("Win", "true", false);
		}
		else
		{
			CallAction(0);
			if (OnRegister != null) OnRegister("Win", "false", false);
		}
		//CallAction(qtdAnswerRight > 0 ? 1 : 0);
	}

	public void GetCurrentQuestion()
	{
		//TxtQuestion.text = _forcaData.questions[_orderQuestion[currentQuestionId]].Question;
		TxtQuestion.text = _forcaData.questions[currentQuestionId].Question;
	}

	public string GetCurrentAnswer()
	{
		//return _forcaData.questions[_orderQuestion[currentQuestionId]].Answer;
		return _forcaData.questions[currentQuestionId].Answer;
	}

	public int GetCurrentID()
	{
		//return _forcaData.questions[_orderQuestion[currentQuestionId]].Id;
		return _forcaData.questions[currentQuestionId].Id;
	}

	IEnumerator WaitingEnd(float time)
	{
		yield return new WaitForSeconds(time);
		if (qtdAnswerRight <= 0 || isOver)
			GameEnd();
	}

}
