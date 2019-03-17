using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ColorPicker : MonoBehaviour {
    
	[SerializeField] Image targetImage;
	[SerializeField] RectTransform colorPickerPanel;

	[SerializeField] Slider redColorSlider;
	[SerializeField] Text redColorValue;

	[SerializeField] Slider greenColorSlider;
	[SerializeField] Text greenColorValue;

	[SerializeField] Slider blueColorSlider;
	[SerializeField] Text blueColorValue;

	[SerializeField] Slider alphaSlider;
	[SerializeField] Text alphaValue;

	public UnityEvent OnColorChange;

	void Start(){
		redColorSlider.onValueChanged.AddListener(delegate {ApplyColor();});
		greenColorSlider.onValueChanged.AddListener(delegate {ApplyColor();});
		blueColorSlider.onValueChanged.AddListener(delegate {ApplyColor();});
		alphaSlider.onValueChanged.AddListener(delegate {ApplyColor();});
	}

	void Update(){
		if (Input.GetMouseButtonDown(0)) {
			bool clickedOnColorPicker = RectTransformUtility.RectangleContainsScreenPoint(
				colorPickerPanel,
				Input.mousePosition,
				null
			);
			bool clickedOnTargetImage = RectTransformUtility.RectangleContainsScreenPoint(
				targetImage.GetComponent<RectTransform>(),
				Input.mousePosition,
				null
			);

			if (colorPickerPanel.gameObject.activeSelf && !clickedOnColorPicker && !clickedOnTargetImage){
				colorPickerPanel.gameObject.SetActive(false);
			}
		}
	}

	public void Show(){
		if (colorPickerPanel.gameObject.activeSelf) return;

		Color32 targetColor = targetImage.color;
		redColorSlider.value = (float)targetColor.r;
		greenColorSlider.value = (float)targetColor.g;
		blueColorSlider.value = (float)targetColor.b;
		alphaSlider.value = (float)targetColor.a;

		colorPickerPanel.gameObject.SetActive(true);
	}

	void ApplyColor(){
		redColorValue.text = redColorSlider.value.ToString();
		greenColorValue.text = greenColorSlider.value.ToString();
		blueColorValue.text = blueColorSlider.value.ToString();
		alphaValue.text = alphaSlider.value.ToString();

		targetImage.color = new Color32(
			(byte)redColorSlider.value, 
			(byte)greenColorSlider.value, 
			(byte)blueColorSlider.value, 
			(byte)alphaSlider.value
		);

		OnColorChange.Invoke();
	}
}
