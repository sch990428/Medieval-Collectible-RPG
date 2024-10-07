using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_HeroSlot : MonoBehaviour, IListItem<Data.CurrentPlayerOwnHero>
{
	public Image HeroPortrait;
	public TextMeshProUGUI HeroName;
    public TextMeshProUGUI HeroLevel;
    public GameObject HeroGradeStar;
	public GameObject HeroClassIcon;
	public Canvas HeroDetailCanvas;

	public Data.CurrentPlayerOwnHero ListItemInfo
    {
        get; set;
    }

    public Color32 HeroTypeColor;

	public void Init()
    {
        // 현재 슬롯의 각 표현요소들에 값을 바인드합니다
		Data.HeroInfo heroinfo = LobbyManager.Instance.HeroDict[ListItemInfo.HeroId];

        GetComponent<Image>().color = HeroTypeColor;

        HeroPortrait.sprite = ResourceManager.Instance.Load<Sprite>($"Art/Heros/Portraits/portrait_Hero{heroinfo.HeroId}");
        HeroName.text = heroinfo.HeroName;
        HeroLevel.text = ListItemInfo.HeroLevel.ToString();
		HeroClassIcon.GetComponent<Image>().sprite = ResourceManager.Instance.Load<Sprite>($"Art/UI/Heros/HeroClassIcon/HeroClassIcon_{heroinfo.HeroClass}");

        // 영웅의 등급에 따라 표시할 별의 개수를 정합니다
        for (int i = 0; i < ListItemInfo.HeroGrade; i++)
        {
            HeroGradeStar.transform.GetChild(i).gameObject.SetActive(true);
		}
    }

	public void OnPointerClick(PointerEventData eventData)
	{
        HeroDetailCanvas.GetComponent<UI_HeroDetail>().Init(ListItemInfo);
        HeroDetailCanvas.gameObject.SetActive(true);
	}
}
