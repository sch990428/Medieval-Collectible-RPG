using Data;
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
	public override void Awake()
	{
		base.Awake();
		Init();
	}

	public void Init()
	{
		
	}

	public Loader LoadJson<Loader, T>(string path) where Loader : ILoader<T>
	{
		TextAsset textAsset = ResourceManager.Instance.Load<TextAsset>(path);
		return JsonUtility.FromJson<Loader>(textAsset.text);
	}

	public Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
	{
		TextAsset textAsset = ResourceManager.Instance.Load<TextAsset>(path);
		return JsonUtility.FromJson<Loader>(textAsset.text);
	}
}
