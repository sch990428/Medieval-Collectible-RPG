using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 데이터를 T 클래스로 불러올 ILoader 인터페이스를 선언한다
public interface ILoader<T>
{
	T LoadToClass();
}

// 데이터를 딕셔너리로 불러올 ILoader 인터페이스를 선언한다
public interface ILoader<Key, Value>
{
	Dictionary<Key, Value> LoadToDictionary();
}

public class DataManager : Singleton<DataManager>
{
	public Data.UserInfo UserInfo { get; private set; } = new Data.UserInfo();
	public Dictionary<int, Data.HeroInfo> HeroDict { get; private set; } = new Dictionary<int, Data.HeroInfo>();

	public void Awake()
	{
		Init();
	}

	public void Init()
	{
		UserInfo = LoadJson<Data.UserInfoData, Data.UserInfo>("Data/UserInfo").LoadToClass();
		HeroDict = LoadJson<Data.HeroData, int, Data.HeroInfo>("Data/Hero").LoadToDictionary();

		foreach (KeyValuePair<int, Data.HeroInfo> h in HeroDict)
		{
			Debug.Log(h.Value.HeroName);
		}
	}

	Loader LoadJson<Loader, T>(string path) where Loader : ILoader<T>
	{
		TextAsset textAsset = ResourceManager.Instance.Load<TextAsset>(path);
		return JsonUtility.FromJson<Loader>(textAsset.text);
	}

	Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
	{
		TextAsset textAsset = ResourceManager.Instance.Load<TextAsset>(path);
		return JsonUtility.FromJson<Loader>(textAsset.text);
	}
}
