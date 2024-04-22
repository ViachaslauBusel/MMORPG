using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.ConfirmationDialog;

namespace  UI.ConfirmationDialog
{
    public class ConfirmationDialogController
    {
        private SortedSet<ConfirmationRequest> _requests = new SortedSet<ConfirmationRequest>();
        private ConfirmationDialogWindow _window;
        private ConfirmationRequest _currentRequest;

        public ConfirmationDialogController(ConfirmationDialogWindow window)
        {
            _window = window;

            _window.OnClose += () =>
            {
                _requests.Remove(_currentRequest);
                ProcessNextRequest();
            };
        }

        public void AddRequest(string description, Action callYES, Action callNO, float waitTime = 0)
        {
            var request = new ConfirmationRequest
            (
               description,
               callYES,
               callNO,
               waitTime
            );
            _requests.Add(request);
            ProcessNextRequest();
        }

        private void ProcessNextRequest()
        {
            // If we have a request and the window is not open
            // Or the next request has a higher priority than the current request
            if (_requests.Count > 0 && (!_window.IsOpen 
            || (_requests.Max.Priority - _currentRequest.Priority > 0)))
            {
                 _currentRequest = _requests.Max;
                _window.Open(_currentRequest);
            }
        }
    }
}
