using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RecruitCutsceneController : MonoBehaviour
{
	[SerializeField]
	Camera mainCamera;
	[SerializeField]
	GameObject resultPanel;

	private bool recruitEnd;
	public IRecruitStrategy recruitStrategy; // 소환 전략

	int RecruitId;

	List<int> recruitResult; // 소환 결과 리스트

	private void OnEnable()
	{
		mainCamera.gameObject.SetActive(false); // 메인카메라를 비활성화
	}

	public void StartRecruit(int recruitId, int count)
	{
		recruitEnd = false;
		RecruitId = recruitId;
		// recruitId에 따라 소환 전략을 할당
		switch (recruitId)
		{
			case 0:
				recruitStrategy = new GeneralRecruitStrategy();
				break;
			case 1:
				recruitStrategy = new Hero1PickUpRecruitStrategy();
				break;
		}

		recruitResult = recruitStrategy.Recruit(count);
		recruitEnd = true;
	}

	private void OnDisable()
	{
		mainCamera.gameObject.SetActive(true);  // 메인카메라를 활성화

		resultPanel.SetActive(true);
		foreach (int i in recruitResult)
		{
			GameObject go = ResourceManager.Instance.Instantiate("Prefabs/UI/Heros/RecruitedHeroCell");
			go.transform.SetParent(resultPanel.transform.GetChild(0), false);
			UI_RecruitedHeroCell slot = go.GetComponent<UI_RecruitedHeroCell>();
			slot.HeroId = i;

			if (RecruitId == 1 && i == 1)
			{
				slot.PickUpText.gameObject.SetActive(true);
			}

			slot.Init();
		}
	}

	private void ClosedCutscene()
	{
		gameObject.SetActive(false);	
	}

	private void Update()
	{
		// 화면이 한번 클릭되면 연출을 스킵합니다
		if (Input.GetMouseButtonUp(0) && recruitEnd)
		{
			Invoke(nameof(ClosedCutscene), 0.2f);
		}
	}
}
