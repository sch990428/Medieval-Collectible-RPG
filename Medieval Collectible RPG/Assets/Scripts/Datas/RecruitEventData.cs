using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
	[Serializable]
	public class RecruitEventInfo
	{
		public int RecruitId; // 징집이벤트 ID
		public string RecruitName; // 징집이벤트 이름
		public string RecruitShortName; // 징집이벤트 메뉴명
		public string RecruitDesciption; // 징집이벤트 설명
		public string RecruitBackgrund; // 징집이벤트 배경 이미지 경로
	}

	public class RecruitDataLoader : ILoader<int, RecruitEventInfo>
	{
		public List<RecruitEventInfo> recruits = new List<RecruitEventInfo>();

		public Dictionary<int, RecruitEventInfo> LoadToDictionary()
		{
			Dictionary<int, RecruitEventInfo> dict = new Dictionary<int, RecruitEventInfo>();
			foreach (RecruitEventInfo recruit in recruits)
			{
				dict.Add(recruit.RecruitId, recruit);
			}

			return dict;
		}
	}
}