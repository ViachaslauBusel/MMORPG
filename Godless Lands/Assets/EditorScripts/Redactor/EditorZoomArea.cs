using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorZoomArea
{

    private const float kEditorWindowTabHeight = 21.0f;
    private const float minScale = 0.1f, maxScale = 3.0f;
    private static Matrix4x4 _prevGuiMatrix;
   
    public static event Action<Vector2> mouseScroll;

    public static float Begin(float scale)
    {
        scale = CalculateScale(scale);
        _prevGuiMatrix = GUI.matrix;
        GUI.EndGroup();
        Rect clippedArea = CalculateRect(scale);
       // clippedArea.center = new Vector2(Screen.width/2.0f, Screen.height/2.0f);

       GUI.BeginGroup(clippedArea);


        
        Matrix4x4 translation = Matrix4x4.TRS(clippedArea.TopLeft(), Quaternion.identity, Vector3.one);
        Matrix4x4 scaleMatrix = Matrix4x4.Scale(new Vector3(scale, scale, 1.0f));
        GUI.matrix = translation * scaleMatrix * translation.inverse * GUI.matrix;

        return scale;
    }

    public static Rect CalculateRect(float scale)
    {
       return new Rect(
            0 ,
            kEditorWindowTabHeight, 
            Width(scale),
            Height(scale) 
            );
    }
    public static float Width(float scale)
    {
        return Screen.width * (1.0f / scale);
    }
    public static float Height(float scale)
    {
        return Screen.height * (1.0f / scale);
    }
    private static float CalculateScale(float scale)
    {
        if (Event.current.type == EventType.ScrollWheel)
        {
            Vector2 delta = Event.current.delta;
            float _s = Mathf.Clamp(scale + delta.y * 0.01f, minScale, maxScale); 


            Vector2 mousePosition = Event.current.mousePosition;
            Vector2 dif = (mousePosition * (1.0f / _s)) - (mousePosition * (1.0f / scale));
            scale = _s;

           

            //  Debug.Log(mousePosition);
            mouseScroll?.Invoke(dif);

            GUI.changed = true;
        }
        return scale;
    }

    public static void End()
    {
        GUI.matrix = _prevGuiMatrix;
        GUI.EndGroup();
       
        GUI.BeginGroup(new Rect(0.0f, kEditorWindowTabHeight, Screen.width, Screen.height));
    }
   
   
}
public static class RectExtensions
{
    public static Vector2 TopLeft(this Rect rect)
    {
        return new Vector2(rect.xMin, rect.yMin);
    }
}