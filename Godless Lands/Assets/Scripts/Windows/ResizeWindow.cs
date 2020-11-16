using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.Events;

namespace Windows
{
	public class ResizeWindow : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
	{

		public Vector2 minSize;
		public Vector2 maxSize;

		[SerializeField] RectTransform resizeWindow;
		private RectTransform windowParent;

		Vector2 previousPosition;


        private void Awake()
        {
			windowParent = resizeWindow.parent as RectTransform;
			float x = PlayerPrefs.GetFloat("chatWidth", minSize.x);
			float y = PlayerPrefs.GetFloat("chatHeight", minSize.y);
			resizeWindow.sizeDelta = new Vector2(x, y);
			resizeWindow.anchoredPosition = new Vector3(0, y);
		}

		public void OnPointerDown(PointerEventData data)
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(
				windowParent,
				data.position,
				data.pressEventCamera,
				out previousPosition
			);
		}

		public void OnDrag(PointerEventData data)
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(
				windowParent,
				data.position,
				data.pressEventCamera,
				out Vector2 position
			);

			Vector2 offset = position - previousPosition;
			Vector2 delta = resizeWindow.sizeDelta;

			delta = new Vector2(
				Mathf.Clamp(delta.x + offset.x, minSize.x, maxSize.x),
				Mathf.Clamp(delta.y + offset.y, minSize.y, maxSize.y)
			);

			resizeWindow.sizeDelta = delta;
			resizeWindow.anchoredPosition = new Vector3(0, delta.y);

			previousPosition = position;
		}

		public void OnEndDrag(PointerEventData data)
		{
			PlayerPrefs.SetFloat("chatWidth", resizeWindow.sizeDelta.x);
			PlayerPrefs.SetFloat("chatHeight", resizeWindow.sizeDelta.y);
		}
	}
}