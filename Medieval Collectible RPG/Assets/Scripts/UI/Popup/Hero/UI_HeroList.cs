using Data;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroFilter
{
	public int ClassFilter;
	public int TypeFilter;

	// 필터에 따라 영웅 슬롯들의 활성화 상태를 갱신합니다
	public void UpdateFilter(List<UI_HeroSlot> slots)
	{
		foreach (UI_HeroSlot slot in slots)
		{
			int slotHeroId = slot.ListItemInfo.HeroId;
			HeroInfo hero = LobbyManager.Instance.HeroDict[slotHeroId];
			bool classCondition = ClassFilter == 0 || hero.HeroClass == ClassFilter;
			bool typeCondition = TypeFilter == 0 || hero.HeroType == TypeFilter;

			slot.gameObject.SetActive(classCondition && typeCondition);
		}
	}
}

public class HeroSorter
{
	private ICompareStrategy<UI_HeroSlot, Data.CurrentPlayerOwnHero> compareStrategy;

	public void SetStrategy(ICompareStrategy<UI_HeroSlot, Data.CurrentPlayerOwnHero> strategy)
	{
		compareStrategy = strategy;
	}

	public void Sort(List<UI_HeroSlot> items, bool isAsending)
	{
		items.Sort((a,b)=>compareStrategy.Compare(a, b, isAsending));

		for (int i = 0; i < items.Count; i++)
		{
			items[i].transform.SetSiblingIndex(i);
		}
	}
}

public class UI_HeroList : UI_List<UI_HeroSlot, Data.CurrentPlayerOwnHero>
{
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

	[SerializeField]
	private Canvas heroDetailCanvas; // 영웅 상세 정보 UI

	private HeroFilter heroFilter;
	private HeroSorter heroSorter;

	protected override void Awake()
	{
		base.Awake();

		heroFilter = new HeroFilter() { ClassFilter = 0, TypeFilter = 0 };
		heroSorter = new HeroSorter();
		heroSorter.SetStrategy(new CompareByHeroGradeStrategy<UI_HeroSlot, Data.CurrentPlayerOwnHero>());

		sortDropdown.onValueChanged.AddListener(UpdateSort);
		toggleAsending.onValueChanged.AddListener(UpdateAscending);

		UpdateClassFilter(0);
		UpdateTypeFilter(0);

		Init();
		UpdateSort(0); // 최초 정렬 호출
	}

    public override void Init()
    {
		base.Init();

		// 플레이어가 소유한 영웅들을 표시하기 위한 슬롯을 생성하고 배치
		foreach (KeyValuePair<int, Data.CurrentPlayerOwnHero> ownHero in LobbyManager.Instance.OwnHeroDict)
        {
            Data.HeroInfo heroinfo = LobbyManager.Instance.HeroDict[ownHero.Value.HeroId];

			UI_HeroSlot slot = CreateItem("Prefabs/UI/Heros/UI_HeroSlotCell");

			slot.HeroTypeColor = heroTypeColors[heroinfo.HeroType];
			slot.ListItemInfo = ownHero.Value;
			slot.HeroDetailCanvas = heroDetailCanvas;

			slot.Init();

			Add(slot);
        }

		// 정렬을 위한 버튼들에 값변경 리스너 추가
		sortDropdown.onValueChanged.AddListener(UpdateSort);
		toggleAsending.onValueChanged.AddListener(UpdateAscending);
	}

	// 영웅의 직군 별로 선택값에 따라 필터링합니다
	public void UpdateClassFilter(int filter)
	{
		heroFilter.ClassFilter = filter;

		// 선택된 버튼과 나머지 버튼을 갱신합니다
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

		heroFilter.UpdateFilter(items);
	}

	// 영웅의 속성 별로 선택값에 따라 필터링합니다
	public void UpdateTypeFilter(int filter)
	{
		heroFilter.TypeFilter = filter;

		// 선택된 버튼과 나머지 버튼을 갱신합니다
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

		heroFilter.UpdateFilter(items);
	}

	// 정렬 순서 변경시 호출되는 중간함수
	public void UpdateAscending(bool toggle)
	{
		UpdateSort(sortDropdown.value);
	}

	// 기준에 따라 영웅 슬롯들을 정렬합니다
	public void UpdateSort(int option)
	{
		switch (option)
		{
			case 0:
				heroSorter.SetStrategy(new CompareByHeroGradeStrategy<UI_HeroSlot, Data.CurrentPlayerOwnHero>());
				break;

			case 1:
				heroSorter.SetStrategy(new CompareByHeroLevelStrategy<UI_HeroSlot, Data.CurrentPlayerOwnHero>());
				break;

			case 2:
				heroSorter.SetStrategy(new CompareByHeroNameStrategy<UI_HeroSlot, Data.CurrentPlayerOwnHero>());
				break;
		}

		heroSorter.Sort(items, toggleAsending.isOn);
	}

	void Update()
    {
        
    }
}
