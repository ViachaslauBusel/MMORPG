using UnityEngine;

namespace Windows
{
    public abstract class WindowElement : MonoBehaviour
    {
        private Window _parentWindow;

        public Window ParentWindow => _parentWindow;

        protected virtual void Awake()
        {
            _parentWindow = GetComponentInParent<Window>();
            _parentWindow.OnOpen += HandleWindowOpen;
            _parentWindow.OnClose += HandleWindowClose;
        }

        protected abstract void HandleWindowClose();
        protected abstract void HandleWindowOpen();

        private void OnDestroy()
        {
            _parentWindow.OnOpen -= HandleWindowOpen;
            _parentWindow.OnClose -= HandleWindowClose;
        }
    }
}
