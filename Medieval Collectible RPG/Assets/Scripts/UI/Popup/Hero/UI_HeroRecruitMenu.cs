using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HeroRecruitMenu : MonoBehaviour
{
	public int SelectedMenuIndex;

	public Action<int> OnMenuIndexChanged;

	private void Awake()
	{
		SelectedMenuIndex = 0;

		foreach (KeyValuePair<int, Data.RecruitEventInfo> pair in LobbyManager.Instance.RecruitEventDict)
		{
			GameObject go = ResourceManager.Instance.Instantiate($"Prefabs/UI/Heros/RecruitOptionButton", transform);
			go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = pair.Value.RecruitShortName;
			go.GetComponent<Button>().onClick.AddListener(() =>
			{
				SelectedMenuIndex = pair.Key;
				OnMenuIndexChanged.Invoke(SelectedMenuIndex);
			});
		}
	}
}
