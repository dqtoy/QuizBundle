using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAttQuestionsFeed : MonoBehaviour {

	public Text _txt;
	private Button _btn;
	public string FeedBack;

	void Start () {

		_btn = GetComponent<Button>();

		_btn.onClick.AddListener(ShowFeedBack);
			
	}

	public void ShowFeedBack() 
	{
		Debug.Log("foi1");
		_txt.text = FeedBack;
		StartCoroutine(WaitTime());
	}

	IEnumerator WaitTime() 
	{
		yield return new WaitForSeconds(2f);
		_txt.text = "";
	}

}
