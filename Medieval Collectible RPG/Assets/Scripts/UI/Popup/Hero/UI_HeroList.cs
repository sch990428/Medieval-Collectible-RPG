using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HeroList : MonoBehaviour
{
	List<Color32> HeroTypeColors;
	UI_HeroSlot slot;
	void Start()
    {
		// 영웅의 속성별 색상을 정의
		HeroTypeColors = new List<Color32>()
		{
			new Color32(200, 200, 200, 255),
			new Color32(173, 193, 255, 255),
			new Color32(255, 190, 190, 255),
			new Color32(178, 255, 185, 255),
			new Color32(255, 255, 230, 255),
			new Color32(163, 163, 163, 255)
		};

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

			slot.HeroTypeColor = HeroTypeColors[heroinfo.HeroType];
			slot.slotInfo = ownHero.Value;
			slot.Init();
        }
    }

	// 삭제하고 다시 Instantiate 하기보단 SetActive로 활성상태를 전환한다
	public void SetClassFilter(int classFilter)
	{
		foreach (Transform heroSlot in transform)
		{
			if (classFilter == 0)
			{
				heroSlot.gameObject.SetActive(true);
				continue;
			}

			CurrentPlayerOwnInfo slotInfo = heroSlot.GetComponent<UI_HeroSlot>().slotInfo;  // 각 슬롯의 영웅 정보를 가져옴
			if ((classFilter != 0 && LobbyManager.Instance.HeroDict[slotInfo.HeroId].HeroClass == classFilter))  // 필터 조건에 맞는지 확인
			{
				heroSlot.gameObject.SetActive(true);  // 조건에 맞으면 활성화
			}
			else
			{
				heroSlot.gameObject.SetActive(false);  // 조건에 맞지 않으면 비활성화
			}
		}
	}

    void Update()
    {
        
    }
}
