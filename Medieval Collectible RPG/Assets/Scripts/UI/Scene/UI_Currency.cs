using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Currency : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI goldText;
	[SerializeField]
	private TextMeshProUGUI cashText;

	private long gold;
	private int cash;

	private string FormatNumber(long number)
	{
		if (number >= 1_0000_0000_0000) // 조단위
		{
			double formatted = Math.Floor(number / 1_0000_0000_0000.0 * 10) / 10; // 소수점 첫 자리 내림
			return formatted.ToString("0.#") + "조";
		}
		else if (number >= 1_0000_0000) // 억단위
		{
			double formatted = Math.Floor(number / 1_0000_0000.0 * 10) / 10; // 소수점 첫 자리 내림
			return formatted.ToString("0.#") + "억";
		}
		else if (number >= 1_0000) // 만단위
		{
			double formatted = Math.Floor(number / 1_0000.0 * 10) / 10; // 소수점 첫 자리 내림
			return formatted.ToString("0.#") + "만";
		}
		else
		{
			return number.ToString(); // 10000 미만일 경우 그냥 숫자 출력
		}
	}


	private void Awake()
	{
		gold = DataManager.Instance.UserInfo.UserGold;
		cash = DataManager.Instance.UserInfo.UserCash;

		goldText.text = FormatNumber(gold);
		cashText.text = cash.ToString();
	}
}
