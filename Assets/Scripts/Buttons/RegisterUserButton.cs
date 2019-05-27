using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterUserButton : MonoBehaviour
{
	public Text txtNome, txtCpf;
	private Button btn;

	private void Start()
	{
		btn = GetComponent<Button>();
		btn.onClick.AddListener(() =>
		{
			UserController.Instance.SetUser(txtNome.text, txtCpf.text);
		});
	}
}
