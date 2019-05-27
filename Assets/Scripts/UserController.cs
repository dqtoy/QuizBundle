using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : Singleton<UserController>
{

	[HideInInspector]
	public InfoUser infoUser;
	[HideInInspector]
	public UserJN user;

	private void Start()
	{
		Reset();
	}

	public void Reset()
	{
		infoUser = new InfoUser();
		user = new UserJN();
	}

	public void SetUser(string name,string cpf)
	{
		user.nome = name;
		user.cpf = cpf;
	}

	public void SetInfo(int acertos, float time)
	{
		infoUser.user = user;
		infoUser.time = time;
		infoUser.score = acertos;
	}


	public Dictionary<string,string> GetInfo()
	{

		Dictionary<string, string> newData = new Dictionary<string, string>();

		newData.Add("nome", infoUser.user.nome);
		newData.Add("cpf", infoUser.user.cpf);
		newData.Add("time", infoUser.time.ToString().Replace(",","."));
		newData.Add("score", infoUser.score.ToString());

		return newData;
	}
	
}
