using UnityEngine;
using System.IO;
using LitJson;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq;
using UnityEngine.Networking;
using System.Text;

public class ScreenGameRequest : Singleton<ScreenGameRequest>
{
	public string BaseUrl = "http://10.11.1.59:9000/";

	private string urlGetMediaP = "pontuacoes/media/";
	private string urlPostUser = "pontuacoes";

	public void PostUser()
	{
		StartCoroutine(WaitPost(UserController.Instance.GetInfo()));
	}

	IEnumerator WaitPost(Dictionary<string,string> data)
	{
		//JsonData dataJson = JsonMapper.ToJson(infoUser);

		//JSONObject json = new JSONObject(dataJson.ToJson());


		//string jsonData = JsonConvert.SerializeObject(infoUser, Formatting.Indented);
		//string data = jsonData.Replace(System.Environment.NewLine, "");


		//WWWForm formData = new WWWForm();
		//formData.AddField("cpf", );

		UnityWebRequest www = UnityWebRequest.Post(BaseUrl + urlPostUser,data);
		//www.SetRequestHeader("Content-Type","application/json");

		yield return www.SendWebRequest();

		if(www.isNetworkError || www.isHttpError)
		{
			Debug.Log(www.error);
		}
		else
		{

			Debug.Log("Upload Sucess!");
			Debug.Log(www.downloadHandler.text);

			List<UserRanking> users =  new List<UserRanking>();
			users = Mapper.MapCreateList<UserRanking>(www.downloadHandler.text, users);

			ScreenRankingManager.Instance.OnSetRanks(users);

			Debug.Log("Upload Sucess!");
		}
	}
}


[System.Serializable]
public class UserRanking
{
	public string nome;
	public int score;
	public float time;
}
