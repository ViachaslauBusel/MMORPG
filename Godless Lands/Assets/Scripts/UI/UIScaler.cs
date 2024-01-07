using Hotbar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScaler : MonoBehaviour {

   
    private CanvasScaler[] canvasScaler;
    public float scaleUI;
    private float minScaleUI = 0.4f;
    private float screenResolution = 1920.0f;
    private SkillsBarMove barMove;

    private void Awake()
    {
        canvasScaler = GetComponentsInChildren<CanvasScaler>();
        barMove = GetComponentInChildren<SkillsBarMove>();
    }

    public void ScaleUI()
    {
        foreach (CanvasScaler _canvas in canvasScaler)
        {
            _canvas.scaleFactor = (Screen.width / screenResolution) * scaleUI;
        }
    }

    public void apply(float value)
    {
        scaleUI = minScaleUI + value;
        ScaleUI();
        barMove.SetBorder((Screen.width / screenResolution) * scaleUI);//Задать границы передвижение панели умений
    }

    

}
