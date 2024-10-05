using Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;

public class FilterData
{
	public int ClassFilter;
	public int TypeFilter;
}


public class UI_HeroList : MonoBehaviour
{
	private List<UI_HeroSlot> slots;

	[SerializeField]
	private List<Color32> heroTypeColors; // 영웅 속성에 따른 슬롯 배경색

	[SerializeField]
	private List<Button> classFilterButtons; // 영웅 클래스에 따른 필터링 버튼

	[SerializeField]
	private List<Button> typeFilterButtons; // 영웅 속성에 따른 필터링 버튼

	[SerializeField]
	private TMP_Dropdown sortDropdown; // 영웅 정렬 옵션을 선택하는 토글 버튼

	[SerializeField]
	private Toggle toggleAsending; // 정렬 순서(오름, 내림)를 선택하는 토글버튼

	UI_HeroSlot slot;
	private FilterData filterData;

	void Start()
	{
		slots = new List<UI_HeroSlot>();
		filterData = new FilterData() { ClassFilter = 0, TypeFilter = 0 };
		sortDropdown.onValueChanged.AddListener(UpdateSort);
		toggleAsending.onValueChanged.AddListener(UpdateAscending);

		UpdateClassFilter(0);
		UpdateTypeFilter(0);

		Init();
		UpdateSort(0); // 최초 정렬 호출
	}

    public void Init()
    {
		// 플레이어가 소유한 영웅들을 표시하기 위한 슬롯을 생성하고 배치
		foreach (KeyValuePair<int, Data.CurrentPlayerOwnInfo> ownHero in LobbyManager.Instance.OwnHeroDict)
        {
            Data.HeroInfo heroinfo = LobbyManager.Instance.HeroDict[ownHero.Value.HeroId];

			GameObject go = ResourceManager.Instance.Instantiate("Prefabs/UI/Heros/UI_HeroSlotCell");

			slot = go.GetComponent<UI_HeroSlot>();
			slot.transform.SetParent(transform, false); // worldPositionStays를 false로 하여 로컬 벡터를 유지하도록 함

			slot.HeroTypeColor = heroTypeColors[heroinfo.HeroType];
			slot.slotInfo = ownHero.Value;
			slot.Init();

			slots.Add(slot);
        }

		// 정렬을 위한 버튼들에 값변경 리스너 추가
		sortDropdown.onValueChanged.AddListener(UpdateSort);
		toggleAsending.onValueChanged.AddListener(UpdateAscending);
	}

	// 영웅의 직군 별로 선택값에 따라 필터링한다
	public void UpdateClassFilter(int filter)
	{
		filterData.ClassFilter = filter;

		// 선택된 버튼과 나머지 버튼을 갱신한다
		for (int i = 0; i < classFilterButtons.Count; i++)
		{
			if (i == filter)
			{
				classFilterButtons[i].image.color = new Color32(65, 65, 65, 255);
			}
			else
			{
				classFilterButtons[i].image.color = Color.white;
			}
		}

		UpdateFilter();
	}

	// 영웅의 속성 별로 선택값에 따라 필터링한다
	public void UpdateTypeFilter(int filter)
	{
		filterData.TypeFilter = filter;

		// 선택된 버튼과 나머지 버튼을 갱신한다
		for (int i = 0; i < typeFilterButtons.Count; i++)
		{
			if (i == filter)
			{
				typeFilterButtons[i].transform.GetChild(0).gameObject.SetActive(true);
			}
			else
			{
				typeFilterButtons[i].transform.GetChild(0).gameObject.SetActive(false);
			}
		}

		UpdateFilter();
	}

	// 필터에 따라 영웅 슬롯들의 활성화 상태를 갱신한다
	public void UpdateFilter()
	{
		foreach(Transform slot in transform)
		{
			int slotHeroId = slot.GetComponent<UI_HeroSlot>().slotInfo.HeroId;
			HeroInfo hero = LobbyManager.Instance.HeroDict[slotHeroId];

			bool classCondition = filterData.ClassFilter == 0 || hero.HeroClass == filterData.ClassFilter;
			bool typeCondition = filterData.TypeFilter == 0 || hero.HeroType == filterData.TypeFilter;

			slot.gameObject.SetActive(classCondition && typeCondition);
		}
	}

	// 이름순 정렬
	public int CompareHerosByName(UI_HeroSlot slotA, UI_HeroSlot slotB, bool isAsending = false)
	{
		Data.HeroInfo heroA = LobbyManager.Instance.HeroDict[slotA.slotInfo.HeroId];
		Data.HeroInfo heroB = LobbyManager.Instance.HeroDict[slotB.slotInfo.HeroId];

		int nameComparison = isAsending? heroB.HeroName.CompareTo(heroA.HeroName) : heroA.HeroName.CompareTo(heroB.HeroName);

		// 이름이 같다면 등급으로 비교
		if (nameComparison == 0)
		{
			int gradeComparison = slotB.slotInfo.HeroGrade.CompareTo(slotA.slotInfo.HeroGrade);

			if (gradeComparison == 0)
			{
				// 등급도 같으면 레벨로 비교
				return slotB.slotInfo.HeroLevel.CompareTo(slotA.slotInfo.HeroLevel);
			}

			return gradeComparison;
		}

		return nameComparison; // 등급이 다르면 등급 기준으로 정렬
	}

	// 등급순 정렬
	public int CompareHerosByGrade(UI_HeroSlot slotA, UI_HeroSlot slotB, bool isAsending = false)
	{
		Data.HeroInfo heroA = LobbyManager.Instance.HeroDict[slotA.slotInfo.HeroId];
		Data.HeroInfo heroB = LobbyManager.Instance.HeroDict[slotB.slotInfo.HeroId];

		int gradeComparison = isAsending ? slotB.slotInfo.HeroGrade.CompareTo(slotA.slotInfo.HeroGrade) : slotA.slotInfo.HeroGrade.CompareTo(slotB.slotInfo.HeroGrade);

		// 등급이 같다면 레벨로 비교
		if (gradeComparison == 0)
		{
			int levelComparison = slotB.slotInfo.HeroLevel.CompareTo(slotA.slotInfo.HeroLevel);

			if (levelComparison == 0)
			{
				// 레벨도 같으면 이름으로 비교
				return heroA.HeroName.CompareTo(heroB.HeroName);
			}

			return levelComparison;
		}

		return gradeComparison; // 등급이 다르면 등급 기준으로 정렬
	}

	// 레벨순 정렬
	public int CompareHerosByLevel(UI_HeroSlot slotA, UI_HeroSlot slotB, bool isAsending = false)
	{
		Data.HeroInfo heroA = LobbyManager.Instance.HeroDict[slotA.slotInfo.HeroId];
		Data.HeroInfo heroB = LobbyManager.Instance.HeroDict[slotB.slotInfo.HeroId];

		int levelComparison = isAsending ? slotB.slotInfo.HeroLevel.CompareTo(slotA.slotInfo.HeroLevel) : slotA.slotInfo.HeroLevel.CompareTo(slotB.slotInfo.HeroLevel);

		// 레벨이 같다면 등급으로 비교
		if (levelComparison == 0)
		{
			int gradeComparison = slotB.slotInfo.HeroGrade.CompareTo(slotA.slotInfo.HeroGrade);

			if (gradeComparison == 0)
			{
				// 등급도 같으면 이름으로 비교
				return heroA.HeroName.CompareTo(heroB.HeroName);
			}

			return gradeComparison;
		}

		return levelComparison; // 등급이 다르면 등급 기준으로 정렬
	}

	// 정렬 순서 변경시 호출되는 중간함수
	public void UpdateAscending(bool toggle)
	{
		UpdateSort(sortDropdown.value);
	}

	// 기준에 따라 영웅 슬롯들을 정렬한다
	public void UpdateSort(int option)
	{
		switch (option)
		{
			case 0:
				slots.Sort((a, b) => CompareHerosByGrade(a, b, toggleAsending.isOn));
				break;

			case 1:
				slots.Sort((a, b) => CompareHerosByLevel(a, b, toggleAsending.isOn));
				break;

			case 2:
				slots.Sort((a, b) => CompareHerosByName(a, b, toggleAsending.isOn));
				break;
		}

		for (int i = 0; i < slots.Count; i++)
		{
			slots[i].transform.SetSiblingIndex(i);
		}
	}

	void Update()
    {
        
    }
}
