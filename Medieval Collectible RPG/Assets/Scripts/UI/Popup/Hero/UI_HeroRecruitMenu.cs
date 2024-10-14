using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UI_HeroRecruitMenu : SideMenu
{
	private void Awake()
	{
		Init();

		// RecruitEvent 데이터에 존재하는 만큼 버튼을 생성해주고 리스너를 할당합니다
		foreach (KeyValuePair<int, Data.RecruitEventInfo> pair in LobbyManager.Instance.RecruitEventDict)
		{
			GameObject go = ResourceManager.Instance.Instantiate($"Prefabs/UI/Heros/RecruitOptionButton", transform);
			go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = pair.Value.RecruitShortName;
			go.GetComponent<Button>().onClick.AddListener(() =>
			{
				ChangeMenuIndex(pair.Key);
			});
		}
	}
}
