using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HeroList : MonoBehaviour
{
    void Start()
    {
        Init();
    }

    public void Init()
    {
        // 플레이어가 소유한 영웅들을 표시하기 위한 슬롯을 생성하고 배치
        foreach(KeyValuePair<int, Data.CurrentPlayerOwnInfo> ownHero in LobbyManager.Instance.OwnHeroDict)
        {
            GameObject go = ResourceManager.Instance.Instantiate("Prefabs/UI/Heros/UI_HeroSlotCell");

			UI_HeroSlot slot = go.GetComponent<UI_HeroSlot>();
            slot.transform.SetParent(transform, false); // worldPositionStays를 false로 하여 로컬 방향을 유지하도록 함

			slot.slotInfo = ownHero.Value;
            slot.Init();
        }
    }

    void Update()
    {
        
    }
}
