using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;
using InterativaSystem.Models;


public class JsonConfig : Singleton<JsonConfig> {


	[HideInInspector]
	public string DataFolder;
	[HideInInspector]
	public string DataNameQuestions;
	[HideInInspector]
	public string DataNameReg;

	//public string DataName;

	public void Save<T>(string DataName, T typeData) {

		if (File.Exists(DataFolder + "/" + DataName)) {
			var strfdl = File.ReadAllText(DataFolder + "/" + DataName);
			//UpdateDataBase(strfdl);
		}

		var str =  JsonConvert.SerializeObject(typeData, Formatting.Indented);

		var dir = Path.GetDirectoryName(DataFolder + "/" + DataName);
		if (!Directory.Exists(dir)) {
			Directory.CreateDirectory(dir);
		}

		File.WriteAllText(DataFolder + "/" + DataName, str);

	}

	/*public void SaveQuestions() {

		if (File.Exists(DataFolder + "/" + DataNameQuestions)) {
			var strfdl = File.ReadAllText(DataFolder + "/" + DataNameQuestions);
			//UpdateDataBase(strfdl);
		}

		var str = SerializeDataBaseQuestions();

		var dir = Path.GetDirectoryName(DataFolder + "/" + DataNameQuestions);
		if (!Directory.Exists(dir)) {
			Directory.CreateDirectory(dir);
		}

		File.WriteAllText(DataFolder + "/" + DataNameQuestions, str);

	}*/

	public void UpdateDataBase(string folderData) {

		var data = JsonConvert.DeserializeObject<QuizConfig>(folderData);
		
	}

	public void DeserializeDataBase(string json) {
		
		ConfigQuizController.Instance.Qconfig = JsonConvert.DeserializeObject<QuizConfig>(json);
	}


	public bool TryLoad(string DataFile) {

			if (File.Exists(DataFolder + "/" + DataFile)) {
				var str = File.ReadAllText(DataFolder + "/" + DataFile);

				DeserializeDataBase(str);
				return true;
			}
			return false;
		
	}

	public string TryLoadRegister() {

		Debug.Log(DataFolder);
		Debug.Log(DataNameReg);
		Debug.Log(File.Exists(DataFolder + "/" + DataNameReg));

		if (File.Exists(DataFolder + "/" + DataNameReg)) {
			var str = File.ReadAllText(DataFolder + "/" + DataNameReg);

			string new2Str = str.Replace(System.Environment.NewLine, "");
			return new2Str;
		}
		else {
			Debug.Log("dssd");
			return null;
		}

	}

	public  string SerializeDataBase<T>(T typeData) {

		return JsonConvert.SerializeObject(typeData, Formatting.Indented);
	}

	/*public string SerializeDataBaseQuestions() {

		return JsonConvert.SerializeObject(ConfigQuizController.Instance._Questions, Formatting.Indented);
	}*/
}
