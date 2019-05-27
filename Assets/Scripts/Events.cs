using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour {

	public delegate void SimpleEvent();
	public delegate void SimpleIntEvent(int var);
	public delegate void SimpleStringEvent(string var);
	public delegate void RegisterEvent(string key, string value, bool updateTime);
	
}
