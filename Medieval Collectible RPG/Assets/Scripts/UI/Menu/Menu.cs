using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideMenu : MonoBehaviour
{
	protected Dictionary<int, Button> menuDict;

	protected int selectedMenuId;
    public Action<int> onSelectedMenuChanged;

    protected void Init()
    {
        selectedMenuId = 0;
    }

    protected void ChangeMenuIndex(int select)
    {
        selectedMenuId = select;
		onSelectedMenuChanged.Invoke(select);
    }
}
