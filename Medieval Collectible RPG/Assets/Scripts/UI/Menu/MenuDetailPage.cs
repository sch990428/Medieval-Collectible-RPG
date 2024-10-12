using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDetailPage : MonoBehaviour
{
	[SerializeField]
	protected SideMenu sideMenu;

	protected void Init()
	{
		sideMenu.onSelectedMenuChanged -= UpdateDetails;
		sideMenu.onSelectedMenuChanged += UpdateDetails;
	}

	protected virtual void UpdateDetails(int index)
	{

	}
}
