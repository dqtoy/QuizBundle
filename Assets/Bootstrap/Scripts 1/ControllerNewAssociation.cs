using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;


public class ControllerNewAssociation : Singleton<ControllerNewAssociation> {

	public int qtdContainers;

	public delegate void GetPos();
	public event GetPos resetPos, getResources, Endd;

	public Dictionary<int, timeAndBool> associationAnswers;

	public AudioSource CorrectSong, LoseSong;

	//public List<pieceAssociation> pieces;
	public List<groupPieces> groupPiece;
	private List<groupPiecesJson> piecesJson;

	public List<Image> imgRefs;
	public List<Image> panels;
	public List<Text> txtRefs;

	[HideInInspector]
	public int checkQtd = 0;
	[HideInInspector]
	public bool ended = false;
	[HideInInspector]
	public bool open = false;
	[HideInInspector]
	public int questionCounts = 0, questionsLimit = 0;
	[HideInInspector]
	public float timeAssociation;

	[HideInInspector]
	public int auxPieceId = -1, auxAssociaitionId;

	private int[] pieceRandom, panelsRandom,gpRandom;
	private string jsonString;

	void Start () {

		GetJson();
		pieceRandom = new int[5];
		panelsRandom = new int[5];
		gpRandom = new int[5];

		gpRandom = _Shuffle.Shuffle();
		associationAnswers = new Dictionary<int, timeAndBool>();
		StartGame();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void  GetJson() 
	{
		jsonString = Resources.Load("Associations").ToString();
		piecesJson = JsonConvert.DeserializeObject<List<groupPiecesJson>>(jsonString);

		for(int i = 0; i < piecesJson.Count; i++) 
		{
			groupPieces gps = new groupPieces {
				pieces = new List<pieceAssociation>()
			};
			for (int j = 0; j < piecesJson.Count; j++) 
			{

			

				/*pieceAssociation pc = new pieceAssociation {

					pieceImage = Resources.Load<Sprite>(piecesJson[i].pieces[j].Sprite),
					Id = int.Parse(piecesJson[i].pieces[j].Id),
					textAssociado = piecesJson[i].pieces[j].textAssociado

				};*/
				gps.pieces.Add(new pieceAssociation {

					pieceImage = Resources.Load<Sprite>(piecesJson[i].pieces[j].Sprite),
					Id = int.Parse(piecesJson[i].pieces[j].Id),
					textAssociado = piecesJson[i].pieces[j].textAssociado

				});
				//groupPiece.Add(gps);

			}
			groupPiece.Add(gps);

		}


	}

	public void registerAssociationTrue(bool flag) 
	{
		if(flag) 
		{
			associationAnswers.Add(auxAssociaitionId, new timeAndBool {
				flag = "true",
				timeInGame = timeAssociation.ToString()
			});

		}
		else 
		{
			associationAnswers.Add(auxAssociaitionId, new timeAndBool {
				flag = "false",
				timeInGame = timeAssociation.ToString()
			});

		}

	}

	public void OnGameStartReset() 
	{
		StartCoroutine(oneSecond());

	}

	public void StartResources() {
		if(getResources != null) { getResources(); }
	}

	public void StartGame() {

		pieceRandom = _Shuffle.Shuffle();
		panelsRandom = _Shuffle.Shuffle();
		auxPieceId++;
		auxAssociaitionId = gpRandom[auxPieceId];


		for (int i = 0; i < imgRefs.Count; i++) 
		{

			imgRefs[i].sprite = groupPiece[gpRandom[auxPieceId]].pieces[pieceRandom[i]].pieceImage; 
			imgRefs[i].gameObject.GetComponent<SetId>().id = groupPiece[gpRandom[auxPieceId]].pieces[pieceRandom[i]].Id;
			// 2 shuffles
			panels[panelsRandom[i]].gameObject.GetComponent<DropAssociation>().Id = groupPiece[gpRandom[auxPieceId]].pieces[pieceRandom[i]].Id;
			txtRefs[panelsRandom[i]].text = groupPiece[gpRandom[auxPieceId]].pieces[pieceRandom[i]].textAssociado;

		}


	}

	public void GameEnd() 
	{
		if (Endd != null)
			Endd();

		questionCounts = 0;
		GetNewIdsPieces();
		

	}

	public void GetNewIdsPieces() 
	{
		auxPieceId = -1;
		gpRandom = _Shuffle.Shuffle();
		associationAnswers = new Dictionary<int, timeAndBool>();
	}

	IEnumerator oneSecond() {

		yield return new WaitForSeconds(1f);
			
		if (resetPos != null) resetPos();
		StartGame();
		ended = false;
		open = false;

}

}

[System.Serializable]
public class pieceAssociation {

	public Sprite pieceImage;
	public int Id;
	public string textAssociado;
	
}

[System.Serializable]
public class groupPieces {
	public List<pieceAssociation> pieces;
}

[System.Serializable]
public class pieceAssociationJson {

	public string Sprite;
	public string Id;
	public string textAssociado;

}

[System.Serializable]
public class groupPiecesJson {
	public List<pieceAssociationJson> pieces;
}

[System.Serializable]
public class timeAndBool {
	public string flag;
	public string timeInGame;

}

