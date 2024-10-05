using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : Singleton<LobbyManager>
{
	public Data.UserInfo UserInfo { get; private set; } = new Data.UserInfo(); // 플레이어 정보
	public Dictionary<int, Data.HeroInfo> HeroDict { get; private set; } = new Dictionary<int, Data.HeroInfo>(); // 영웅 정보
	public Dictionary<int, Data.CurrentPlayerOwnHero> OwnHeroDict { get; private set; } = new Dictionary<int, Data.CurrentPlayerOwnHero>(); // 플레이어의 소유 영웅 정보

	public override void Awake()
	{
		base.Awake();

		UserInfo = DataManager.Instance.LoadJson<Data.UserInfoData, Data.UserInfo>("Data/UserInfo").LoadToClass();
		HeroDict = DataManager.Instance.LoadJson<Data.HeroData, int, Data.HeroInfo>("Data/Hero").LoadToDictionary();
		OwnHeroDict = DataManager.Instance.LoadJson<Data.CurrentPlayerOwnHeroData, int, Data.CurrentPlayerOwnHero>("Data/OwnInfo").LoadToDictionary();
	}
}