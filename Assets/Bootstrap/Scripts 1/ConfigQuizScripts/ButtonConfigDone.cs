using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InterativaSystem.Views.HUD.ActionCall;

public class ButtonConfigDone : MonoBehaviour {

	private Button _btn;

	public List<InputField> InputFields;
	public List<Toggle> Toggles;

	public GameObject Panel;

	private void Awake() {

		ConfigQuizController.Instance.SetRefs += SetValuesInCampos;

	}

	void Start () {

		_btn = GetComponent<Button>();

		_btn.onClick.AddListener(() => {

			Panel.SetActive(true);

			if (InputFields[0].text == null || InputFields[0].text == "")
				InputFields[0].text = "30";

			if (InputFields[1].text == null || InputFields[1].text == "")
				InputFields[1].text = "5";

			if (InputFields[2].text == null || InputFields[2].text == "")
				InputFields[2].text = "0";

			ConfigQuizController.Instance.ChangeQuizConfig(new QuizConfig {

					mostrarTempo = Toggles[0].isOn,
					mostrarCorreta = Toggles[1].isOn,
					mostrarDesempenho = Toggles[2].isOn,
					random = Toggles[3].isOn,
					tempResposta = int.Parse(InputFields[0].text),
					qtdPerguntasSorteadas = int.Parse(InputFields[1].text),
					version = int.Parse(InputFields[2].text)

			});

		});

	}

	public void SetValuesInCampos(int tempo, int qtdSorteadas, int versao, bool showTempo,bool showCorrect, bool showDesempenho, bool random) 
	{
		InputFields[0].text = tempo.ToString();
		InputFields[1].text = qtdSorteadas.ToString();
		InputFields[2].text = versao.ToString();
		Toggles[0].isOn = showTempo;
		Toggles[1].isOn = showCorrect;
		Toggles[2].isOn = showDesempenho;
		Toggles[3].isOn = random;
	}
	
}
