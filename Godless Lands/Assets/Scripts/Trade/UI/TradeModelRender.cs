using Cells;
using Cells.Trade;
using Items;
using System.Collections.Generic;
using UnityEngine;
using Windows;
using Zenject;

namespace Trade.UI
{
    public class TradeModelRender : WindowElement
    {
        [SerializeField]
        private Transform _playerTradeItemsHolder;
        [SerializeField]
        private Transform _partnerTradeItemsHolder;
        [SerializeField]
        private GameObject _tradeCellPrefab;
        private List<OfferCell> _playerTradeCells = new List<OfferCell>();
        private List<OfferCell> _parnerTradeCells = new List<OfferCell>();
        private TradeModel _tradeModel;
        private DiContainer _diContainer;

        [Inject]
        private void Construt(TradeModel tradeModel, DiContainer diContainer)
        {
            _tradeModel = tradeModel;
            _diContainer = diContainer;
        }

        protected override void HandleWindowOpen()
        {
            UpdateItems();
            _tradeModel.OnItemsChanged += UpdateItems;
        }

        protected override void HandleWindowClose()
        {
            _tradeModel.OnItemsChanged -= UpdateItems;
        }

        private void UpdateItems()
        {
            UpdateTradeItems(_tradeModel.PlayerTradeItems, _playerTradeCells, _playerTradeItemsHolder, true);
            UpdateTradeItems(_tradeModel.PartnerTradeItems, _parnerTradeCells, _partnerTradeItemsHolder, false);
        }

        private void UpdateTradeItems(Item[] tradeItems, List<OfferCell> tradeCells, Transform tradeItemsHolder, bool isPlayerOwner)
        {
            //Debug.Log($"TradeItems: {tradeItems.Length}, TradeCells: {tradeCells.Count}");
            // Create new cells if needed
            while (tradeCells.Count < tradeItems.Length)
            {
                var cell = _diContainer.InstantiatePrefabForComponent<OfferCell>(_tradeCellPrefab, tradeItemsHolder);
                cell.Init(isPlayerOwner, tradeCells.Count);
                tradeCells.Add(cell);
            }

            // Remove extra cells
            while (tradeCells.Count > tradeItems.Length)
            {
                int lastIndex = tradeCells.Count - 1;
                Destroy(tradeCells[lastIndex].gameObject);
                tradeCells.RemoveAt(lastIndex);
            }

            // Update cells
            for (int i = 0; i < tradeItems.Length; i++)
            {
                if (ShouldUpdateItem(tradeItems[i], tradeCells[i].GetItem()))
                {
                    tradeCells[i].PutItem(tradeItems[i]);
                }
            }
        }

        private bool ShouldUpdateItem(Item itemData, Item item)
        {
            return itemData != item;
        }
    }
}
