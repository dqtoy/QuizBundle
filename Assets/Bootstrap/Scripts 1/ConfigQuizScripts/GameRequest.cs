using UnityEngine;
using System.IO;
using LitJson;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using InterativaSystem.Models;
using UnityEngine.Networking;

public class GameRequest : MonoBehaviour {


	private QuizConfig quizConfig;
	private List<Question> questions;

	private const string urlConfig = "http://10.11.1.97:9000/photos/";
	private const string urlQuestions = "http://10.11.1.97:9000/photos";
	// private Questions questions;
	
	public string NumberProject;

    void Start()
    {
		//StartCoroutine(Upload());
    }


	public void UploadRegister (){
		StartCoroutine(Upload());
	}

	IEnumerator Upload() {
		WWWForm form = new WWWForm();

		string jsonRegister = JsonConfig.Instance.TryLoadRegister();
		if(jsonRegister != null) 
		{
			form.AddField("registers", jsonRegister);
			form.AddField("project", NumberProject);
			UnityWebRequest www = UnityWebRequest.Post(urlQuestions, form);

			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError) {
				Debug.Log(www.error);
			}
			else {
				Debug.Log("Form upload complete!");
			}
		}
		else {
			Debug.Log("nao foi");
		}
	
	}

	public  void LoadGameData()
    {
		Debug.Log("ENTRANDO");

        WWW request = new WWW(urlConfig + NumberProject);
        StartCoroutine(OnResponse(request));
	
    }

	public void LoadGameDataQuestions() {
		WWW request = new WWW(urlQuestions);
		StartCoroutine(OnResponseQuestions(request));
	}

	private IEnumerator OnResponse(WWW req)
    {
        yield return req;

        SetAllIData(req);        
    }

	private IEnumerator OnResponseQuestions(WWW req)
	{
		yield return req;

		SetAllIDataQuestions(req);
	}


	private void SetAllIData(WWW req)
    {
		Debug.Log("set");

		quizConfig = JsonConvert.DeserializeObject<QuizConfig>(req.text);

		bool aux = ConfigQuizController.Instance.Qconfig.version >= quizConfig.version;

		if (aux)
			ConfigQuizController.Instance.OnFeedBack(aux);
		else {
			ConfigQuizController.Instance.OnSetRefs(quizConfig.tempResposta,quizConfig.qtdPerguntasSorteadas,quizConfig.version,quizConfig.mostrarTempo,quizConfig.mostrarCorreta,quizConfig.mostrarDesempenho,quizConfig.random);
			ConfigQuizController.Instance.OnFeedBack(aux);
		}

    }

	private void SetAllIDataQuestions(WWW req)
	{
		questions = JsonConvert.DeserializeObject<List<Question>>(req.text);

		if(questions != null) 
		{
			ConfigQuizController.Instance.OnSimpleQuizEvent(questions);
		}

	}






}
