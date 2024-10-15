using Cells;
using UnityEngine;
using UnityEngine.EventSystems;
using CraftingStations.UI.RecipeCrafting;

namespace CraftingStations.GUI
{
    public class EventRecipeCelll : EventTrigger
    {
        private bool _focus = false; // Indicator whether the cursor is on the cell
        private float _time_show = 1.0f; // Time after which to show information after hovering the cursor
        private float _timer;
        protected GameObject _informer;
        private RecipeComponent _component;
        private Canvas _parentCanvas;

        private void Awake()
        {
            _component = GetComponentInParent<RecipeComponent>();
        }
        private void Start()
        {
            _parentCanvas = GetComponentInParent<Canvas>();
        }

        public override void OnPointerEnter(PointerEventData data) // Triggered when the cursor is hovered
        {
            _focus = true;
        }

        public override void OnPointerExit(PointerEventData data) // Triggered when the cursor is removed
        {
            ResetTimerAndFocus();
            HideInfo();
        }

        private void Update()
        {
            if (_focus)
            {
                _timer -= Time.deltaTime;
                if (_timer < 0.0f) // Timer to show information about the object
                {
                    ResetTimerAndFocus();
                    ShowInfo();
                }
            }
        }

        private void ResetTimerAndFocus()
        {
            _timer = _time_show;
            _focus = false;
        }

        public virtual void ShowInfo()
        {
            if (_informer == null)
            {
                _informer = ItemCellEvent.ItemInfo(_parentCanvas.transform, _component.GetItem());
            }
        }

        public virtual void HideInfo()
        {
            if (_informer != null)
            {
                Destroy(_informer);
                _informer = null;
            }
        }
    }
}