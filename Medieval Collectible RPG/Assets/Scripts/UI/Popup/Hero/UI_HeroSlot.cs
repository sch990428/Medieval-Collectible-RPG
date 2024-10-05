using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_HeroSlot : MonoBehaviour, IListItem
{
    public TextMeshProUGUI HeroName;
    public TextMeshProUGUI HeroLevel;
    public GameObject HeroGradeStar;
	public GameObject HeroClassIcon;

	public Data.CurrentPlayerOwnHero slotInfo;

    public Color32 HeroTypeColor;

	public void Init()
    {
        // 현재 슬롯의 각 표현요소들에 값을 바인드합니다
		Data.HeroInfo heroinfo = LobbyManager.Instance.HeroDict[slotInfo.HeroId];

        GetComponent<Image>().color = HeroTypeColor;

        HeroName.text = heroinfo.HeroName;
        HeroLevel.text = slotInfo.HeroLevel.ToString();
		HeroClassIcon.GetComponent<Image>().sprite = ResourceManager.Instance.Load<Sprite>($"Art/UI/Heros/HeroClassIcon/HeroClassIcon_{heroinfo.HeroClass}");

        // 영웅의 등급에 따라 표시할 별의 개수를 정합니다
        for (int i = 0; i < slotInfo.HeroGrade; i++)
        {
            HeroGradeStar.transform.GetChild(i).gameObject.SetActive(true);
		}
    }

	public void OnPointerClick(PointerEventData eventData)
	{
        Debug.Log("아이템 클릭");
	}
}
