using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.Events;

public class Chat_ResizeWindow : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler {
	
	public Vector2 minSize;
	public Vector2 maxSize;

 	[SerializeField] RectTransform resizeTarget;

	Vector2 currentPointerPosition;
	Vector2 previousPointerPosition;

	public UnityEvent OnChatResize;

	public void OnPointerDown (PointerEventData data) {
			if(resizeTarget == null) return;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(
				resizeTarget, 
				data.position, 
				data.pressEventCamera, 
				out previousPointerPosition
			);
	}

	public void OnDrag (PointerEventData data) {
		if(resizeTarget == null) return;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(
			resizeTarget, 
			data.position, 
			data.pressEventCamera, 
			out currentPointerPosition
		);
			
		Vector2 newSize = currentPointerPosition - previousPointerPosition;
		Vector2 delta = resizeTarget.sizeDelta;

		delta += new Vector2 (newSize.x, newSize.y);
		delta = new Vector2 (
			Mathf.Clamp (delta.x, minSize.x, maxSize.x),
			Mathf.Clamp (delta.y, minSize.y, maxSize.y)
		);

		resizeTarget.sizeDelta = delta;

		previousPointerPosition = currentPointerPosition;

		if (OnChatResize != null){
			OnChatResize.Invoke();
		}
	}

	public void Start(){
		float x = PlayerPrefs.GetFloat("chatWidth", minSize.x);
		float y = PlayerPrefs.GetFloat("chatHeight", minSize.y);
		resizeTarget.sizeDelta = new Vector2(x,y);
	}

	public void OnEndDrag(PointerEventData data){
		PlayerPrefs.SetFloat("chatWidth", resizeTarget.sizeDelta.x);
		PlayerPrefs.SetFloat("chatHeight", resizeTarget.sizeDelta.y);
	}
}
