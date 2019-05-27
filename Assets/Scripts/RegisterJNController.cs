using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterJNController : Singleton<RegisterJNController>
{
	public event Events.RegisterEvent OnRegister;
	public event Events.SimpleIntEvent OnPage;
	

	public void AddRegister(string key, string value)
	{
		if (OnRegister != null) OnRegister(key, value, false);
	}

	public void OnPageOpenId(int id)
	{
		if (OnPage != null) OnPage(id);
	}
   
}
