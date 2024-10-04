using Data;
using System.Collections;
using System.Collections.Generic;
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
	[SerializeField]
	private List<Color32> heroTypeColors; // 영웅 속성에 따른 슬롯 배경색

	[SerializeField]
	private List<Button> classFilterButtons; // 영웅 클래스에 따른 필터링 버튼

	[SerializeField]
	private List<Button> typeFilterButtons; // 영웅 속성에 따른 필터링 버튼

	UI_HeroSlot slot;
	private FilterData filterData;

	void Start()
	{
		filterData = new FilterData() { ClassFilter = 0, TypeFilter = 0 };
		UpdateClassFilter(0);
		UpdateTypeFilter(0);
		Init();
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
        }
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

	// 필터에 따라 영웅 슬롯들을 갱신한다
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

	void Update()
    {
        
    }
}
