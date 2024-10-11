using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : Singleton<LobbyManager>
{
	public Data.UserInfo UserInfo { get; private set; } = new Data.UserInfo(); // 플레이어 정보
	public Dictionary<int, Data.HeroInfo> HeroDict { get; private set; } = new Dictionary<int, Data.HeroInfo>(); // 영웅 정보
	public Dictionary<int, Data.CurrentPlayerOwnHero> OwnHeroDict { get; private set; } = new Dictionary<int, Data.CurrentPlayerOwnHero>(); // 플레이어의 소유 영웅 정보
	public Dictionary<int, Data.RecruitEventInfo> RecruitEventDict { get; private set; } = new Dictionary<int, Data.RecruitEventInfo>(); // 영웅 징집 이벤트 정보

	public override void Awake()
	{
		base.Awake();

		UserInfo = DataManager.Instance.LoadJson<Data.UserInfoLoader, Data.UserInfo>("Data/UserInfo").LoadToClass();
		HeroDict = DataManager.Instance.LoadJson<Data.HeroInfoLoader, int, Data.HeroInfo>("Data/Hero").LoadToDictionary();
		OwnHeroDict = DataManager.Instance.LoadJson<Data.CurrentPlayerOwnHeroLoader, int, Data.CurrentPlayerOwnHero>("Data/OwnInfo").LoadToDictionary();
		RecruitEventDict = DataManager.Instance.LoadJson<Data.RecruitDataLoader, int, Data.RecruitEventInfo>("Data/Recruit").LoadToDictionary();
	}
}