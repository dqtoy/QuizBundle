using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigSoundBgm : MonoBehaviour {

	public AudioSource AudioBgm;
	public Text TextField;
	private Slider slider;

	void Start () {

		slider = GetComponent<Slider>();

		slider.onValueChanged.AddListener(delegate { SoundBgmControl(); });
	}
	
	public void SoundBgmControl() 
	{
		AudioBgm.volume = slider.value;
		TextField.text = slider.value.ToString();
	}
}
