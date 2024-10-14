using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HeroRecruitDetail : MenuDetailPage
{
	[SerializeField]
	RecruitCutsceneController RecruitCutscene;

	private void Awake()
	{
		Init();

		foreach (KeyValuePair<int, Data.RecruitEventInfo> pair in LobbyManager.Instance.RecruitEventDict)
		{
			// 소환 세부 페이지의 요소들을 갱신
			GameObject go = ResourceManager.Instance.Instantiate($"Prefabs/UI/Heros/RecruitDetail", transform);
			go.transform.GetChild(0).GetComponent<Image>().sprite = ResourceManager.Instance.Load<Sprite>($"Art/UI/Heros/{pair.Value.RecruitBackgrund}");
			go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = pair.Value.RecruitName;
			go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = pair.Value.RecruitDesciption;
			go.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => {
				RecruitCutscene.gameObject.SetActive(true);
				RecruitCutscene.StartRecruit(pair.Key, 1);
			});
			go.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(() => {
				RecruitCutscene.gameObject.SetActive(true);
				RecruitCutscene.StartRecruit(pair.Key, 10);
			});
			go.SetActive(pair.Key == 0);
		}
	}

	// 선택된 메뉴에 대응하는 세부페이지를 활성화
	protected override void UpdateDetails(int index)
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).gameObject.SetActive(i == index);
		}
	}
}
