using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ColorPicker : MonoBehaviour {

	[SerializeField] Image targetImage;
	RectTransform targetImageTransform;

	[SerializeField] RectTransform colorPickerPanel;
	[SerializeField] Image paletteTexture;
	RectTransform paletteRectTransform;

	public UnityEvent OnColorChange;

	public void Show(){
		if (colorPickerPanel.gameObject.activeSelf) return;
		targetImageTransform = targetImage.GetComponent<RectTransform>();
		paletteRectTransform = paletteTexture.GetComponent<RectTransform>();

		colorPickerPanel.gameObject.SetActive(true);
	}

	void Update(){
		if (Input.GetMouseButtonDown(0)) {
			bool clickedOnColorPicker = RectTransformUtility.RectangleContainsScreenPoint(
				colorPickerPanel,
				Input.mousePosition,
				null
			);
			bool clickedOnTargetImage = RectTransformUtility.RectangleContainsScreenPoint(
				targetImageTransform,
				Input.mousePosition,
				null
			);
			

			if (colorPickerPanel.gameObject.activeSelf && !clickedOnColorPicker && !clickedOnTargetImage){
				colorPickerPanel.gameObject.SetActive(false);
			}

		}

		if (Input.GetMouseButton(0)){
			bool clickedOnPalette = RectTransformUtility.RectangleContainsScreenPoint(
				paletteRectTransform,
				Input.mousePosition,
				null
			);

			if (clickedOnPalette){ 
				ApplyColor();
			}
		}
	}

	void ApplyColor(){
		Vector2 point = paletteRectTransform.InverseTransformPoint(Input.mousePosition);

		targetImage.color = paletteTexture.sprite.texture.GetPixel(
			(int)point.x * 2,
			(int)point.y * 2	
		);

		if (OnColorChange != null){ 
			OnColorChange.Invoke();
		}
	}
}
