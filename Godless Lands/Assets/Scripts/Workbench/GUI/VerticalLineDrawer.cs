using UnityEngine;
using Workbench.UI.RecipeCrafting;

namespace Workbench.GUI
{
    public class VerticalLineDrawer : MonoBehaviour
    {
        private RectTransform _rect;
        private RecipeComponent _component;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            _component = GetComponentInParent<RecipeComponent>();
        }

        private void Start()
        {
            // rect.localScale = new Vector3(1.0f / rect.lossyScale.x, 1.0f / rect.lossyScale.y, 1.0f / rect.lossyScale.z);
        }

        private void Update()
        {
            _rect.sizeDelta = new Vector2(2.0f / _rect.lossyScale.x, _component.GetVertical() / _rect.lossyScale.y);
        }
    }
}