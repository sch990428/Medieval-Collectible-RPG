using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_RecruitedHeroCell : MonoBehaviour, IListItem<Data.CurrentPlayerOwnHero>
{
	public Image HeroPortrait;
	public TextMeshProUGUI HeroName;
	public GameObject HeroClassIcon;
	public GameObject PickUpText;

    public int HeroId;
	public Data.CurrentPlayerOwnHero ListItemInfo
    {
        get; set;
    }

    public Color32 HeroTypeColor;

	public void Init()
    {
        if (HeroId != 0)
        {
			// 현재 슬롯의 각 표현요소들에 값을 바인드합니다
			Data.HeroInfo heroinfo = LobbyManager.Instance.HeroDict[HeroId];

			HeroPortrait.sprite = ResourceManager.Instance.Load<Sprite>($"Art/Heros/Portraits/portrait_Hero{heroinfo.HeroId}");
			HeroName.text = heroinfo.HeroName;
			HeroClassIcon.GetComponent<Image>().sprite = ResourceManager.Instance.Load<Sprite>($"Art/UI/Heros/HeroClassIcon/HeroClassIcon_{heroinfo.HeroClass}");
		}
		else
		{
			Destroy(HeroPortrait);
			Destroy(HeroName);
			Destroy(HeroClassIcon);
			Destroy(PickUpText);
		} 
    }

	public void OnPointerClick(PointerEventData eventData)
	{

	}
}
