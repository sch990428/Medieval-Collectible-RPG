using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_RecruitedHeroList : MonoBehaviour
{
	private void OnDisable()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			ResourceManager.Instance.Destroy(transform.GetChild(i).gameObject);
		}
	}
}
