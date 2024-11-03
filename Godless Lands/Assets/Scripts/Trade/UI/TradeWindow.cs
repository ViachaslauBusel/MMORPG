using Cells;
using Items;
using System.Collections.Generic;
using Systems.Stats;
using UnityEngine;
using UnityEngine.LowLevel;
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
        private CharacterStatsHolder _characterStatsHolder;

        [Inject]
        private void Construct(CharacterStatsHolder characterStatsHolder)
        {
            _characterStatsHolder = characterStatsHolder;
        }

        internal void SyncState(bool playerLock, bool partnerLock)
        {
            _playerLockPanel.enabled = playerLock || _playerLockPanel.enabled;
            _parnerLockPanel.enabled = partnerLock || _parnerLockPanel.enabled;
        }

        public void Open(string apponentName)
        {
            _playerLockPanel.enabled = false;
            _parnerLockPanel.enabled = false;
            _playerName.text = _characterStatsHolder.GetName();
            _partnerName.text = apponentName;
            Open();
        }

        public void LockPlayerPanel()
        {
            _playerLockPanel.enabled = true;
        }
    }
}
