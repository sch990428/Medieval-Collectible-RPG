using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
	// UserInfo 클래스는 플레이어들의 계정 정보를 관리합니다
	[Serializable]
	public class UserInfo
	{
		public int UserId; // 플레이어 식별코드
		public string UserName; // 플레이어 이름
		public int UserLevel; // 플레이어 레벨
		public int UserExp; // 플레이어 경험치
		public long UserGold; // 골드
		public int UserCash; // 캐시
	}

	// HeroOwn 클래스는 플레이어들이 가진 영웅들의 정보와 성장 상태를 관리합니다
	// 같은 영웅을 여럿 모집할 수 있게 만들어 나중에 grade를 올릴 수 있어야합니다
	[Serializable]
	public class HeroOwnInfo
	{
		public int HeroOwnId; // 소유정보 코드
		public int UserId; // 플레이어 식별코드
		public int HeroId; // 영웅 코드
		public int HeroGrade; // 해당 영웅의 등급
		public int HeroLevel; // 해당 영웅의 레벨
	}

	[Serializable]
	public class UserInfoData : ILoader<UserInfo>
	{
		// 이런 식으로 모든 유저의 정보를 불러오는 것은 위험하지만
		// 지금은 포트폴리오 용도의 싱글플레이 게임이므로 전부 불러와서 특정 사용자를 찾는다
		// 추후 서버가 연동될 경우 DB의 SELECT문이나 서버 로직으로 대체한다
		public List<UserInfo> users = new List<UserInfo>();
		// public UserInfo UserInfo = new UserInfo();

		public UserInfo LoadToClass()
		{
			UserInfo info = new UserInfo();

			foreach (UserInfo user in users)
			{
				if (user.UserId == 19990428)
				{
					info = user;
				}
			}

			return info;
		}
	}
}

