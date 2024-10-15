using UnityEngine;
using CraftingStations.UI.RecipeCrafting;

namespace CraftingStations.GUI
{
    public class HorizaontalLineDrawer : MonoBehaviour
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
            //  rect.localScale = new Vector3(1.0f / rect.lossyScale.x, 1.0f / rect.lossyScale.y, 1.0f / rect.lossyScale.z);
        }

        private void Update()
        {
            if (_component.IsChild())
                _rect.sizeDelta = new Vector2(34.0f, 2.0f / _rect.lossyScale.y);
        }
    }
}