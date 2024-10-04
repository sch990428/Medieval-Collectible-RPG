using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HeroList : MonoBehaviour
{
    void Start()
    {
		Init(0);
    }

    public void Init(int typeFilter)
    {
		// 기존의 슬롯들을 전부 지운다
		foreach (Transform child in transform)
		{
			GameObject.Destroy(child.gameObject);
		}

		// 플레이어가 소유한 영웅들을 표시하기 위한 슬롯을 생성하고 배치
		foreach (KeyValuePair<int, Data.CurrentPlayerOwnInfo> ownHero in LobbyManager.Instance.OwnHeroDict)
        {
            Data.HeroInfo heroinfo = LobbyManager.Instance.HeroDict[ownHero.Value.HeroId];

			if (typeFilter == 0 || (typeFilter != 0 && heroinfo.HeroClass == typeFilter))
            {
				GameObject go = ResourceManager.Instance.Instantiate("Prefabs/UI/Heros/UI_HeroSlotCell");

				UI_HeroSlot slot = go.GetComponent<UI_HeroSlot>();
				slot.transform.SetParent(transform, false); // worldPositionStays를 false로 하여 로컬 방향을 유지하도록 함

				slot.slotInfo = ownHero.Value;
				slot.Init();
			} 
        }
    }

    void Update()
    {
        
    }
}
