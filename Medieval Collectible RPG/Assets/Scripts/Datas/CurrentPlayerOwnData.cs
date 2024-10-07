using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
	// 현재 플레이하는 플레이어가 가진 영웅들의 정보를 관리합니다
	public class CurrentPlayerOwnHero
	{
		public int HeroId; // 영웅 코드
		public int HeroGrade; // 영웅 등급
		public int HeroLevel; // 영웅 레벨

		public CurrentPlayerOwnHero(EntirePlayerOwnHero oi)
		{
			// EntirePlayerOwnHero 리스트에서 접속중인 유저의 식별코드를 비교하고 
			// 소유정보코드와 식별코드를 제외한 나머지를 불러옵니다
			HeroId = oi.HeroId;
			HeroGrade = oi.HeroGrade;
			HeroLevel = oi.HeroLevel;
		}
	}

	// HeroOwn 클래스는 모든 플레이어들이 가진 영웅들의 정보와 성장 상태를 관리합니다
	// 같은 영웅을 여럿 모집할 수 있게 만들어 나중에 grade를 올릴 수 있어야합니다
	[Serializable]
	public class EntirePlayerOwnHero
	{
		public int HeroOwnId; // 소유정보 코드
		public int UserId; // 플레이어 식별코드
		public int HeroId; // 영웅 코드
		public int HeroGrade; // 해당 영웅의 등급
		public int HeroLevel; // 해당 영웅의 레벨
	}

	[Serializable]
	public class CurrentPlayerOwnHeroLoader : ILoader<int, CurrentPlayerOwnHero>
	{
		public List<EntirePlayerOwnHero> ownInfos = new List<EntirePlayerOwnHero>();

		public Dictionary<int, CurrentPlayerOwnHero> LoadToDictionary()
		{
			Dictionary<int, CurrentPlayerOwnHero> dict = new Dictionary<int, CurrentPlayerOwnHero>();
			foreach (EntirePlayerOwnHero own in ownInfos)
			{
				if (own.UserId == 19990428)
				{
					dict.Add(own.HeroId, new CurrentPlayerOwnHero(own));
				}
			}

			// 임시 샘플데이터 추가
			for (int i = 0; i < 349; i++)
			{
				dict.Add(11 + i, new CurrentPlayerOwnHero(new EntirePlayerOwnHero { HeroId = UnityEngine.Random.Range(1, 11) , HeroLevel = UnityEngine.Random.Range(1, 1000), HeroGrade = UnityEngine.Random.Range(1, 6) }));
			}

			return dict;
		}
	}
}