using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
	public class DefaultStat
	{
		public int HeroId;
		public int MaxHp;
		public int MaxMp;
		public int MeleeDamage;
		public int MagicDamage;
		public int MeleeDefence;
		public int MagicDefence;
		public int CriticalChance;
		public int CriticalDamage;
		public int AvoidChance;
	}

	public class Skill
	{
		public int SkillId;
		public string SkillName;
		public int SkillLevel;
		public string SkillExplain;
	}

	public class Item
	{
		public int ItemId;
		public string ItemName;
		public string ItemExplain;
	}

	// HeroInfo 클래스는 영웅들의 스탯과 각종 정보들을 가집니다
	[Serializable]
	public class HeroInfo
	{
		public int HeroId; // 영웅 코드
		public string HeroName; // 영웅 이름
		public int HeroClass; // 영웅 클래스
		public int HeroType; // 영웅 속성
		public string HeroDescription; // 영웅 설명
	}

	[Serializable]
	public class HeroInfoLoader : ILoader<int, HeroInfo>
	{
		public List<HeroInfo> heros = new List<HeroInfo>();

		public Dictionary<int, HeroInfo> LoadToDictionary()
		{
			Dictionary<int, HeroInfo> dict = new Dictionary<int, HeroInfo>();
			foreach (HeroInfo hero in heros)
			{
				dict.Add(hero.HeroId, hero);
			}

			return dict;
		}
	}
}