using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigSoundSfx : MonoBehaviour {

	public AudioSource AudioSfx;
	public Text TextField;
	private Slider slider;

	void Start() {

		slider = GetComponent<Slider>();
		slider.onValueChanged.AddListener(delegate { SoundSfxControl(); });
	}

	public void SoundSfxControl() {

		AudioSfx.volume = slider.value;
		TextField.text = slider.value.ToString();
	}
}
