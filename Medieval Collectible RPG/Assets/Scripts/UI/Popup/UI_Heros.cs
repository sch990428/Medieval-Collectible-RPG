using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Heros : MonoBehaviour
{
	// private Animator animator;

	private void Awake()
	{
		// animator = GetComponent<Animator>();
	}

	private void OnEnable()
	{
		// ����â���� ���� ������ Ȱ��ȭ�Ѵ�
	}

	public void Close()
	{
		StartCoroutine(CloseAfterDelay());
	}

	private IEnumerator CloseAfterDelay()
	{
		//animator.SetTrigger("close");	
		yield return new WaitForSeconds(0.5f);
		gameObject.SetActive(false);
		//animator.ResetTrigger("close");
	}
}
