using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            GameObject slot = ResourceManager.Instance.Instantiate("Prefabs/UI/Heros/UI_HeroSlotCell");
            slot.transform.SetParent(transform);
        }
    }

    void Update()
    {
        
    }
}
