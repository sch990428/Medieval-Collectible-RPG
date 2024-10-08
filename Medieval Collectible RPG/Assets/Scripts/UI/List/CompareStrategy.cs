using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICompareStrategy<T, Tdata> where T : IListItem<Tdata>
{
	int Compare(T item1, T item2, bool isAsending);
}

public class CompareByHeroNameStrategy<T, Tdata> : ICompareStrategy<T, Tdata>
	where T : IListItem<Tdata>
	where Tdata : Data.CurrentPlayerOwnHero
{
	public int Compare(T item1, T item2, bool isAsending)
	{
		Data.HeroInfo heroA = LobbyManager.Instance.HeroDict[item1.ListItemInfo.HeroId];
		Data.HeroInfo heroB = LobbyManager.Instance.HeroDict[item2.ListItemInfo.HeroId];

		int nameComparison = isAsending ? heroB.HeroName.CompareTo(heroA.HeroName) : heroA.HeroName.CompareTo(heroB.HeroName);

		// 이름이 같다면 등급으로 비교
		if (nameComparison == 0)
		{
			int gradeComparison = item2.ListItemInfo.HeroGrade.CompareTo(item1.ListItemInfo.HeroGrade);

			if (gradeComparison == 0)
			{
				// 등급도 같으면 레벨로 비교
				return item2.ListItemInfo.HeroLevel.CompareTo(item1.ListItemInfo.HeroLevel);
			}

			return gradeComparison;
		}

		return nameComparison; // 등급이 다르면 등급 기준으로 정렬
	}
}

public class CompareByHeroGradeStrategy<T, Tdata> : ICompareStrategy<T, Tdata>
	where T : IListItem<Tdata>
	where Tdata : Data.CurrentPlayerOwnHero
{
	public int Compare(T item1, T item2, bool isAsending)
	{
		Data.HeroInfo heroA = LobbyManager.Instance.HeroDict[item1.ListItemInfo.HeroId];
		Data.HeroInfo heroB = LobbyManager.Instance.HeroDict[item2.ListItemInfo.HeroId];

		int gradeComparison = isAsending ? item2.ListItemInfo.HeroGrade.CompareTo(item1.ListItemInfo.HeroGrade) : item1.ListItemInfo.HeroGrade.CompareTo(item2.ListItemInfo.HeroGrade);

		// 등급이 같다면 레벨로 비교
		if (gradeComparison == 0)
		{
			int levelComparison = item2.ListItemInfo.HeroLevel.CompareTo(item1.ListItemInfo.HeroLevel);

			if (levelComparison == 0)
			{
				// 레벨도 같으면 이름으로 비교
				return heroA.HeroName.CompareTo(heroB.HeroName);
			}

			return levelComparison;
		}

		return gradeComparison; // 등급이 다르면 등급 기준으로 정렬
	}
}

public class CompareByHeroLevelStrategy<T, Tdata> : ICompareStrategy<T, Tdata>
	where T : IListItem<Tdata>
	where Tdata : Data.CurrentPlayerOwnHero
{
	public int Compare(T item1, T item2, bool isAsending)
	{
		Data.HeroInfo heroA = LobbyManager.Instance.HeroDict[item1.ListItemInfo.HeroId];
		Data.HeroInfo heroB = LobbyManager.Instance.HeroDict[item2.ListItemInfo.HeroId];

		int levelComparison = isAsending ? item2.ListItemInfo.HeroLevel.CompareTo(item1.ListItemInfo.HeroLevel) : item1.ListItemInfo.HeroLevel.CompareTo(item2.ListItemInfo.HeroLevel);

		// 레벨이 같다면 등급으로 비교
		if (levelComparison == 0)
		{
			int gradeComparison = item2.ListItemInfo.HeroGrade.CompareTo(item1.ListItemInfo.HeroGrade);

			if (gradeComparison == 0)
			{
				// 등급도 같으면 이름으로 비교
				return heroA.HeroName.CompareTo(heroB.HeroName);
			}

			return gradeComparison;
		}

		return levelComparison; // 등급이 다르면 등급 기준으로 정렬
	}
}