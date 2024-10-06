using Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_HeroDetail : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI heroName;

    [SerializeField]
    TextMeshProUGUI heroDescription;

    private Data.HeroInfo heroData;
    private GameObject heroModel;

    // 캐릭터 모델의 레이어를 바꿉니다
    // 모델 프리팹의 계층구조를 순회하며 재귀적으로 모든 요소를 바꿔야합니다
	void SetLayerRecursively(GameObject obj, int newLayer)
	{
		if (obj == null)
			return;

		// 현재 오브젝트의 레이어 설정
		obj.layer = newLayer;

		// 자식 오브젝트들의 레이어도 재귀적으로 설정
		foreach (Transform child in obj.transform)
		{
			SetLayerRecursively(child.gameObject, newLayer);
		}
	}

	public void SetInfo(Data.CurrentPlayerOwnHero data)
    {
        heroData = LobbyManager.Instance.HeroDict[data.HeroId];

        heroModel = ResourceManager.Instance.Instantiate($"Prefabs/Heros/Hero_{data.HeroId}/Hero{data.HeroId}");
        heroModel.transform.SetParent(transform, false);
        SetLayerRecursively(heroModel, 6);
        heroModel.transform.localPosition = new Vector3(-960, -542, 0);
        heroModel.transform.Rotate(new Vector3(0, 180f, 0));
        heroModel.transform.localScale = new Vector3(1.75f, 1.75f, 1.75f);
    }

    public void RemoveModel()
    {
        ResourceManager.Instance.Destroy(heroModel);
    }
}
