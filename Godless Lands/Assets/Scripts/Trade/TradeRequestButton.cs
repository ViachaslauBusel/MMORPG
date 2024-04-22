using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Trade
{
    internal class TradeRequestButton : MonoBehaviour
    {
        private TradeInputHandler _tradeInputHandler;

        [Inject]
        public void Construct(TradeInputHandler tradeInputHandler)
        {
            _tradeInputHandler = tradeInputHandler;
        }

        public void OnClick()
        {
            _tradeInputHandler.RequestTrade();
        }
    }
}
