using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Trade.UI
{
    internal class TradeWindowButtonController : MonoBehaviour
    {
        private TradeInputHandler _tradeInputHandler;

        [Inject]
        public void Construct(TradeInputHandler tradeInputHandler)
        {
            _tradeInputHandler = tradeInputHandler;
        }

        public void OnAcceptButtonClicked()
        {
            _tradeInputHandler.AcceptTrade();
        }

        public void OnCancelButtonClicked()
        {
            _tradeInputHandler.CancelTrade();
        }
    }
}
