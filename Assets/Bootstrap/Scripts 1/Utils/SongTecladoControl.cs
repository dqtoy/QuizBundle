using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SongTecladoControl : Singleton<SongTecladoControl> {

	private AudioSource audioSorce;

	void Start () {

		audioSorce = GetComponent<AudioSource>();
	}
	
	public void PlaySongKeyboard() {

		if(audioSorce.isPlaying)
			audioSorce.Stop();


		audioSorce.Play();

	}
}
