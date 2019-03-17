using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayerItem : MonoBehaviour{

	[SerializeField] Text layerName;
	[SerializeField] Toggle toggle;
	[SerializeField] Image image;

	public delegate void LayerChangedEvent(LayerItem layer, bool visible, Color color);
	public LayerChangedEvent OnLayerChangedEvent;
	
	public void SetLayerData(ChatLayer layer){
		layerName.text = layer.name;
		toggle.isOn = layer.visible;
		image.color = layer.color;
	}

	public void OnLayerChange(){
		if (OnLayerChangedEvent != null){
			OnLayerChangedEvent(this, toggle.isOn, image.color);
		}
	}
}
