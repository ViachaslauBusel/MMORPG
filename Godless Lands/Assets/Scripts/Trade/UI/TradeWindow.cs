using Cells;
using Items;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Windows;
using Zenject;

namespace Trade.UI
{
    public class TradeWindow : Window
    {
        [SerializeField]
        private Text _playerName;
        [SerializeField]
        private Text _partnerName;
        [SerializeField]
        private Image _playerLockPanel;
        [SerializeField]
        private Image _parnerLockPanel;

        internal void Open(bool playerLock, bool partnerLock)
        {
            _playerLockPanel.enabled = playerLock;
            _parnerLockPanel.enabled = partnerLock;
            Open();
        }
    }
}
