using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_HeroModel : MonoBehaviour, IPointerClickHandler, IDragHandler, IScrollHandler
{
	RawImage rawImage;
	[SerializeField]
	Camera heroModelCamera;

	Animator animator;

	private float rotateSpeed = 0.08f;
	private float zoomSpeed = 10.0f;

	private float defaltFieldOfView = 90f;

	private GameObject targetHero;
	public GameObject TargetHero
	{
		get { return targetHero; }
		set { 
			targetHero = value; 
			animator = targetHero.GetComponent<Animator>(); 
		}
	}

	private void Awake()
	{
		rawImage = GetComponent<RawImage>();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Vector2 localCursor;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(
			rawImage.rectTransform, eventData.position, eventData.pressEventCamera, out localCursor
		);

		Rect rect = rawImage.rectTransform.rect;
		float normalizedX = (localCursor.x - rect.x) / rect.width;
		float normalizedY = (localCursor.y - rect.y) / rect.height;

		RaycastHit hit;
		Ray ray = heroModelCamera.ViewportPointToRay(new Vector3(normalizedX, normalizedY, 0));

		Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);

		if (Physics.Raycast(ray, out hit))
		{
			animator.SetTrigger("Touch");
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		targetHero.transform.Rotate(0f, -eventData.delta.x * rotateSpeed, 0f, Space.World);
	}

	public void OnScroll(PointerEventData eventData)
	{
		heroModelCamera.fieldOfView += -eventData.scrollDelta.y * zoomSpeed;
		heroModelCamera.fieldOfView = Mathf.Clamp(heroModelCamera.fieldOfView, 40f, 120f);
	}
}
