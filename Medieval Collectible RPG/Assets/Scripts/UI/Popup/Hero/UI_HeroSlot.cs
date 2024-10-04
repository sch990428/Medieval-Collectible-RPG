using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_HeroSlot : MonoBehaviour
{
    public TextMeshProUGUI HeroName;
    public TextMeshProUGUI HeroLevel;
    public GameObject HeroGradeStar;
	public GameObject HeroClassIcon;

	public Data.CurrentPlayerOwnInfo slotInfo;

	public void Init()
    {
		// 영웅의 속성별 색상을 정의합니다
		List<Color32> HeroTypeColors = new List<Color32>()
		{
			new Color32(200, 200, 200, 255),
			new Color32(173, 193, 255, 255),
			new Color32(255, 190, 190, 255),
			new Color32(178, 255, 185, 255),
			new Color32(255, 255, 230, 255),
			new Color32(163, 163, 163, 255)
		};

		// 현재 슬롯의 각 표현요소들에 값을 바인드합니다
		Data.HeroInfo heroinfo = LobbyManager.Instance.HeroDict[slotInfo.HeroId];

        GetComponent<Image>().color = HeroTypeColors[heroinfo.HeroType];

        HeroName.text = heroinfo.HeroName;
        HeroLevel.text = slotInfo.HeroLevel.ToString();
		HeroClassIcon.GetComponent<Image>().sprite = ResourceManager.Instance.Load<Sprite>($"Art/UI/Heros/HeroClassIcon/HeroClassIcon_{heroinfo.HeroClass}");

        for (int i = 0; i < slotInfo.HeroGrade; i++)
        {
            HeroGradeStar.transform.GetChild(i).gameObject.SetActive(true);
		}
    }
}
