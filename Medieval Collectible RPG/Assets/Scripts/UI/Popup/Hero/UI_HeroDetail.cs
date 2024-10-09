using Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HeroDetail : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI heroName;

    [SerializeField]
    TextMeshProUGUI heroDescription;

	[SerializeField]
	GameObject heroGradeStar;

	[SerializeField]
	Image heroClassImage;

	[SerializeField]
	TextMeshProUGUI heroLevelText;

	[SerializeField]
	TextMeshProUGUI heroStatText;

    [SerializeField]
    RawImage HeroModelDisplayer;

	[SerializeField]
	Color32[] heroTypeColor;

	private Data.HeroInfo heroData;
    private Data.CurrentPlayerOwnHero ownData;

    private GameObject heroModel;

    // 캐릭터 모델의 레이어를 바꿉니다
    // 모델 프리팹의 계층구조를 순회하며 재귀적으로 모든 요소를 바꿔야합니다
	void SetLayerRecursively(GameObject go, int newLayer)
	{
		if (go == null)
			return;

		// 현재 오브젝트의 레이어 설정
		go.layer = newLayer;

		// 자식 오브젝트들의 레이어도 재귀적으로 설정
		foreach (Transform child in go.transform)
		{
			SetLayerRecursively(child.gameObject, newLayer);
		}
	}

	public void Init(Data.CurrentPlayerOwnHero data)
    {
        // 세부정보 창에 영웅의 기본 데이터와 소유 정보를 불러옵니다
        heroData = LobbyManager.Instance.HeroDict[data.HeroId];
        ownData = data;

        // 모델을 표시하고 모델 오브젝트의 각종 트랜스폼 등을 조정합니다
        heroModel = ResourceManager.Instance.Instantiate($"Prefabs/Heros/Hero_{data.HeroId}/Hero{data.HeroId}");
        heroModel.transform.SetParent(transform, false);
        SetLayerRecursively(heroModel, 6);
        heroModel.transform.localPosition = new Vector3(-960, -542, 0);
        heroModel.transform.Rotate(new Vector3(0, 180f, 0));
        heroModel.transform.localScale = new Vector3(1.75f, 1.75f, 1.75f);

        Animator anim = heroModel.GetComponent<Animator>();
        if (anim == null)
        {
            anim = heroModel.AddComponent<Animator>();
        }
        anim.runtimeAnimatorController = ResourceManager.Instance.Load<RuntimeAnimatorController>("Animation/Heros/LobbyHeroAnimController");

        HeroModelDisplayer.GetComponent<UI_HeroModel>().TargetHero = heroModel;

        // 하위 UI요소들을 변경합니다
        heroName.text = heroData.HeroName;
        heroDescription.text = heroData.HeroDescription;
        heroClassImage.sprite = ResourceManager.Instance.Load<Sprite>($"Art/UI/Heros/HeroClassIcon/HeroClassIcon_{heroData.HeroClass}");
        heroClassImage.color = heroTypeColor[heroData.HeroType];

		for (int i = 0; i < 5; i++)
		{
			heroGradeStar.transform.GetChild(i).gameObject.SetActive(false);
		}
		for (int i = 0; i < ownData.HeroGrade; i++)
        {
            heroGradeStar.transform.GetChild(i).gameObject.SetActive(true);
        }

        heroLevelText.text = GetLevelToText();
        heroStatText.text = GetStatsToText();
    }

    public string GetLevelToText()
    {
        return $"Lv {ownData.HeroLevel} / <#941F00>{20 * ownData.HeroGrade - 1}</color>";
    }

	public string GetStatsToText()
	{
		return $"{heroData.HeroStats.MaxHp + 100 * ownData.HeroLevel}\n" +
            $"{heroData.HeroStats.MeleeDamage * ownData.HeroLevel}\n" +
            $"{heroData.HeroStats.MagicDamage * ownData.HeroLevel}\n" +
            $"{heroData.HeroStats.MeleeDefence * ownData.HeroLevel}\n" +
            $"{heroData.HeroStats.MagicDefence * ownData.HeroLevel}\n" +
            $"{heroData.HeroStats.AttackSpeed + 1 * (ownData.HeroLevel / 30)}\n";
	}

	public void RemoveModel()
    {
        // 모델을 제거합니다(세부정보 창 종료)
        ResourceManager.Instance.Destroy(heroModel);
    }
}
