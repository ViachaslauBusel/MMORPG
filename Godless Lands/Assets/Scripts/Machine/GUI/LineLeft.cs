using Machines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineLeft : MonoBehaviour
{
    private RectTransform rect;
    private RecipeComponent component;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        component = GetComponentInParent<RecipeComponent>();
    }

    private void Start()
    {
      //  rect.localScale = new Vector3(1.0f / rect.lossyScale.x, 1.0f / rect.lossyScale.y, 1.0f / rect.lossyScale.z);
    }

    private void Update()
    {
        if(component.IsChild())
        rect.sizeDelta = new Vector2(34.0f, 2.0f / rect.lossyScale.y);
    }
}
