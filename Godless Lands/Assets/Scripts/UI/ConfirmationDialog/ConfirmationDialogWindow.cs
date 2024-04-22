using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.ConfirmationDialog
{
    public class ConfirmationDialogWindow : MonoBehaviour
    {
        [SerializeField]
        private Text description;
        [SerializeField]
        private Button buttonYES, buttonNO;
        [SerializeField]
        private Image _timer;
        private Canvas canvas;
        private int _generatorId = 0;
        private int _requestId;

        public event Action OnClose;

        public bool IsOpen { get; internal set; }

        private void Awake()
        {
            canvas = GetComponent<Canvas>();
            canvas.enabled = false;
        }

        private async void Open(string description, Action callYES, Action callNO, float endWaitTime = 0)
        {
            _requestId = ++_generatorId;
            canvas.enabled = true;
            this.description.text = description;
            buttonYES.onClick.RemoveAllListeners();
            buttonYES.onClick.AddListener(() => { callYES?.Invoke(); Close(); });

            buttonNO.gameObject.SetActive(true);
            buttonNO.onClick.RemoveAllListeners();
            if (callNO != null)
                buttonNO.onClick.AddListener(() => { callNO?.Invoke(); Close(); });
            else
                buttonNO.gameObject.SetActive(false);


            if (endWaitTime > 0)
            {
                int waitRequestID = _requestId;
                float totalWaitTime = endWaitTime - Time.time;
                float timer = totalWaitTime;
                while (timer > 0 && waitRequestID == _requestId)
                {
                    _timer.fillAmount = Mathf.Clamp01(timer / endWaitTime);
                    await UniTask.Yield();
                    timer -= Time.deltaTime;
                }
                if (waitRequestID == _requestId)
                {
                    callNO?.Invoke();
                    Close();
                }
            }
            else _timer.fillAmount = 0f;
        }

        private void Close()
        {
            _requestId = 0;
            canvas.enabled = false;
            OnClose?.Invoke();
        }

        internal void Open(ConfirmationRequest currentRequest)
        {
           Open(currentRequest.Description, currentRequest.CallYes, currentRequest.CallNo, currentRequest.EndWaitTime);
        }
    }
}
