using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_UserProfile : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI usernameText;
	[SerializeField] private TextMeshProUGUI userLevelText;
	[SerializeField] private int userExp;

	private Data.UserInfo LobbyData;

	private void Awake()
	{
		LobbyData = LobbyManager.Instance.UserInfo;

		usernameText.text = LobbyData.UserName;
		userLevelText.text = $"Lv. {LobbyData.UserLevel.ToString()}";
	}


    void Update()
    {
        
    }
}
