using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IListItem<T> : IPointerClickHandler
{
	T ListItemInfo { get; set; }
	public void Init();
}
