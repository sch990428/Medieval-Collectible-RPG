using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : Singleton<LobbyManager>
{
	public Data.UserInfo UserInfo { get; private set; } = new Data.UserInfo();
	public Dictionary<int, Data.HeroInfo> HeroDict { get; private set; } = new Dictionary<int, Data.HeroInfo>();
	public Dictionary<int, Data.CurrentPlayerOwnInfo> OwnHeroDict { get; private set; } = new Dictionary<int, Data.CurrentPlayerOwnInfo>();

	public override void Awake()
	{
		base.Awake();

		UserInfo = DataManager.Instance.LoadJson<Data.UserInfoData, Data.UserInfo>("Data/UserInfo").LoadToClass();
		HeroDict = DataManager.Instance.LoadJson<Data.HeroData, int, Data.HeroInfo>("Data/Hero").LoadToDictionary();
		OwnHeroDict = DataManager.Instance.LoadJson<Data.CurrentPlayerOwnData, int, Data.CurrentPlayerOwnInfo>("Data/OwnInfo").LoadToDictionary();

		foreach (KeyValuePair<int, Data.CurrentPlayerOwnInfo> h in OwnHeroDict)
		{
			Debug.Log($"{HeroDict[h.Value.HeroId].HeroName}의 레벨 {h.Value.HeroLevel}");
		}
	}
}