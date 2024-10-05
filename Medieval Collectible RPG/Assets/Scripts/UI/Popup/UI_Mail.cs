using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMail : MonoBehaviour
{
	private Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void OnEnable()
	{
		// 설정창에서 설정 값들을 활성화
	}

	public void Close()
	{
		StartCoroutine(CloseAfterDelay());
	}

	private IEnumerator CloseAfterDelay()
	{
		animator.SetTrigger("close");	
		yield return new WaitForSeconds(0.5f);
		gameObject.SetActive(false);
		animator.ResetTrigger("close");
	}
}
