using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RecruitCutsceneController : MonoBehaviour
{
	[SerializeField]
	Camera mainCamera;

	private int totalHeroNum;
	private bool recruitEnd = false;

	private void Awake()
	{
		totalHeroNum = LobbyManager.Instance.HeroDict.Count;
	}

	private void OnEnable()
	{
		mainCamera.gameObject.SetActive(false);

		//GeneralRecruit();
		HeroPickUpRecruit(1);

		recruitEnd = true;
	}

	private void OnDisable()
	{
		mainCamera.gameObject.SetActive(true);
	}

	private bool IsHero()
	{
		int random = UnityEngine.Random.Range(1, 11);

		return random == 1 || random == 2;
	}

	private void GeneralRecruit()
	{
		for (int i = 0; i < 10; i++)
		{
			if (IsHero())
			{
				int random = UnityEngine.Random.Range(1, totalHeroNum + 1);
				Debug.Log(LobbyManager.Instance.HeroDict[random].HeroName);
			}
			else
			{
				Debug.Log("꽝");
			}
		}

		gameObject.SetActive(false);
	}

	private void HeroPickUpRecruit(int target)
	{
		for (int i = 0; i < 10; i++)
		{
			if (IsHero())
			{
				int random = UnityEngine.Random.Range(1, totalHeroNum + 2);
				if (random == totalHeroNum + 1)
				{
					random = target;
				}

				string MSG = "";
				if (random == target)
				{
					MSG = "[@@PickUp@@]";
				}

				Debug.Log($"{MSG}{LobbyManager.Instance.HeroDict[random].HeroName}");
			}
			else
			{
				Debug.Log("꽝");
			}
		}
	}

	private void ClosedCutscene()
	{
		gameObject.SetActive(false);
	}

	private void Update()
	{
		if (Input.GetMouseButton(0) && recruitEnd)
		{
			Invoke(nameof(ClosedCutscene), 0.5f);
		}
	}
}
