using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 데이터를 T 클래스로 불러올 ILoader 인터페이스를 선언
public interface ILoader<T>
{
	T LoadToClass();
}

// 데이터를 딕셔너리로 불러올 ILoader 인터페이스를 선언
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

	// Loader는 ILoader 인터페이스를 구현한 클래스
	// 특정한 로더 클래스를 지정할 수 있도록 만들어짐
	// 그 클래스는 데이터를 T 혹은 Dictionary<Key-Value> 타입 등으로 반환

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
