using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IList<T, Tdata> where T : IListItem<Tdata>
{
	public void Init();
	public void Add(T item);
	public void Remove(T item);
	public T CreateItem(string path);
}

public class UI_List<T, Tdata> : MonoBehaviour, IList<T, Tdata> where T : MonoBehaviour, IListItem<Tdata>
{
	protected List<T> items;

	protected virtual void Awake()
	{
		items = new List<T>();
	}

	public virtual void Init()
	{
		
	}

	public void Add(T item)
	{
		items.Add(item);
		item.transform.SetParent(transform, false); // worldPositionStays를 false로 하여 로컬 벡터를 유지하도록 함
	}

	public void Remove(T item)
	{

	}

	public T CreateItem(string path)
	{
		GameObject go = ResourceManager.Instance.Instantiate(path);
		T item = go.GetComponent<T>();

		return item;
	}
}