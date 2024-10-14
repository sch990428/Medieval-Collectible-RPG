using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRecruitStrategy
{
    public List<int> Recruit(int count);
}

// 일반 소환
public class GeneralRecruitStrategy : IRecruitStrategy
{
	public List<int> Recruit(int count)
	{
		List<int> recruitResult = new List<int>();
		int totalHeroNum = LobbyManager.Instance.HeroDict.Count;

		for (int i = 0; i < count; i++)
		{
			int random = UnityEngine.Random.Range(1, 11);

			if (random == 1 || random == 2)
			{
				int whichHero = UnityEngine.Random.Range(1, totalHeroNum + 1);
				recruitResult.Add(whichHero);
			}
			else
			{
				recruitResult.Add(0);
			}
		}

		return recruitResult;
	}
}

// 라이언 픽업소환
public class Hero1PickUpRecruitStrategy : IRecruitStrategy
{
	public List<int> Recruit(int count)
	{
		List<int> recruitResult = new List<int>();
		int totalHeroNum = LobbyManager.Instance.HeroDict.Count;

		for (int i = 0; i < count; i++)
		{
			int random = UnityEngine.Random.Range(1, 11);

			if (random == 1 || random == 2)
			{
				int whichHero = UnityEngine.Random.Range(1, totalHeroNum + 3);
				if (whichHero >= totalHeroNum + 1)
				{
					whichHero = 1;
				}
				recruitResult.Add(whichHero);
			}
			else
			{
				recruitResult.Add(0);
			}
		}

		return recruitResult;
	}
}