using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HeroRecruitDetail : MenuDetailPage
{
	private void Awake()
	{
		Init();

		foreach (KeyValuePair<int, Data.RecruitEventInfo> pair in LobbyManager.Instance.RecruitEventDict)
		{
			GameObject go = ResourceManager.Instance.Instantiate($"Prefabs/UI/Heros/RecruitDetail", transform);
			go.transform.GetChild(0).GetComponent<Image>().sprite = ResourceManager.Instance.Load<Sprite>($"Art/UI/Heros/{pair.Value.RecruitBackgrund}");
			go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = pair.Value.RecruitName;
			go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = pair.Value.RecruitDesciption;
			go.SetActive(pair.Key == 0);
		}
	}

	protected override void UpdateDetails(int index)
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).gameObject.SetActive(i == index);
		}
	}
}
