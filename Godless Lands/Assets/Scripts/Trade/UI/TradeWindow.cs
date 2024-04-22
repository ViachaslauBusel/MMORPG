using Cells;
using Items;
using Protocol.Data.Items;
using RUCP;
using RUCP.Handler;
using System;
using System.Collections.Generic;
using Trade;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Trade.UI
{
    public class TradeWindow : MonoBehaviour
    {
        [SerializeField]
        private GameObject _tradeCellPrefab;
        [SerializeField]
        private Transform _playerTradeItemsHolder;
        [SerializeField]
        private Transform _partnerTradeItemsHolder;
        [SerializeField]
        private Text _playerName;
        [SerializeField]
        private Text _partnerName;
        [SerializeField]
        private Image _playerLockPanel;
        [SerializeField]
        private Image _parnerLockPanel;
        private List<OfferCell> _playerTradeCells = new List<OfferCell>();
        private List<OfferCell> _parnerTradeCells = new List<OfferCell>();
        private TradeModel _tradeModel;
        private DiContainer _diContainer;
        private Canvas _canvas;


        [Inject]
        private void Construt(TradeModel tradeModel, DiContainer diContainer)
        {
            _tradeModel = tradeModel;
            _diContainer = diContainer;
        }

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _canvas.enabled = false;
        }

        internal void Open(bool playerLock, bool partnerLock)
        {
            _playerLockPanel.enabled = playerLock;
            _parnerLockPanel.enabled = partnerLock;
            if (!_canvas.enabled)
            {
                _canvas.enabled = true;
                UpdateItems();
                _tradeModel.OnItemsChanged += UpdateItems;
            }
        }

        private void UpdateItems()
        {
            UpdateTradeItems(_tradeModel.PlayerTradeItems, _playerTradeCells, _playerTradeItemsHolder, true);
            UpdateTradeItems(_tradeModel.PartnerTradeItems, _parnerTradeCells, _partnerTradeItemsHolder, false);
        }

        private void UpdateTradeItems(Item[] tradeItems, List<OfferCell> tradeCells, Transform tradeItemsHolder, bool isPlayerOwner)
        {
            Debug.Log($"TradeItems: {tradeItems.Length}, TradeCells: {tradeCells.Count}");
            // Create new cells if needed
            while (tradeCells.Count < tradeItems.Length)
            {
                var cell = _diContainer.InstantiatePrefab(_tradeCellPrefab, tradeItemsHolder).GetComponent<OfferCell>();
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

        public void Close()
        {
            _canvas.enabled = false;
            _tradeModel.OnItemsChanged -= UpdateItems;
        }
    }
}
