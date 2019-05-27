using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InterativaSystem.Models;

public class ConfigQuizController : Singleton<ConfigQuizController> {

	[HideInInspector]
	public QuizConfig Qconfig;
	[HideInInspector]
	public List<Question> _Questions;

	public delegate void Simple();
	public delegate void SimpleInt(int value);
	public delegate void SimpleBool(bool value);
	public delegate void SimpleSetQuiz(List<Question> questions);
	public delegate void SimpleCampos(int tempo, int qtdSorteadas, int versao, bool showTempo, bool showCorrect, bool showDesempenho,bool random);

	public event Simple SimpleEvent;
	public event SimpleBool FeedBackEvent, SimpleBoolEvent;
	public event SimpleInt SimpleIntEvent, SimpleIntEvent2, SimpleIntEventCall;
	public event SimpleCampos SetRefs;
	public event SimpleSetQuiz SimpleSetQuizEvent;

	public List<GameObject> Campos;

	[HideInInspector]
	public bool ShowCorret = true;

	void Start () {

		//StartConfigQuiz();
	}

	public void GetRefsCampos() {

		Campos[0].SetActive(Qconfig.mostrarTempo);
		ShowCorret = Qconfig.mostrarCorreta;
		Campos[1].SetActive(Qconfig.mostrarDesempenho);

		if (SimpleEvent != null) SimpleEvent();

		if (SimpleBoolEvent != null) SimpleBoolEvent(Qconfig.random);

	}

	public void OnChangeSimpleInt() {

		if (SimpleIntEvent != null) SimpleIntEvent(Qconfig.qtdPerguntasSorteadas);
		if (SimpleIntEvent2 != null) SimpleIntEvent2(Qconfig.tempResposta);
		
	}

	public void StartConfigQuiz() {

		if (JsonConfig.Instance.TryLoad("ConfigQuiz.json")) {
			Debug.Log("ConfigQuiz Carregado");
			ResetConfig();
		}
		else {
			DefaultConfig();
		}

		JsonConfig.Instance.Save<QuizConfig>("ConfigQuiz.json", Qconfig);
	}

	private void DefaultConfig() {

		Qconfig.mostrarTempo = true;
		Qconfig.mostrarCorreta = true;
		Qconfig.mostrarDesempenho = true;
		Qconfig.random = true;
		Qconfig.tempResposta = 30;
		Qconfig.qtdPerguntasSorteadas = 5;
		Qconfig.version = 0;

	}

	public void ResetConfig() {
		GetRefsCampos();
		OnChangeSimpleInt();
	}

	public void ChangeQuizConfig(QuizConfig quizConfig) {

		Qconfig.mostrarTempo = quizConfig.mostrarTempo;
		Qconfig.mostrarCorreta = quizConfig.mostrarCorreta;
		Qconfig.mostrarDesempenho = quizConfig.mostrarDesempenho;
		Qconfig.random = quizConfig.random;

		Qconfig.tempResposta = quizConfig.tempResposta;
		Qconfig.qtdPerguntasSorteadas = quizConfig.qtdPerguntasSorteadas;
		Qconfig.version = quizConfig.version;

		ResetConfig();
		JsonConfig.Instance.Save<QuizConfig>("ConfigQuiz.json", Qconfig);
	}

	public void OnFeedBack(bool flag) 
	{
		if (FeedBackEvent != null) FeedBackEvent(flag);
	}

	public void OnSetRefs(int tempo, int qtdSorteadas, int versao, bool showTempo, bool showCorrect, bool showDesempenho, bool random) 
	{
		if (SetRefs != null) SetRefs(tempo, qtdSorteadas, versao, showTempo, showCorrect,showDesempenho, random);
	}

	public void OnSimpleQuizEvent(List<Question> questions) 
	{
		if (SimpleSetQuizEvent != null) SimpleSetQuizEvent(questions);

	}

	public void OnCallEvent(int index) 
	{
		if (SimpleIntEventCall != null) SimpleIntEventCall(index);
	}

}
