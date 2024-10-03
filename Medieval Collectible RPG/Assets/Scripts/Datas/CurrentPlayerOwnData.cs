using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
	// 현재 플레이하는 플레이어가 가진 영웅들의 정보를 관리합니다
	public class CurrentPlayerOwnInfo
	{
		public int HeroId; // 영웅 코드
		public int HeroGrade; // 영웅 등급
		public int HeroLevel; // 영웅 레벨

		public CurrentPlayerOwnInfo(OwnInfo oi)
		{
			HeroId = oi.HeroId;
			HeroGrade = oi.HeroGrade;
			HeroLevel = oi.HeroLevel;
		}
	}

	// HeroOwn 클래스는 모든 플레이어들이 가진 영웅들의 정보와 성장 상태를 관리합니다
	// 같은 영웅을 여럿 모집할 수 있게 만들어 나중에 grade를 올릴 수 있어야합니다
	[Serializable]
	public class OwnInfo
	{
		public int HeroOwnId; // 소유정보 코드
		public int UserId; // 플레이어 식별코드
		public int HeroId; // 영웅 코드
		public int HeroGrade; // 해당 영웅의 등급
		public int HeroLevel; // 해당 영웅의 레벨
	}

	[Serializable]
	public class CurrentPlayerOwnData : ILoader<int, CurrentPlayerOwnInfo>
	{
		public List<OwnInfo> ownInfos = new List<OwnInfo>();

		public Dictionary<int, CurrentPlayerOwnInfo> LoadToDictionary()
		{
			Dictionary<int, CurrentPlayerOwnInfo> dict = new Dictionary<int, CurrentPlayerOwnInfo>();
			foreach (OwnInfo own in ownInfos)
			{
				if (own.UserId == 19990428)
				{
					dict.Add(own.HeroId, new CurrentPlayerOwnInfo(own));
				}
			}

			return dict;
		}
	}
}