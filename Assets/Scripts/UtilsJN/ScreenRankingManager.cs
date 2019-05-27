using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenRankingManager : Singleton<ScreenRankingManager>
{

	[HideInInspector]
	public List<UserRanking> infosUsers;
	public List<ContainerRank> containers;
	

	public void OnSetRanks(List<UserRanking> infos)
	{
		infosUsers = infos;
		SetRankings();
	}

	private void SetRankings()
	{
		for(int i = 0; i < containers.Count; i++)
		{
			var pos = (i + 1).ToString() + "º";
			var name = infosUsers[i].nome;
			var score = infosUsers[i].score.ToString();
			var time = infosUsers[i].time.ToString();

			containers[i].posRankTxt.text = pos + " - " + name + "Acertou: " + score + " Tempo: " + time;
		}
	}
}
