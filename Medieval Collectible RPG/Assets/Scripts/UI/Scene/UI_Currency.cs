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
		if (number >= 1_0000_0000_0000) // ������
		{
			double formatted = Math.Floor(number / 1_0000_0000_0000.0 * 10) / 10; // �Ҽ��� ù �ڸ� ����
			return formatted.ToString("0.#") + "��";
		}
		else if (number >= 1_0000_0000) // �����
		{
			double formatted = Math.Floor(number / 1_0000_0000.0 * 10) / 10; // �Ҽ��� ù �ڸ� ����
			return formatted.ToString("0.#") + "��";
		}
		else if (number >= 1_0000) // ������
		{
			double formatted = Math.Floor(number / 1_0000.0 * 10) / 10; // �Ҽ��� ù �ڸ� ����
			return formatted.ToString("0.#") + "��";
		}
		else
		{
			return number.ToString(); // 10000 �̸��� ��� �׳� ���� ���
		}
	}


	private void Awake()
	{
		gold = DataManager.Instance.UserInfo.UserGold;
		cash = DataManager.Instance.UserInfo.UserCash;

		Debug.Log(gold);
		goldText.text = FormatNumber(gold);
		cashText.text = cash.ToString();
	}
}
