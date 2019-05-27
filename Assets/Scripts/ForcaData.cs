using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;

public class ForcaData : MonoBehaviour {

	public string dataFolder;
	public string dataName;

	public List<QuestionForca> questions;

	public void SaveData()
	{
		if (File.Exists(dataFolder + "/" + dataName))
		{
			var strfdl = File.ReadAllText(dataFolder + "/" + dataName);
		}

		var str = JsonConvert.SerializeObject(questions, Formatting.Indented);

		var dir = Path.GetDirectoryName(dataFolder + "/" + dataName);
		if (!Directory.Exists(dir))
		{
			Directory.CreateDirectory(dir);
		}

		File.WriteAllText(dataFolder + "/" + dataName, str);
	}

	public void DeserializeDataBase(string json)
	{
		questions = JsonConvert.DeserializeObject<List<QuestionForca>>(json);
	}


	public bool TryLoad()
	{
		if (File.Exists(dataFolder + "/" + dataName))
		{
			var str = File.ReadAllText(dataFolder + "/" + dataName);

			DeserializeDataBase(str);
			return true;
		}
		return false;
	}

}
