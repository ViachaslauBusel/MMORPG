using Network;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Quests.ActiveQuestTracker
{
    public class ActiveQuestsTrackerPersistence : IDisposable
    {
        private readonly ActiveQuestsTrackerModel _model;
        private readonly SessionManagmentService _sessionManagmentService;
        private Dictionary<int, bool> _loadData;

        public ActiveQuestsTrackerPersistence(ActiveQuestsTrackerModel model, SessionManagmentService sessionManagmentService)
        {
            _model = model;
            _sessionManagmentService = sessionManagmentService;

            _sessionManagmentService.OnCharacterIDChanged += LoadActiveQuests;
            LoadActiveQuests(_sessionManagmentService.CharacterID);

            _model.OnQuestListUpdated += SaveActiveQuests;
        }

        private void LoadActiveQuests(int characterId)
        {
            try
            {
                if (characterId == 0) return;

                string activeQuestsJson = PlayerPrefs.GetString("ActiveQuests_" + characterId, string.Empty);
                Debug.Log($"[{characterId}]Loading active quests: " + activeQuestsJson);
                if (string.IsNullOrEmpty(activeQuestsJson)) return;

                _loadData = JsonConvert.DeserializeObject<Dictionary<int, bool>>(activeQuestsJson);
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load active quests for character {characterId}: {e.Message}");
            }
        }

        private void SaveActiveQuests()
        {
            var quests = _model.Quests.ToDictionary(q => q, q => _model.IsQuestTracked(q));
            string activeQuestsJson = JsonConvert.SerializeObject(quests);
            PlayerPrefs.SetString("ActiveQuests_" + _sessionManagmentService.CharacterID, activeQuestsJson);
            Debug.Log($"[{_sessionManagmentService.CharacterID}]Saving active quests: " + activeQuestsJson);
        }

        internal void ClearData()
        {
            _loadData = null;
        }

        internal bool GetDataFor(int iD, bool defaultValue)
        {
            if (_loadData == null) return defaultValue;
            return _loadData.TryGetValue(iD, out bool value) ? value : defaultValue;
        }

        public void Dispose()
        {
            _sessionManagmentService.OnCharacterIDChanged -= LoadActiveQuests;
            _model.OnQuestListUpdated -= SaveActiveQuests;
        }
    }
}
